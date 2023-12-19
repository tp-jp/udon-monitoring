using System.Reflection;
using UdonSharp;

namespace TpLab.UdonMonitoring.Editor
{
    public static class UdonSharpBehaviourExtensions
    {
        /// <summary>
        /// フィールド一覧を取得する。
        /// </summary>
        /// <param name="self">UdonSharpBehaviour</param>
        /// <returns>Publicフィールド一覧</returns>
        public static FieldInfo[] GetFields(this UdonSharpBehaviour self)
            => self.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

        /// <summary>
        /// フィールドに値を設定する。
        /// </summary>
        /// <param name="self">UdonSharpBehaviour</param>
        /// <param name="fieldName">フィールド名</param>
        /// <param name="value">設定する値</param>
        /// <typeparam name="T">設定する値の型</typeparam>
        public static void SetFieldValue<T>(this UdonSharpBehaviour self, string fieldName, T value)
        {
            var field = self.GetType().GetField(fieldName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            field.SetValue(self, value);
        }
    }
}
