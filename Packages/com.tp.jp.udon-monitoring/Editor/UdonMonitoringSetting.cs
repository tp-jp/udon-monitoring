using UnityEngine;

namespace TpLab.UdonMonitoring.Editor
{
    // [CreateAssetMenu(fileName = "MonitoringSetting", menuName = "TpLab/UdonMonitoring/UdonMonitoringSetting")]
    public class UdonMonitoringSetting : ScriptableObject
    {
        [Header("General")]
        [SerializeField]
        [Tooltip("有効にするとオーナー情報が表示される")]
        bool isShowOwner = true;

        #region Formatting
        
        [Header("Formatting")]
        [SerializeField]
        [Tooltip("有効にするとメンバー変数名が変換される（_isOnLid => Is On Lid）")]
        bool isConvertNicifyNames = true;
        
        #endregion

        #region Color

        [Header("Color")]
        [SerializeField]
        Color nullColor = Color.red;

        [SerializeField]
        Color trueColor = Color.green;

        [SerializeField]
        Color falseColor = Color.red;

        [Header("Direction Color")]
        [SerializeField]
        Color xColor = new Color(0.41f, 0.38f, 1f);

        [SerializeField]
        Color yColor = new Color(0.49f, 1f, 0.53f);

        [SerializeField]
        Color zColor = new Color(1f, 0.38f, 0.35f);

        [SerializeField]
        Color wColor = new Color(0.6f, 0f, 1f);

        [Header("Active Line Color")]
        [SerializeField]
        Color activeColor = new Color(0f, 1f, 0f, 0.5f);

        [SerializeField]
        Color inactiveColor = new Color(1f, 0f, 0f, 0.5f);

        #endregion

        static UdonMonitoringSetting _instance;

        public static UdonMonitoringSetting Instance => _instance ?? (_instance = AssetRepository.LoadSetting<UdonMonitoringSetting>());

        /// <summary>
        /// 有効にするとメンバー変数名が変換される（_isOnLid => Is On Lid）
        /// </summary>
        public bool IsConvertNicifyNames => isConvertNicifyNames;
        
        /// <summary>
        /// 有効にするとオーナー情報が表示される
        /// </summary>
        public bool IsShowOwner => isShowOwner;
        
        /// <summary>
        /// NULLの色
        /// </summary>
        public Color NullColor => nullColor;

        /// <summary>
        /// TRUEの色
        /// </summary>
        public Color TrueColor => trueColor;
        
        /// <summary>
        /// FALSEの色
        /// </summary>
        public Color FalseColor => falseColor;
        
        /// <summary>
        /// Xの色
        /// </summary>
        public Color XColor => xColor;
        
        /// <summary>
        /// Yの色
        /// </summary>
        public Color YColor => yColor;
        
        /// <summary>
        /// Zの色
        /// </summary>
        public Color ZColor => zColor;
        
        /// <summary>
        /// Wの色
        /// </summary>
        public Color WColor => wColor;
        
        /// <summary>
        /// Activeの色
        /// </summary>
        public Color ActiveColor => activeColor;
        
        /// <summary>
        /// Inactiveの色
        /// </summary>
        public Color InactiveColor => inactiveColor;
    }
}
