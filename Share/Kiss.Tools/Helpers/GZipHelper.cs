using System;
using System.Data;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace Kiss.Tools.Helpers
{
    /// <summary>
    /// GZip操作帮助类
    /// </summary>
    public class GZipHelper
    {
        /// <summary>
        /// 解压
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DataSet GetDataSetByString(string value)
        {
            var ds = new DataSet();
            var cc = GZipDecompressString(value);
            var sr = new StringReader(cc);
            ds.ReadXml(sr);
            return ds;
        }

        /// <summary>
        /// 根据DataSet压缩字符串
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static string GetStringByDataSet(string ds)
        {
            return GZipCompressString(ds);
        }

        /// <summary>
        /// 将传入字符串以GZip算法压缩后，返回Base64编码字符
        /// </summary>
        /// <param name="rawString">需要压缩的字符串</param>
        /// <returns>压缩后的Base64编码的字符串</returns>
        public static string GZipCompressString(string rawString)
        {
            if (string.IsNullOrEmpty(rawString) || rawString.Length == 0)
            {
                return "";
            }
            else
            {
                var rawData = Encoding.UTF8.GetBytes(rawString);
                var zippedData = Compress(rawData);
                return Convert.ToBase64String(zippedData);
            }
        }

        /// <summary>
        /// GZip压缩
        /// </summary>
        /// <param name="rawData"></param>
        /// <returns></returns>
        public static byte[] Compress(byte[] rawData)
        {
            var ms = new MemoryStream();
            var compressedZipStream = new GZipStream(ms, CompressionMode.Compress, true);
            compressedZipStream.Write(rawData, 0, rawData.Length);
            compressedZipStream.Close();
            return ms.ToArray();
        }

        /// <summary>
        /// 将传入的二进制字符串资料以GZip算法解压缩
        /// </summary>
        /// <param name="zippedString">经GZip压缩后的二进制字符串</param>
        /// <returns>原始未压缩字符串</returns>
        public static string GZipDecompressString(string zippedString)
        {
            if (string.IsNullOrEmpty(zippedString) || zippedString.Length == 0)
            {
                return "";
            }
            else
            {
                var zippedData = Convert.FromBase64String(zippedString);
                return Encoding.UTF8.GetString(Decompress(zippedData));
            }
        }

        /// <summary>
        /// ZIP解压
        /// </summary>
        /// <param name="zippedData"></param>
        /// <returns></returns>
        public static byte[] Decompress(byte[] zippedData)
        {
            var ms = new MemoryStream(zippedData);
            var compressedZipStream = new GZipStream(ms, CompressionMode.Decompress);
            var outBuffer = new MemoryStream();
            var block = new byte[1024];
            while (true)
            {
                var bytesRead = compressedZipStream.Read(block, 0, block.Length);
                if (bytesRead <= 0)
                {
                    break;
                }
                else
                {
                    outBuffer.Write(block, 0, bytesRead);
                }
            }
            compressedZipStream.Close();
            return outBuffer.ToArray();
        }
    }
}