using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VideoChat.Network;
using UniRx;
using System;
using UnityEngine.SceneManagement;

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

            PUNConnection.Instance.OnJoinRoom
                         .Subscribe(_ =>
                         {
                             SceneManager.LoadScene("Chat");
                         }).AddTo(gameObject);
        }
    }
}
