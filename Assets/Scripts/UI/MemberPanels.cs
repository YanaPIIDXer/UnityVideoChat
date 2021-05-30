using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VideoChat.UI
{
    /// <summary>
    /// メンバ表示用パネル
    /// </summary>
    public class MemberPanels : MonoBehaviour
    {
        /// <summary>
        /// 自分自身のパネル
        /// </summary>
        [SerializeField]
        private GameObject OwnPanel = null;

        /// <summary>
        /// 他人のパネル
        /// </summary>
        [SerializeField]
        private GameObject[] OtherPanel = new GameObject[4] { null, null, null, null };
    }
}
