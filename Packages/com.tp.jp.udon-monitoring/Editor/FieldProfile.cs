using System;
using System.Reflection;
using TpLab.UdonMonitoring.Udon;
using UnityEditor;
using UnityEngine;
using VRC.SDK3.Data;

namespace TpLab.UdonMonitoring.Editor
{
    public class FieldProfile
    {
        /// <summary>
        /// フィールド名
        /// </summary>
        public string Name { get; }
        
        /// <summary>
        /// フィールドタイプ
        /// </summary>
        public FieldType FieldType { get; }
        
        /// <summary>
        /// 配列かどうか
        /// </summary>
        public bool IsArray { get; }

        /// <summary>
        /// ラベル
        /// </summary>
        public string Label { get; }

        /// <summary>
        /// コンストラクタ。
        /// </summary>
        /// <param name="field">フィールド情報</param>
        public FieldProfile(FieldInfo field)
        {
            Name = field.Name;
            FieldType = GetFieldType(field);
            IsArray = field.FieldType.IsArray;
            Label = UdonMonitoringSetting.Instance.IsConvertNicifyNames
                ? ObjectNames.NicifyVariableName(Name)
                : Name;
        }

        /// <summary>
        /// メタデータを取得する。
        /// </summary>
        /// <returns>メタデータ</returns>
        public FieldMetadata GetMetadata()
            => new FieldMetadata()
            {
                fieldName = Name,
                fieldType = FieldType,
                isArray = IsArray,
                label = Label
            };

        /// <summary>
        /// フィールドタイプを取得する。
        /// </summary>
        /// <param name="field">フィールド情報</param>
        /// <returns>フィールドタイプ</returns>
        static FieldType GetFieldType(FieldInfo field)
        {
            var type = field.FieldType;
            return type.IsArray ? GetFieldType(type.GetElementType()) : GetFieldType(type);
        }

        /// <summary>
        /// フィールドタイプを取得する。
        /// </summary>
        /// <param name="type">タイプ</param>
        /// <returns>フィールドタイプ</returns>
        static FieldType GetFieldType(Type type)
        {
            if (type == typeof(bool)) return FieldType.Bool;
            if (type == typeof(sbyte)) return FieldType.Sbyte;
            if (type == typeof(byte)) return FieldType.Byte;
            if (type == typeof(short)) return FieldType.Short;
            if (type == typeof(ushort)) return FieldType.Ushort;
            if (type == typeof(int)) return FieldType.Int;
            if (type == typeof(uint)) return FieldType.Uint;
            if (type == typeof(long)) return FieldType.Long;
            if (type == typeof(ulong)) return FieldType.Ulong;
            if (type == typeof(float)) return FieldType.Float;
            if (type == typeof(double)) return FieldType.Double;
            if (type == typeof(double)) return FieldType.DateTime;
            if (type == typeof(double)) return FieldType.String;
            if (type == typeof(Vector2)) return FieldType.Vector2;
            if (type == typeof(Vector3)) return FieldType.Vector3;
            if (type == typeof(Quaternion)) return FieldType.Quaternion;
            if (type.IsSubclassOf(typeof(Component))) return FieldType.Component;
            if (type == typeof(DataList)) return FieldType.DataList;
            return FieldType.Unknown;
        }
    }
}
