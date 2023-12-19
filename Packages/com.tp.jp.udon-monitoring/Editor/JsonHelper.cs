using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TpLab.UdonMonitoring.Editor
{
    public static class JsonHelper
    {
        /// <summary>
        /// 指定した配列やリストなどのコレクションを Root オブジェクトを持たない JSON 配列に変換します。
        /// </summary>
        public static string ToJson<T>(IEnumerable<T> collection)
        {
            var json = JsonUtility.ToJson(new DummyNode<T>(collection));
            var start = DummyNode<T>.RootName.Length + 4;
            var len = json.Length - start - 1;
            return json.Substring(start, len); // 追加ルートの文字を取り除いて返す
        }

        // 内部で使用するダミーのルート要素
        [Serializable]
        struct DummyNode<T>
        {
            // JSONに付与するダミールートの名称
            public const string RootName = nameof(array);

            // 疑似的な子要素
            public T[] array;

            // コレクション要素を指定してオブジェクトを作成する
            public DummyNode(IEnumerable<T> collection) => this.array = collection.ToArray();
        }
    }
}
