using TMPro;
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;

namespace TpLab.UdonMonitoring.Udon
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class UdonDetailButton : UdonSharpBehaviour
    {
        [SerializeField]
        TextMeshProUGUI objectName;

        [SerializeField]
        Image activeLine;

        [SerializeField]
        UdonSharpBehaviour targetScript;

        [SerializeField]
        Color activeLineColor = new Color(0f, 1f, 0f, 0.5f);

        [SerializeField]
        Color inactiveLineColor = new Color(1f, 0f, 0f, 0.5f);
        
        void Start()
        {
            objectName.text = targetScript.gameObject.name;
        }

        void Update()
        {
            activeLine.color = targetScript.enabled ? activeLineColor : inactiveLineColor;
        }
    }
}