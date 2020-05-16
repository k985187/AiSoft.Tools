using System;
#if !NETSTANDARD2_0
    using System.Media;
#endif
using System.Reflection;
using System.Threading.Tasks;

namespace Kiss.Tools.Utils
{
    /// <summary>
    /// 音频播放
    /// </summary>
    public class SoundPlay
    {
#if  !NETSTANDARD2_0
        /// <summary>
        /// 播放
        /// </summary>
        /// <param name="mediaFilePath">音频文件路径</param>
        public static void Play(string mediaFilePath)
        {
            Task.Run(() =>
            {
                var runWavStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(mediaFilePath);
                var soundPlayer = new SoundPlayer {Stream = runWavStream};
                soundPlayer.Play();
            });
        }
#endif
    }
}