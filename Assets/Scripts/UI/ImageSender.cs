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

        void Awake()
        {
            View = GetComponent<PhotonView>();
        }
    }
}
