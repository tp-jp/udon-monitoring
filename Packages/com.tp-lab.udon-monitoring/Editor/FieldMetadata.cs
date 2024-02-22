using System;
using TpLab.UdonMonitoring.Udon;

namespace TpLab.UdonMonitoring.Editor
{
    [Serializable]
    public class FieldMetadata
    {
        /// <summary>
        /// フィールド名
        /// </summary>
        public string fieldName;

        /// <summary>
        /// フィールドタイプ
        /// </summary>
        public FieldType fieldType;

        /// <summary>
        /// 配列かどうか
        /// </summary>
        public bool isArray;
        
        /// <summary>
        /// ラベル
        /// </summary>
        public string label;
    }
}
