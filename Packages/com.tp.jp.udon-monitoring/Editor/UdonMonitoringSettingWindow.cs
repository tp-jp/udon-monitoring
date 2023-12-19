using System;
using UnityEditor;
using UnityEngine;

namespace TpLab.UdonMonitoring.Editor
{
    public class UdonMonitoringSettingWindow : EditorWindow
    {
        UdonMonitoringSetting _setting;
        UnityEditor.Editor _editor;

        [MenuItem("TpLab/UdonMonitoring", false)]
        static void ShowWindow()
        {
            var window = GetWindow<UdonMonitoringSettingWindow>(nameof(UdonMonitoringSettingWindow));
            window.minSize = new Vector2(450f, 400f);
            window._setting = AssetRepository.LoadSetting<UdonMonitoringSetting>();
            window._editor = UnityEditor.Editor.CreateEditor(window._setting);
        }

        void OnGUI()
        {
            _editor.OnInspectorGUI();
        }

        void OnDestroy()
        {
            AssetRepository.SaveSetting(_setting);
        }
    }
}

