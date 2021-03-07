using System;
using AiSoft.Tools.Security;

namespace AiSoft.Tools.Helpers
{
    public static class EncryptHelper
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
        /// <returns></returns>
        public static byte[] EncryptTo(this byte[] bytes)
        {
            if (bytes == null)
            {
                return bytes;
            }
            return EncryptProvider.AESEncrypt(bytes, AesKey, AesIv);
        }

        /// <summary>
        /// 数组解密
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static byte[] DecryptTo(this byte[] bytes)
        {
            if (bytes == null)
            {
                return bytes;
            }
            return EncryptProvider.AESDecrypt(bytes, AesKey, AesIv);
        }

        /// <summary>
        /// 字符串加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string EncryptTo(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return str;
            }
            return EncryptProvider.AESEncrypt(str, AesKey, AesIv);
        }

        /// <summary>
        /// 字符串解密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string DecryptTo(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return str;
            }
            return EncryptProvider.AESDecrypt(str, AesKey, AesIv);
        }
    }
}