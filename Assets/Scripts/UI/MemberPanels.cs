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
        private RectTransform OwnPanel = null;

        /// <summary>
        /// 他人のパネル
        /// </summary>
        [SerializeField]
        private RectTransform[] OtherPanel = new RectTransform[4] { null, null, null, null };

        /// <summary>
        /// 現在の他人の数
        /// </summary>
        private int CurrentOtherCount = 0;

        /// <summary>
        /// 自分自身のパネルにセット
        /// </summary>
        /// <param name="OwnTransform">Transform</param>
        public void SetOwn(RectTransform OwnTransform)
        {
            OwnTransform.SetParent(OwnPanel, false);
        }

        /// <summary>
        /// 他人をセット
        /// </summary>
        /// <param name="OtherTransform">他人のTransform</param>
        public void SetOther(RectTransform OtherTransform)
        {
            OtherTransform.SetParent(OtherPanel[CurrentOtherCount], false);
            CurrentOtherCount++;
        }
    }
}
