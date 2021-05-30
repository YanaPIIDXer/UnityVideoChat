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
        /// テクスチャの幅
        /// </summary>
        public const int TextureWidth = 640;

        /// <summary>
        /// テクスチャの高さ
        /// </summary>
        public const int TextureHeight = 480;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CameraImage()
        {
            WebCamDevice Device = WebCamTexture.devices[0];
            CameraTexture = new WebCamTexture(Device.name, TextureWidth, TextureHeight, 30);
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
