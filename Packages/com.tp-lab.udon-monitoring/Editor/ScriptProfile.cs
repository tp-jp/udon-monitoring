using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UdonSharp;

namespace TpLab.UdonMonitoring.Editor
{
    public class ScriptProfile
    {
        /// <summary>
        /// 対象スクリプト
        /// </summary>
        public UdonSharpBehaviour Script { get; }

        /// <summary>
        /// 対象フィールド一覧
        /// </summary>
        public FieldProfile[] Fields { get; }

        /// <summary>
        /// コンストラクタ。
        /// </summary>
        /// <param name="script">対象スクリプト</param>
        public ScriptProfile(UdonSharpBehaviour script)
        {
            Script = script;
            var fields = GetFields(script.GetType()).ToList();
            
            var baseType = script.GetType().BaseType;
            while (baseType != typeof(UdonSharpBehaviour))
            {
                fields.AddRange(GetFields(baseType));
                baseType = baseType.BaseType;
            }

            Fields = fields.ToArray();
        }

        /// <summary>
        /// 指定した型のフィールド一覧を取得する。
        /// </summary>
        /// <param name="type">型</param>
        /// <returns>フィールド一覧</returns>
        static IEnumerable<FieldProfile> GetFields(Type type)
        {
            return type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(x => !x.IsLiteral) // 定数除外
                .Where(x => !x.Name.Contains("<")) // プロパティ除外
                .Select(x => new FieldProfile(x));
        }
    }
}
