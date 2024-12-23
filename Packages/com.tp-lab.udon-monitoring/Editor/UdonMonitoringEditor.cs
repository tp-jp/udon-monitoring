using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace TpLab.UdonMonitoring.Editor
{
    [CustomEditor(typeof(Udon.UdonMonitoring))]
    public class UdonMonitoringEditor : UnityEditor.Editor
    {
        Udon.UdonMonitoring _target;

        void OnEnable()
        {
            _target = _target ? _target : target as Udon.UdonMonitoring;
        }

        /// <summary>
        /// Udonスクリプトをセットアップする。
        /// </summary>
        public void SetupUdonScript()
        {
            var sideViewContent = _target.transform.Find("Canvas/SideView/Viewport/Content");
            var sideViewTemplate = sideViewContent.Find("Template");

            var detailViewContent = _target.transform.Find("Canvas/DetailView/ScrollView/Viewport/Content");
            var detailViewTemplate = detailViewContent.Find("Template");
            var detailTitle = _target.transform.Find("Canvas/DetailView/TitleView/Title").GetComponent<TextMeshProUGUI>();

            var detailViews = new List<GameObject>();
            var detailButtons = new List<Button>();
            var profiles = _target.TargetScripts
                .Where(x => x != null)
                .Select(x => new ScriptProfile(x))
                .ToArray();
            var setting = UdonMonitoringSetting.Instance;
            var settingMetadata = new SettingMetadata()
            {
                nullColor = ColorUtility.ToHtmlStringRGB(setting.NullColor),
                trueColor = ColorUtility.ToHtmlStringRGB(setting.TrueColor),
                falseColor = ColorUtility.ToHtmlStringRGB(setting.FalseColor),
                xColor = ColorUtility.ToHtmlStringRGB(setting.XColor),
                yColor = ColorUtility.ToHtmlStringRGB(setting.YColor),
                zColor = ColorUtility.ToHtmlStringRGB(setting.ZColor),
                wColor = ColorUtility.ToHtmlStringRGB(setting.WColor),
            };
            foreach (var profile in profiles)
            {
                // SideView
                var sideView = Instantiate(sideViewTemplate.gameObject, sideViewContent, false);
                sideView.SetActive(true);
                var udonDetailButton = sideView.transform.GetComponent<Udon.UdonDetailButton>();
                var objectName = sideView.transform.Find("Button/ObjectName").GetComponent<TextMeshProUGUI>();
                var activeLine = sideView.transform.Find("Button/ActiveLine").GetComponent<Image>();
                var button = sideView.transform.Find("Button").GetComponent<Button>();
                detailButtons.Add(button);
                udonDetailButton.SetFieldValue("objectName", objectName);
                udonDetailButton.SetFieldValue("activeLine", activeLine);
                udonDetailButton.SetFieldValue("targetScript", profile.Script);
                udonDetailButton.SetFieldValue("activeLineColor", setting.ActiveLineColor);
                udonDetailButton.SetFieldValue("inactiveLineColor", setting.InactiveLineColor);

                // DetailView
                var detailView = Instantiate(detailViewTemplate.gameObject, detailViewContent, false);
                var ownerValue = detailView.transform.Find("Owner/Value").GetComponent<TextMeshProUGUI>();
                var variableTemplate = detailView.transform.Find("Template");
                detailViews.Add(detailView);

                // Variables
                var variableNames = new List<TextMeshProUGUI>();
                var variableValues = new List<TextMeshProUGUI>();
                foreach (var field in profile.Fields)
                {
                    var variableRecord = Instantiate(variableTemplate.gameObject, detailView.transform, false);
                    variableRecord.SetActive(true);
                    variableNames.Add(variableRecord.transform.Find("Name").GetComponent<TextMeshProUGUI>());
                    variableValues.Add(variableRecord.transform.Find("Value").GetComponent<TextMeshProUGUI>());
                }

                // UdonDetailView
                var udonDetailView = detailView.transform.GetComponent<Udon.UdonDetailView>();
                var ownerRecord = detailView.transform.Find("Owner").gameObject;
                udonDetailView.SetFieldValue("detailTitle", detailTitle);
                udonDetailView.SetFieldValue("variableNames", variableNames.ToArray());
                udonDetailView.SetFieldValue("variableValues", variableValues.ToArray());
                udonDetailView.SetFieldValue("ownerRecord", ownerRecord);
                udonDetailView.SetFieldValue("ownerValue", ownerValue);
                udonDetailView.SetFieldValue("isShowOwner", setting.IsShowOwner);
                udonDetailView.SetFieldValue("targetScript", profile.Script);
                udonDetailView.SetFieldValue("fieldMetadataJson", JsonConvert.SerializeObject(profile.Fields.Select(x => x.GetMetadata())));
                udonDetailView.SetFieldValue("settingMetadataJson", JsonConvert.SerializeObject(settingMetadata));
            }

            _target.SetFieldValue("detailViews", detailViews.ToArray());
            _target.SetFieldValue("detailButtons", detailButtons.ToArray());
        }
    }
}