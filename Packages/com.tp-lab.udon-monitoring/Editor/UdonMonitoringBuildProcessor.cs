using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TpLab.UdonMonitoring.Editor
{
    public class UdonMonitoringBuildProcessor : IProcessSceneWithReport
    {
        public int callbackOrder => 0;

        /// <summary>
        /// シーンのビルド中に発生するイベント。
        /// </summary>
        /// <param name="scene">シーン</param>
        /// <param name="report">ビルドレポート</param>
        public void OnProcessScene(Scene scene, BuildReport report)
        {
            var targets = Object.FindObjectsOfType<Udon.UdonMonitoring>(true);
            foreach (var target in targets)
            {
                var editor = UnityEditor.Editor.CreateEditor(target, typeof(UdonMonitoringEditor)) as UdonMonitoringEditor;
                if (!editor) continue;
                editor.SetupUdonScript();
            }
        }
    }
}
