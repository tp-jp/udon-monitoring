using System.Linq;
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
            Fields = script.GetFields()
                .Select(x => new FieldProfile(x))
                .ToArray();
        }
    }
}
