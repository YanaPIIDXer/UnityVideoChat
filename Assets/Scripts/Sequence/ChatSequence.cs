using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VideoChat.Network;
using UniRx;
using System;
using Photon.Pun;
using VideoChat.UI;

namespace VideoChat.Sequence
{
    /// <summary>
    /// チャット画面シーケンス
    /// </summary>
    public class ChatSequence : MonoBehaviour
    {
        /// <summary>
        /// パネル
        /// </summary>
        [SerializeField]
        private MemberPanels Panels = null;

        void Awake()
        {
            PUNConnection.Instance.OnJoinRoom
                         .Subscribe(_ => OnJoinRoom())
                         .AddTo(gameObject);
        }

        /// <summary>
        /// 入室した
        /// </summary>
        private void OnJoinRoom()
        {
            var Img = PhotonNetwork.Instantiate("Prefabs/CameraImage", Vector3.zero, Quaternion.identity);
        }
    }
}
