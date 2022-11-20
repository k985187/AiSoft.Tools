﻿using System;
using System.Collections.Generic;
using System.IO;
using AiSoft.Tools.Systems;
using SharpCompress.Common;

namespace AiSoft.Tools.Files
{
    /// <summary>
    /// 7z压缩
    /// </summary>
    public interface ISevenZipCompressor
    {
        /// <summary>
        /// 解压文件，自动检测压缩包类型
        /// </summary>
        /// <param name="compressedFile">rar文件</param>
        /// <param name="dir">解压到...</param>
        void Decompress(string compressedFile, string dir = "");

        /// <summary>
        /// 压缩多个文件
        /// </summary>
        /// <param name="files">多个文件路径，文件或文件夹</param>
        /// <param name="zipFile">压缩到...</param>
        /// <param name="rootDir">压缩包内部根文件夹</param>
        /// <param name="archiveType"></param>
        void Zip(IEnumerable<string> files, string zipFile, string rootDir = "", ArchiveType archiveType = ArchiveType.Zip);

        /// <summary>
        /// 压缩多个文件
        /// </summary>
        /// <param name="streams">多个文件流</param>
        /// <param name="zipFile">压缩到...</param>
        /// <param name="archiveType"></param>
        /// <param name="disposeAllStreams">是否需要释放所有流</param>
        void Zip(DisposableDictionary<string, Stream> streams, string zipFile, ArchiveType archiveType = ArchiveType.Zip, bool disposeAllStreams = false);

        /// <summary>
        /// 将多个文件压缩到一个文件流中，可保存为zip文件，方便于web方式下载
        /// </summary>
        /// <param name="files">多个文件路径，文件或文件夹，或网络路径http/https</param>
        /// <param name="rootDir"></param>
        /// <param name="archiveType"></param>
        /// <returns>文件流</returns>
        MemoryStream ZipStream(IEnumerable<string> files, string rootDir = "", ArchiveType archiveType = ArchiveType.Zip);

        /// <summary>
        /// 将多个文件压缩到一个文件流中，可保存为zip文件，方便于web方式下载
        /// </summary>
        /// <param name="streams">多个文件流</param>
        /// <param name="archiveType"></param>
        /// <param name="disposeAllStreams">是否需要释放所有流</param>
        /// <returns>文件流</returns>
        MemoryStream ZipStream(DisposableDictionary<string, Stream> streams, ArchiveType archiveType = ArchiveType.Zip, bool disposeAllStreams = false);
    }
}