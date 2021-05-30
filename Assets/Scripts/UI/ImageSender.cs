using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UniRx;
using UniRx.Triggers;
using System;
using ExitGames.Client.Photon;
using Photon.Realtime;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Triggers;

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

        /// <summary>
        /// ストリーム送信中か？
        /// </summary>
        private bool bIsStreaming = false;

        /// <summary>
        /// ハンドシェイクのイベントコード
        /// </summary>
        public const byte HandshakeEventCode = 10;

        /// <summary>
        /// ストリームのイベントコード
        /// </summary>
        public const byte StreamEventCode = 11;

        void Awake()
        {
            View = GetComponent<PhotonView>();
            CamImage = new CameraImage();

            this.UpdateAsObservable()
                .ThrottleFirstFrame(60)
                .Where(_ => !bIsStreaming)
                .Subscribe(_ => Send())
                .AddTo(gameObject);

            CamImage.Play();
        }

        /// <summary>
        /// 送信
        /// </summary>
        private async void Send()
        {
            if (!CamImage.IsPlaying) { return; }

            var Pixels = CamImage.CameraTexture.GetPixels();
            byte[] Data = new byte[Pixels.Length * 3];
            for (var i = 0; i < Pixels.Length; i++)
            {
                Data[i * 3 + 0] = (byte)(Pixels[i].r * 255);
                Data[i * 3 + 1] = (byte)(Pixels[i].g * 255);
                Data[i * 3 + 2] = (byte)(Pixels[i].b * 255);
            }

            // ハンドシェイク
            int[] HandshakeData = new int[] { View.ViewID, CameraImage.TextureWidth, CameraImage.TextureHeight, Data.Length };
            var EventOpt = new RaiseEventOptions()
            {
                CachingOption = EventCaching.DoNotCache,
                Receivers = ReceiverGroup.All
            };
            var SendOpt = new SendOptions()
            {
                Reliability = true
            };
            PhotonNetwork.RaiseEvent(HandshakeEventCode, HandshakeData, EventOpt, SendOpt);

            // ストリーム
            bIsStreaming = true;
            await UniTask.Run(() =>
            {
                Data.ToObservable()
                    .Buffer(Data.Length / 6)
                    .Subscribe(async BufferData =>
                    {
                        byte[] SendData = new byte[BufferData.Count];
                        BufferData.CopyTo(SendData, 0);
                        PhotonNetwork.RaiseEvent(StreamEventCode, SendData, EventOpt, SendOpt);
                        await UniTask.Delay(1);
                    });
            });
            bIsStreaming = false;
        }
    }
}
