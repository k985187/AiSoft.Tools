using System;
#if !NETSTANDARD
    using System.Media;
#endif
using System.Reflection;
using System.Threading.Tasks;

namespace AiSoft.Tools.Utils
{
    /// <summary>
    /// 音频播放
    /// </summary>
    public class SoundPlay
    {
#if  !NETSTANDARD

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