﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using AiSoft.Tools.Extensions;
using AiSoft.Tools.Systems;
using SharpCompress.Archives;
using SharpCompress.Common;
using SharpCompress.Writers;

namespace AiSoft.Tools.Files
{
    /// <summary>
    /// 7z压缩
    /// </summary>
    public class SevenZipCompressor : ISevenZipCompressor
    {
        private readonly HttpClient _httpClient;

        /// <summary>
        ///初始化
        /// </summary>
        /// <param name="httpClient"></param>
        public SevenZipCompressor(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        /// <summary>
        /// 将多个文件压缩到一个文件流中，可保存为zip文件，方便于web方式下载
        /// </summary>
        /// <param name="files">多个文件路径，文件或文件夹，或网络路径http/https</param>
        /// <param name="rootDir"></param>
        /// <param name="archiveType"></param>
        /// <returns>文件流</returns>
        public MemoryStream ZipStream(IEnumerable<string> files, string rootDir = "", ArchiveType archiveType = ArchiveType.SevenZip)
        {
            using (var archive = CreateZipArchive(files, rootDir, archiveType))
            {
                var ms = new MemoryStream();
                archive.SaveTo(ms, new WriterOptions(CompressionType.LZMA)
                {
                    LeaveStreamOpen = true,
                    ArchiveEncoding = new ArchiveEncoding {Default = Encoding.UTF8}
                });
                return ms;
            }
        }

        /// <summary>
        /// 将多个文件压缩到一个文件流中，可保存为zip文件，方便于web方式下载
        /// </summary>
        /// <param name="streams">多个文件流</param>
        /// <param name="archiveType"></param>
        /// <param name="disposeAllStreams">是否需要释放所有流</param>
        /// <returns>文件流</returns>
        public MemoryStream ZipStream(DisposableDictionary<string, Stream> streams, ArchiveType archiveType = ArchiveType.Zip, bool disposeAllStreams = false)
        {
            using (var archive = ArchiveFactory.Create(archiveType))
            {
                foreach (var pair in streams)
                {
                    archive.AddEntry(pair.Key, pair.Value, true);
                }

                var ms = new MemoryStream();
                archive.SaveTo(ms, new WriterOptions(CompressionType.LZMA)
                {
                    LeaveStreamOpen = true,
                    ArchiveEncoding = new ArchiveEncoding()
                    {
                        Default = Encoding.UTF8
                    }
                });
                if (disposeAllStreams)
                {
                    streams.Dispose();
                }
                return ms;
            }
        }

        /// <summary>
        /// 压缩多个文件
        /// </summary>
        /// <param name="files">多个文件路径，文件或文件夹</param>
        /// <param name="zipFile">压缩到...</param>
        /// <param name="rootDir">压缩包内部根文件夹</param>
        /// <param name="archiveType"></param>
        public void Zip(IEnumerable<string> files, string zipFile, string rootDir = "", ArchiveType archiveType = ArchiveType.SevenZip)
        {
            using (var archive = CreateZipArchive(files, rootDir, archiveType))
            {
                archive.SaveTo(zipFile, new WriterOptions(CompressionType.LZMA)
                {
                    LeaveStreamOpen = true,
                    ArchiveEncoding = new ArchiveEncoding {Default = Encoding.UTF8}
                });
            }
        }

        /// <summary>
        /// 压缩多个文件
        /// </summary>
        /// <param name="streams">多个文件流</param>
        /// <param name="zipFile">压缩到...</param>
        /// <param name="archiveType"></param>
        /// <param name="disposeAllStreams">是否需要释放所有流</param>
        public void Zip(DisposableDictionary<string, Stream> streams, string zipFile, ArchiveType archiveType = ArchiveType.Zip, bool disposeAllStreams = false)
        {
            using (var archive = ArchiveFactory.Create(archiveType))
            {
                foreach (var pair in streams)
                {
                    archive.AddEntry(pair.Key, pair.Value, true);
                }

                archive.SaveTo(zipFile, new WriterOptions(CompressionType.LZMA)
                {
                    LeaveStreamOpen = true,
                    ArchiveEncoding = new ArchiveEncoding()
                    {
                        Default = Encoding.UTF8
                    }
                });
                if (disposeAllStreams)
                {
                    streams.Dispose();
                }
            }
        }

        /// <summary>
        /// 解压文件，自动检测压缩包类型
        /// </summary>
        /// <param name="compressedFile">rar文件</param>
        /// <param name="dir">解压到...</param>
        public void Decompress(string compressedFile, string dir = "")
        {
            if (string.IsNullOrEmpty(dir))
            {
                dir = Path.GetDirectoryName(compressedFile);
            }
            ArchiveFactory.WriteToDirectory(compressedFile, Directory.CreateDirectory(dir).FullName, new ExtractionOptions {ExtractFullPath = true, Overwrite = true});
        }

        /// <summary>
        /// 创建zip包
        /// </summary>
        /// <param name="files"></param>
        /// <param name="rootDir"></param>
        /// <param name="archiveType"></param>
        /// <returns></returns>
        private IWritableArchive CreateZipArchive(IEnumerable<string> files, string rootDir, ArchiveType archiveType = ArchiveType.Zip)
        {
            var archive = ArchiveFactory.Create(archiveType);
            var dic = GetFileEntryMaps(files);
            var remoteUrls = files.Distinct().Where(s => s.StartsWith("http")).Select(s =>
            {
                try
                {
                    return new Uri(s);
                }
                catch (UriFormatException)
                {
                    return null;
                }
            }).Where(u => u != null).ToList();
            foreach (var pair in dic)
            {
                archive.AddEntry(Path.Combine(rootDir, pair.Value), pair.Key);
            }
            if (remoteUrls.Any())
            {
                var streams = new ConcurrentDictionary<string, Stream>();
                Parallel.ForEach(remoteUrls, url =>
                {
                    _httpClient.GetAsync(url).ContinueWith(async t =>
                    {
                        if (t.IsCompleted)
                        {
                            var res = await t;
                            if (res.IsSuccessStatusCode)
                            {
                                var stream = await res.Content.ReadAsStreamAsync();
                                streams[Path.Combine(rootDir, Path.GetFileName(HttpUtility.UrlDecode(url.AbsolutePath)))] = stream;
                            }
                        }
                    }).Wait();
                });
                foreach (var pair in streams)
                {
                    archive.AddEntry(pair.Key, pair.Value, true);
                }
            }
            return archive;
        }

        /// <summary>
        /// 获取文件路径和zip-entry的映射
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        private Dictionary<string, string> GetFileEntryMaps(IEnumerable<string> files)
        {
            var fileList = new List<string>();
            void GetFilesRecurs(string path)
            {
                //遍历目标文件夹的所有文件
                fileList.AddRange(Directory.GetFiles(path));
                //遍历目标文件夹的所有文件夹
                foreach (var directory in Directory.GetDirectories(path))
                {
                    GetFilesRecurs(directory);
                }
            }
            files.Where(s => !s.StartsWith("http")).ForEach(s =>
            {
                if (Directory.Exists(s))
                {
                    GetFilesRecurs(s);
                }
                else
                {
                    fileList.Add(s);
                }
            });
            if (!fileList.Any())
            {
                return new Dictionary<string, string>();
            }
            var dirname = new string(fileList.First().Substring(0, fileList.Min(s => s.Length)).TakeWhile((c, i) => fileList.All(s => s[i] == c)).ToArray());
            if (!Directory.Exists(dirname))
            {
                dirname = Directory.GetParent(dirname).FullName;
            }
            var dic = fileList.ToDictionary(s => s, s => s.Substring(dirname.Length));
            return dic;
        }
    }
}