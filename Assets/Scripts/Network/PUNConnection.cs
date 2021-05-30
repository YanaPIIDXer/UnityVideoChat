using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pun2Task;
using System.Threading;
using Cysharp.Threading.Tasks;
using UniRx;
using System;

namespace VideoChat.Network
{
    /// <summary>
    /// PUNとの接続
    /// </summary>
    public class PUNConnection : MonoBehaviour
    {
        /// <summary>
        /// インスタンス
        /// </summary>
        public static PUNConnection Instance { get; private set; } = null;

        /// <summary>
        /// サーバ接続時Subject
        /// </summary>
        private Subject<Unit> OnConnectedServerSubject = new Subject<Unit>();

        /// <summary>
        /// サーバに接続した
        /// </summary>
        public Subject<Unit> OnConnectedServer { get { return OnConnectedServerSubject; } }

        async void Awake()
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            try
            {
                var Token = this.GetCancellationTokenOnDestroy();
                await Pun2TaskNetwork.ConnectUsingSettingsAsync(Token);

                await Pun2TaskNetwork.JoinLobbyAsync(token: Token);
            }
            catch (Exception e)
            {
                Debug.LogError("Connect to PUN Failed. Reason:" + e.Message);
            }

            OnConnectedServerSubject.OnNext(Unit.Default);
        }
    }
}
