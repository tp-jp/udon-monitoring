using UdonSharp;
using UnityEngine;
using UnityEngine.UI;

namespace TpLab.UdonMonitoring.Udon
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class UdonMonitoring : UdonSharpBehaviour
    {
        [SerializeField]
        UdonSharpBehaviour[] targetScripts = new UdonSharpBehaviour[0];

        [HideInInspector]
        [SerializeField]
        GameObject[] detailViews;

        [HideInInspector]
        [SerializeField]
        Button[] detailButtons;

#if UNITY_EDITOR
        public UdonSharpBehaviour[] TargetScripts => targetScripts;
#endif

        void Start()
        {
            ShowDetailView(0);
        }

        public void OnClickedDetailButton()
        {
            var selectedIndex = GetSelectedIndex();
            if (selectedIndex < 0) return;
            ShowDetailView(selectedIndex);
        }

        int GetSelectedIndex()
        {
            for (var i = 0; i < detailButtons.Length; i++)
            {
                if (!detailButtons[i].enabled)
                {
                    return i;
                }
            }
            return -1;
        }

        void ShowDetailView(int index)
        {
            for (var i = 0; i < detailViews.Length; i++)
            {
                detailViews[i].SetActive(i == index);
            }
        }
    }
}