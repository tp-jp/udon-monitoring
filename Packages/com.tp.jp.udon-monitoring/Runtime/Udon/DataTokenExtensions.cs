using System;
using VRC.SDK3.Data;

namespace TpLab.UdonMonitoring.Udon
{
    public static class DataTokenExtensions
    {
        /// <summary>
        /// データトークンから数値を取得する。
        /// </summary>
        /// <typeparam name="T">データ型</typeparam>
        /// <param name="self">データトークン</param>
        /// <returns>数値</returns>
        public static T GetNumber<T>(this DataToken self)
            where T : struct, IComparable, IFormattable, IConvertible, IComparable<T>, IEquatable<T>
        {
            return (T)(object)self.Number;
        }
    }
}
