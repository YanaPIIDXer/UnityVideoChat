using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VideoChat.Network;
using UniRx;
using System;

namespace VideoChat.Sequence
{
    /// <summary>
    /// チャット画面シーケンス
    /// </summary>
    public class ChatSequence : MonoBehaviour
    {
        void Awake()
        {
            PUNConnection.Instance.OnJoinRoom
                         .Subscribe(_ => Debug.Log("Join Room"))
                         .AddTo(gameObject);
        }
    }
}
