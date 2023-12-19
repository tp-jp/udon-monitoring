using UnityEditor;
using UnityEngine;

namespace TpLab.UdonMonitoring.Editor
{
    public static class AssetRepository
    {
        /// <summary>
        /// 設定ファイルのパス
        /// </summary>
        static readonly string SettingPath = "Packages/com.tp.jp.udon-monitoring/Runtime/UdonMonitoringSetting.asset";

        /// <summary>
        /// 設定ファイルを読み込む。存在しない場合は新規作成する。
        /// </summary>
        public static T LoadSetting<T>() where T : ScriptableObject, new()
        {
            return AssetDatabase.LoadAssetAtPath<T>(SettingPath) ?? ScriptableObject.CreateInstance<T>();
        }

        /// <summary>
        /// 設定ファイルを保存する。
        /// </summary>
        /// <param name="setting">設定</param>
        public static void SaveSetting<T>(T setting) where T : ScriptableObject
        {
            if (!AssetDatabase.Contains(setting))
            {
                AssetDatabase.CreateAsset(setting, SettingPath);
            }

            EditorUtility.SetDirty(setting);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}