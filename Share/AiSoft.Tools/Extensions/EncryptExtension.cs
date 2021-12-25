using System;
using AiSoft.Tools.Security;

namespace AiSoft.Tools.Extensions
{
    public static class EncryptExtensions
    {
        /// <summary>
        /// 加密Key(32位)
        /// </summary>
        public static string AesKey { get; set; } = "LaxNUhu2sF2mGfVHlj3Emb0yQrOmbmuo";

        /// <summary>
        /// 加密IV(16位)
        /// </summary>
        public static string AesIv { get; set; } = "FbNWxb25Ze6qEGbD";

        /// <summary>
        /// 数组加密
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        public static byte[] EncryptTo(this byte[] bytes, string key = null, string iv = null)
        {
            if (bytes == null)
            {
                return bytes;
            }
            return EncryptProvider.AESEncrypt(bytes, string.IsNullOrWhiteSpace(key) ? AesKey : key, string.IsNullOrWhiteSpace(iv) ? AesIv : iv);
        }

        /// <summary>
        /// 数组解密
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        public static byte[] DecryptTo(this byte[] bytes, string key = null, string iv = null)
        {
            if (bytes == null)
            {
                return bytes;
            }
            return EncryptProvider.AESDecrypt(bytes, string.IsNullOrWhiteSpace(key) ? AesKey : key, string.IsNullOrWhiteSpace(iv) ? AesIv : iv);
        }

        /// <summary>
        /// 字符串加密
        /// </summary>
        /// <param name="str"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param> 
        /// <returns></returns>
        public static string EncryptTo(this string str, string key = null, string iv = null)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return str;
            }
            return EncryptProvider.AESEncrypt(str, string.IsNullOrWhiteSpace(key) ? AesKey : key, string.IsNullOrWhiteSpace(iv) ? AesIv : iv);
        }

        /// <summary>
        /// 字符串解密
        /// </summary>
        /// <param name="str"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param> 
        /// <returns></returns>
        public static string DecryptTo(this string str, string key = null, string iv = null)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return str;
            }
            return EncryptProvider.AESDecrypt(str, string.IsNullOrWhiteSpace(key) ? AesKey : key, string.IsNullOrWhiteSpace(iv) ? AesIv : iv);
        }
    }
}