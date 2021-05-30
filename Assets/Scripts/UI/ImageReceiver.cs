using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace VideoChat.UI
{
    /// <summary>
    /// 映像受信
    /// </summary>
    public class ImageReceiver : MonoBehaviour, IPunInstantiateMagicCallback
    {
        /// <summary>
        /// PhotonView
        /// </summary>
        private PhotonView View = null;

        void Awake()
        {
            View = GetComponent<PhotonView>();
        }

        public void OnPhotonInstantiate(PhotonMessageInfo info)
        {
            if (info.photonView.IsMine)
            {
                MemberPanels.Instance.SetOwn(GetComponent<RectTransform>());
            }
            else
            {
                MemberPanels.Instance.SetOther(GetComponent<RectTransform>());
            }
        }
    }
}
