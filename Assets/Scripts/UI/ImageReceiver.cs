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

        /// <summary>
        /// 受信バッファ
        /// </summary>
        private byte[] RecvBuffer = null;

        /// <summary>
        /// テクスチャの幅
        /// </summary>
        private int TextureWidth = 0;

        /// <summary>
        /// テクスチャの高さ
        /// </summary>
        private int TextureHeight = 0;

        /// <summary>
        /// データ長
        /// </summary>
        private int DataLength = 0;

        /// <summary>
        /// 現在のデータ長
        /// </summary>
        private int CurrentDataLength = 0;

        /// <summary>
        /// ストリーミング中か？
        /// </summary>
        private bool IsStreaming { get { return RecvBuffer != null; } }

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

                case ImageSender.StreamEventCode:

                    if (IsStreaming)
                    {
                        OnRecvStream(photonEvent.CustomData as byte[]);
                    }
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
        private void OnRecvHandshake(int ViewID, int Width, int Height, int Length)
        {
            if (ViewID != View.ViewID) { return; }

            TextureWidth = Width;
            TextureHeight = Height;
            DataLength = Length;
            CurrentDataLength = 0;

            RecvBuffer = new byte[Length];
            Debug.Log("Stream Start");
        }

        /// <summary>
        /// ストリームを受信した
        /// </summary>
        /// <param name="Data">データ</param>
        private void OnRecvStream(byte[] Data)
        {
            Data.CopyTo(RecvBuffer, CurrentDataLength);
            CurrentDataLength += Data.Length;
            Debug.Log("StreamLength:" + CurrentDataLength + " / " + DataLength);

            if (CurrentDataLength >= DataLength)
            {
                // TODO:テクスチャ構築
                RecvBuffer = null;
                Debug.Log("Stream End");
            }
        }
    }
}
