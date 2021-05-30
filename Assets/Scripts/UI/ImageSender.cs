using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace VideoChat.UI
{
    /// <summary>
    /// 映像送信
    /// </summary>
    public class ImageSender : MonoBehaviour
    {
        /// <summary>
        /// PhotonView
        /// </summary>
        private PhotonView View = null;

        /// <summary>
        /// カメラの映像
        /// </summary>
        private CameraImage CamImage = null;

        void Awake()
        {
            View = GetComponent<PhotonView>();
            CamImage = new CameraImage();

            // テスト
            GetComponent<UnityEngine.UI.RawImage>().texture = CamImage.CameraTexture;
            CamImage.Play();
        }
    }
}
