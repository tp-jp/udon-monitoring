using System.Linq;
using UnityEditor;
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
            var targets = FindObjectsOfType<Udon.UdonMonitoring>();
            foreach (var target in targets)
            {
                var editor = UnityEditor.Editor.CreateEditor(target, typeof(UdonMonitoringEditor)) as UdonMonitoringEditor;
                editor.SetupUdonScript();
            }
        }

        /// <summary>
        /// 指定した型で見つかったオブジェクト一覧を取得する。
        /// </summary>
        /// <typeparam name="T">型</typeparam>
        /// <returns>オブジェクト一覧</returns>
        T[] FindObjectsOfType<T>() where T : Component
        {
            return Resources.FindObjectsOfTypeAll<T>()
                .Where(x => AssetDatabase.GetAssetOrScenePath(x).EndsWith(".unity"))
                .ToArray();
        }
    }
}
