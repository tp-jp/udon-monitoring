using System.Reflection;
using UnityEngine;

namespace TpLab.UdonMonitoring.Editor
{
    public static class MonoBehaviourExtensions
    {
        /// <summary>
        /// フィールド一覧を取得する。
        /// </summary>
        /// <param name="behaviour">MonoBehaviour</param>
        /// <returns>Publicフィールド一覧</returns>
        public static FieldInfo[] GetFields(this MonoBehaviour behaviour)
            => behaviour.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
    }
}
