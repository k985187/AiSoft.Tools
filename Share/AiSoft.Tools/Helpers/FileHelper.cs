using System;
using System.IO;

namespace AiSoft.Tools.Helpers
{
    /// <summary>
    /// 文件帮助类
    /// </summary>
    public class FileHelper
    {
        /// <summary>
        /// 拷贝目录内容
        /// </summary>
        /// <param name="sourceDirPath">源目录</param>
        /// <param name="saveDirPath">目的目录</param>
        /// <param name="copySubDirs">是否拷贝子目录</param>
        public static void CopyDirectory(string sourceDirPath, string saveDirPath, bool copySubDirs)
        {
            if (!Directory.Exists(saveDirPath))
            {
                // 目标目录若不存在就创建
                Directory.CreateDirectory(saveDirPath);
            }
            var files = Directory.GetFiles(sourceDirPath);
            foreach (var file in files)
            {
                // 复制目录中所有文件
                var pFilePath = $"{saveDirPath}\\{Path.GetFileName(file)}";
                File.Copy(file, pFilePath, true);
            }
            if (copySubDirs)
            {
                var dirs = Directory.GetDirectories(sourceDirPath);
                foreach (var dir in dirs)
                {
                    var destinationDir = $"{saveDirPath}\\{Path.GetFileName(dir)}";
                    // 复制子目录
                    CopyDirectory(dir, destinationDir, copySubDirs);
                }
            }
        }

        /// <summary>
        /// 删除指定目录下的指定后缀名的文件
        /// </summary>
        /// <param name="directory">要删除的文件所在的绝对目录</param>
        /// <param name="masks">要删除的文件的后缀名的一个数组</param>
        /// <param name="searchSubdirectories">表示是否需要递归删除，即是否也要删除子目录中相应的文件</param>
        /// <param name="ignoreHidden">表示是否忽略隐藏文件</param>
        /// <param name="deletedFileCount">表示总共删除的文件数</param>
        public static void DeleteFiles(string directory, string[] masks, bool searchSubdirectories, bool ignoreHidden, ref int deletedFileCount)
        {
            foreach (var file in Directory.GetFiles(directory, "*.*"))
            {
                if (!(ignoreHidden && (File.GetAttributes(file) & FileAttributes.Hidden) == FileAttributes.Hidden))
                {
                    foreach (var mask in masks)
                    {
                        if (Path.GetExtension(file) == mask)
                        {
                            File.Delete(file);
                            deletedFileCount++;
                        }
                    }
                }
            }
            if (searchSubdirectories)
            {
                var childDirectories = Directory.GetDirectories(directory);
                foreach (var dir in childDirectories)
                {
                    if (!(ignoreHidden && (File.GetAttributes(dir) & FileAttributes.Hidden) == FileAttributes.Hidden))
                    {
                        DeleteFiles(dir, masks, searchSubdirectories, ignoreHidden, ref deletedFileCount);
                    }
                }
            }
        }
    }
}