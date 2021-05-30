using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pun2Task;
using System.Threading;
using Cysharp.Threading.Tasks;
using UniRx;
using System;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

namespace VideoChat.Network
{
    /// <summary>
    /// PUNとの接続
    /// </summary>
    public class PUNConnection : MonoBehaviourPunCallbacks
    {
        /// <summary>
        /// インスタンス
        /// </summary>
        public static PUNConnection Instance { get; private set; } = null;

        /// <summary>
        /// サーバ接続時Subject
        /// </summary>
        private Subject<Unit> OnConnectServerSubject = new Subject<Unit>();

        /// <summary>
        /// サーバに接続した
        /// </summary>
        public Subject<Unit> OnConnectServer { get { return OnConnectServerSubject; } }

        /// <summary>
        /// 切断時Subject
        /// </summary>
        private Subject<DisconnectCause> OnDisconnectSubject = new Subject<DisconnectCause>();

        /// <summary>
        /// 切断された
        /// </summary>
        public IObservable<DisconnectCause> OnDisconnect { get { return OnDisconnectSubject; } }

        /// <summary>
        /// ルームリスト更新時Subject
        /// </summary>
        private Subject<List<RoomInfo>> OnRoomListUpdatedSubject = new Subject<List<RoomInfo>>();

        /// <summary>
        /// ルームリストが更新された
        /// </summary>
        public IObservable<List<RoomInfo>> OnRoomListUpdated { get { return OnRoomListUpdatedSubject; } }

        /// <summary>
        /// ルーム入室Subject
        /// </summary>
        private Subject<Unit> OnJoinRoomSubject = new Subject<Unit>();

        /// <summary>
        /// ルームに入室した
        /// </summary>
        public IObservable<Unit> OnJoinRoom { get { return OnJoinRoomSubject; } }

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

            OnConnectServerSubject.OnNext(Unit.Default);
        }

        /// <summary>
        /// デバッグ用。
        /// ルームを新規作成するか既存のルームに入る
        /// </summary>
        public async void DebugJoinRoom()
        {
            SceneManager.LoadScene("Chat");
            var Token = this.GetCancellationTokenOnDestroy();
            try
            {
                await Pun2TaskNetwork.JoinOrCreateRoomAsync("TestRoom", new RoomOptions(), TypedLobby.Default, token: Token);
            }
            catch (Exception e)
            {
                Debug.LogError("Create or Join Room Failed. Reason:" + e.Message);
                SceneManager.LoadScene("Lobby");
            }

            OnJoinRoomSubject.OnNext(Unit.Default);
        }

        /// <summary>
        /// 切断された
        /// </summary>
        /// <param name="cause">切断理由</param>
        public override void OnDisconnected(DisconnectCause cause)
        {
            OnDisconnectSubject.OnNext(cause);
        }

        /// <summary>
        /// ルームリスト更新
        /// </summary>
        /// <param name="roomList">ルームリスト</param>
        public override void OnRoomListUpdate(List<RoomInfo> roomList)
        {
            OnRoomListUpdatedSubject.OnNext(roomList);
        }
    }
}
