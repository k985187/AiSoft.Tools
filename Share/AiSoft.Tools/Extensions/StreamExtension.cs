using System.IO;
#if NET
    using System;
    using System.Buffers;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Threading.Tasks;
#endif

namespace AiSoft.Tools.Extensions
{
    public static class StreamExtension
    {
        /// <summary>
        /// 将流转换为内存流
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static MemoryStream SaveAsMemoryStream(this Stream stream)
        {
            stream.Position = 0;
            return new MemoryStream(stream.ToArray());
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static byte[] ToArray(this Stream stream)
        {
            stream.Position = 0;
            var bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            // 设置当前流的位置为流的开始
            stream.Seek(0, SeekOrigin.Begin);
            return bytes;
        }

#if NET

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<byte[]> ToArrayAsync(this Stream stream, CancellationToken cancellationToken = default)
        {
            var bytes = new byte[stream.Length];
            await stream.ReadAsync(bytes, cancellationToken);
            return bytes;
        }

#endif
    }
}