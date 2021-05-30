using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VideoChat.UI
{
    /// <summary>
    /// カメライメージ
    /// </summary>
    public class CameraImage
    {
        /// <summary>
        /// Webカメラテクスチャ
        /// </summary>
        public WebCamTexture CameraTexture { get; private set; } = null;

        /// <summary>
        /// 再生中？
        /// </summary>
        public bool IsPlaying { get { return CameraTexture.isPlaying; } }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CameraImage()
        {
            WebCamDevice Device = WebCamTexture.devices[0];
            CameraTexture = new WebCamTexture(Device.name, 640, 480, 30);
        }

        /// <summary>
        /// デストラクタ
        /// </summary>
        ~CameraImage()
        {
            CameraTexture.Stop();
            CameraTexture = null;
        }

        /// <summary>
        /// 再生
        /// </summary>
        public void Play()
        {
            CameraTexture.Play();
        }

        /// <summary>
        /// 停止
        /// </summary>
        public void Stop()
        {
            CameraTexture.Stop();
        }
    }
}
