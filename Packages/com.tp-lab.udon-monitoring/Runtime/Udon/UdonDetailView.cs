using System;
using TMPro;
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDK3.Data;
using VRC.SDKBase;
using static VRC.SDKBase.Utilities;

namespace TpLab.UdonMonitoring.Udon
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class UdonDetailView : UdonSharpBehaviour
    {
        [SerializeField]
        TextMeshProUGUI detailTitle;

        [SerializeField]
        TextMeshProUGUI[] variableNames;

        [SerializeField]
        TextMeshProUGUI[] variableValues;

        [SerializeField]
        GameObject ownerRecord;

        [SerializeField]
        TextMeshProUGUI ownerValue;

        [SerializeField]
        bool isShowOwner;

        [SerializeField]
        UdonSharpBehaviour targetScript;

        [SerializeField]
        string fieldMetadataJson;

        [SerializeField]
        string settingMetadataJson;

        DataList _fieldMetadata;
        DataDictionary _settingMetadata;

        /// <summary>
        /// 初期化。
        /// </summary>
        void Start()
        {
            _fieldMetadata = DeserializeFromJson(fieldMetadataJson, "field metadata").DataList;
            _settingMetadata = DeserializeFromJson(settingMetadataJson, "setting metadata").DataDictionary;
            ownerRecord.SetActive(isShowOwner);
        }

        /// <summary>
        /// 有効化した際に呼ばれるイベント。
        /// </summary>
        void OnEnable()
        {
            detailTitle.text = $"<b>{targetScript.name}</b> ({GetUdonTypeShortName(targetScript)})";
        }

        /// <summary>
        /// 更新処理。
        /// </summary>
        void Update()
        {
            if (isShowOwner)
            {
                var owner = Networking.GetOwner(targetScript.gameObject);
                if (!IsValid(owner)) return;
                ownerValue.text = $"{owner.displayName} ({owner.playerId})";
            }

            for (var i = 0; i < _fieldMetadata.Count; i++)
            {
                var fieldMetadata = _fieldMetadata[i].DataDictionary;
                var fieldName = fieldMetadata["fieldName"].String;
                var fieldType = (FieldType)fieldMetadata["fieldType"].GetNumber<int>();
                var isArray = fieldMetadata["isArray"].Boolean;
                var label = fieldMetadata["label"].String;
                var value = targetScript.GetProgramVariable(fieldName);
                variableNames[i].text = label;
                variableValues[i].text = FormatValue(value, fieldType, _settingMetadata, isArray);
            }
        }

        /// <summary>
        /// JSONからデシリアライズする。
        /// </summary>
        /// <param name="json">JSON文字列</param>
        /// <param name="name">データ名</param>
        /// <returns>データトークン</returns>
        static DataToken DeserializeFromJson(string json, string name)
        {
            if (VRCJson.TryDeserializeFromJson(json, out var result))
            {
                return result;
            }
            Debug.LogError($"Failed to deserialize {name}");
            return new DataToken(DataError.None);
        }

        /// <summary>
        /// namespaceを除いたクラス名を取得する。
        /// </summary>
        /// <param name="script">UdonSharpBehaviour</param>
        /// <returns>クラス名</returns>
        static string GetUdonTypeShortName(UdonSharpBehaviour script)
        {
            var names = script.GetUdonTypeName().Split('.');
            return names[names.Length - 1];
        }

        #region Format

        /// <summary>
        /// 値をフォーマットする。
        /// </summary>
        /// <param name="value">値</param>
        /// <param name="fieldType">フィールドタイプ</param>
        /// <param name="setting">設定</param>
        /// <param name="isArray">配列かどうか</param>
        /// <returns>フォーマット後の値</returns>
        static string FormatValue(object value, FieldType fieldType, DataDictionary setting, bool isArray = false)
        {
            if (value == null) return FormatNullValue(setting);
            if (isArray)
            {
                switch (fieldType)
                {
                    case FieldType.Bool:
                        return FormatArray((bool[])value, fieldType, setting);
                    case FieldType.Sbyte:
                        return FormatArray((sbyte[])value, fieldType, setting);
                    case FieldType.Byte:
                        return FormatArray((byte[])value, fieldType, setting);
                    case FieldType.Short:
                        return FormatArray((short[])value, fieldType, setting);
                    case FieldType.Ushort:
                        return FormatArray((ushort[])value, fieldType, setting);
                    case FieldType.Int:
                        return FormatArray((int[])value, fieldType, setting);
                    case FieldType.Uint:
                        return FormatArray((uint[])value, fieldType, setting);
                    case FieldType.Long:
                        return FormatArray((long[])value, fieldType, setting);
                    case FieldType.Ulong:
                        return FormatArray((ulong[])value, fieldType, setting);
                    case FieldType.Float:
                        return FormatArray((float[])value, fieldType, setting);
                    case FieldType.Double:
                        return FormatArray((double[])value, fieldType, setting);
                    case FieldType.DateTime:
                        return FormatArray((DateTime[])value, fieldType, setting);
                    case FieldType.String:
                        return FormatArray((string[])value, fieldType, setting);
                    case FieldType.Vector2:
                        return FormatArray((Vector2[])value, fieldType, setting);
                    case FieldType.Vector3:
                        return FormatArray((Vector3[])value, fieldType, setting);
                    case FieldType.Quaternion:
                        return FormatArray((Quaternion[])value, fieldType, setting);
                    case FieldType.Component:
                        return FormatArray((Component[])value, fieldType, setting);
                }
            }
            else
            {
                switch (fieldType)
                {
                    case FieldType.Bool:
                        return FormatValue((bool)value, setting);
                    case FieldType.Vector2:
                        return FormatValue((Vector2)value, setting);
                    case FieldType.Vector3:
                        return FormatValue((Vector3)value, setting);
                    case FieldType.Quaternion:
                        return FormatValue((Quaternion)value, setting);
                    case FieldType.DataList:
                        return FormatDataList((DataList)value, setting);
                }
            }

            return value.ToString();
        }

        /// <summary>
        /// NULL値をフォーマットする。
        /// </summary>
        /// <param name="setting">設定</param>
        /// <returns>フォーマット後の値</returns>
        static string FormatNullValue(DataDictionary setting)
            => $"<color=#{setting["nullColor"].String}>NULL</color>";

        /// <summary>
        /// bool値をフォーマットする。
        /// </summary>
        /// <param name="value">値</param>
        /// <param name="setting">設定</param>
        /// <returns>フォーマット後の値</returns>
        static string FormatValue(bool value, DataDictionary setting)
            => value
            ? $"<color=#{setting["trueColor"].String}>TRUE</color>"
            : $"<color=#{setting["falseColor"].String}>FALSE</color>";

        /// <summary>
        /// Vector2値をフォーマットする。
        /// </summary>
        /// <param name="value">値</param>
        /// <param name="setting">設定</param>
        /// <returns>フォーマット後の値</returns>
        static string FormatValue(Vector2 value, DataDictionary setting)
            => $"X:<color=#{setting["xColor"].String}>[{value.x}]</color> Y:<color=#{setting["yColor"].String}>[{value.y}]</color>";

        /// <summary>
        /// Vector3値をフォーマットする。
        /// </summary>
        /// <param name="value">値</param>
        /// <param name="setting">設定</param>
        /// <returns>フォーマット後の値</returns>
        static string FormatValue(Vector3 value, DataDictionary setting)
            => $"X:<color=#{setting["xColor"].String}>[{value.x}]</color> Y:<color=#{setting["yColor"].String}>[{value.y}]</color> Z:<color=#{setting["zColor"].String}>[{value.z}]</color>";

        /// <summary>
        /// Quaternion値をフォーマットする。
        /// </summary>
        /// <param name="value">値</param>
        /// <param name="setting">設定</param>
        /// <returns>フォーマット後の値</returns>
        static string FormatValue(Quaternion value, DataDictionary setting)
            => $"X:<color=#{setting["xColor"].String}>[{value.x}]</color> Y:<color=#{setting["yColor"].String}>[{value.y}]</color> Z:<color=#{setting["zColor"].String}>[{value.z}]</color> W:<color=#{setting["wColor"].String}>[{value.w}]</color>";

        /// <summary>
        /// 配列をフォーマットする。
        /// </summary>
        /// <param name="array">配列</param>
        /// <param name="fieldType">フィールドタイプ</param>
        /// <param name="setting">設定</param>
        /// <typeparam name="T">配列の要素の型</typeparam>
        /// <returns>フォーマットした値</returns>
        static string FormatArray<T>(T[] array, FieldType fieldType, DataDictionary setting)
        {
            var result = "";
            for (var i = 0; i < array.Length; i++)
            {
                result += $"[{i}]: " + FormatValue(array[i], fieldType, setting) + "\n";
            }

            return result.Length > 0
                ? result.Remove(result.Length - 1)
                : result;
        }

        /// <summary>
        /// DataListをフォーマットする。
        /// </summary>
        /// <param name="dataList">データリスト</param>
        /// <param name="setting">設定</param>
        /// <returns>フォーマットした値</returns>
        static string FormatDataList(DataList dataList, DataDictionary setting)
        {
            var result = "";
            for (var i = 0; i < dataList.Count; i++)
            {
                result += $"[{i}]: ";
                switch (dataList[i].TokenType)
                {
                    case TokenType.Null:
                        result += FormatNullValue(setting);
                        break;
                    case TokenType.Boolean:
                        result += FormatValue(dataList[i].Boolean, setting);
                        break;
                    case TokenType.SByte:
                        result += FormatValue(dataList[i].SByte, FieldType.Sbyte, setting);
                        break;
                    case TokenType.Byte:
                        result += FormatValue(dataList[i].Byte, FieldType.Byte, setting);
                        break;
                    case TokenType.Short:
                        result += FormatValue(dataList[i].Short, FieldType.Short, setting);
                        break;
                    case TokenType.UShort:
                        result += FormatValue(dataList[i].UShort, FieldType.Ushort, setting);
                        break;
                    case TokenType.Int:
                        result += FormatValue(dataList[i].Int, FieldType.Int, setting);
                        break;
                    case TokenType.UInt:
                        result += FormatValue(dataList[i].UInt, FieldType.Uint, setting);
                        break;
                    case TokenType.Long:
                        result += FormatValue(dataList[i].Long, FieldType.Long, setting);
                        break;
                    case TokenType.ULong:
                        result += FormatValue(dataList[i].ULong, FieldType.Ulong, setting);
                        break;
                    case TokenType.Float:
                        result += FormatValue(dataList[i].Float, FieldType.Ulong, setting);
                        break;
                    case TokenType.Double:
                        result += FormatValue(dataList[i].Double, FieldType.Double, setting);
                        break;
                    case TokenType.String:
                        result += FormatValue(dataList[i].String, FieldType.String, setting);
                        break;
                    case TokenType.DataList:
                        result += FormatValue(dataList[i].DataList, FieldType.DataList, setting);
                        break;
                    case TokenType.DataDictionary:
                        result += FormatValue(dataList[i].DataDictionary, FieldType.Unknown, setting);
                        break;
                    case TokenType.Reference:
                        result += FormatValue(dataList[i].Reference, FieldType.Unknown, setting);
                        break;
                }
                result += "\n";
            }
            return result.Length > 0
                ? result.Remove(result.Length - 1)
                : result;
        }
 
        #endregion
    }
}