using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

namespace VideoChat.UI
{
    /// <summary>
    /// 映像受信
    /// </summary>
    public class ImageReceiver : MonoBehaviour, IPunInstantiateMagicCallback, IOnEventCallback
    {
        /// <summary>
        /// PhotonView
        /// </summary>
        private PhotonView View = null;

        void Awake()
        {
            View = GetComponent<PhotonView>();
        }

        void OnEnable()
        {
            PhotonNetwork.AddCallbackTarget(this);
        }

        void OnDisable()
        {
            PhotonNetwork.RemoveCallbackTarget(this);
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

        public void OnEvent(EventData photonEvent)
        {
            switch (photonEvent.Code)
            {
                case ImageSender.HandshakeEventCode:

                    int[] Data = photonEvent.CustomData as int[];
                    OnRecvHandshake(Data[0], Data[1], Data[2], Data[3]);
                    break;
            }
        }

        /// <summary>
        /// ハンドシェイクを受信した
        /// </summary>
        /// <param name="ViewID">ViewID</param>
        /// <param name="Width">幅</param>
        /// <param name="Height">高さ</param>
        /// <param name="DataLength">データ長</param>
        private void OnRecvHandshake(int ViewID, int Width, int Height, int DataLength)
        {
            Debug.Log("OnRecvHandshake");
            Debug.Log("ViewID:" + ViewID + " Width:" + Width + " Height:" + Height + " DataLength:" + DataLength);
        }
    }
}
