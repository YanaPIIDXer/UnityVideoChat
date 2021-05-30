using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VideoChat.Network;
using UniRx;
using System;

namespace VideoChat.Sequence
{
    /// <summary>
    /// ロビーシーケンス
    /// </summary>
    public class LobbySequence : MonoBehaviour
    {
        void Start()
        {
            PUNConnection.Instance.OnConnectServer
                         .Subscribe(_ =>
                         {
                             PUNConnection.Instance.DebugJoinRoom();
                         }).AddTo(gameObject);
        }
    }
}
