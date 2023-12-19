using System.Reflection;
using UnityEditor;

namespace TpLab.UdonMonitoring.Editor
{
    [CustomEditor(typeof(UdonMonitoringSetting))]
    public class UdonMonitoringSettingEditor : UnityEditor.Editor
    {
        SerializedProperty _isShowOwner;
        SerializedProperty _isConvertNicifyNames;
        SerializedProperty _nullColor;
        SerializedProperty _trueColor;
        SerializedProperty _falseColor;
        SerializedProperty _xColor;
        SerializedProperty _yColor;
        SerializedProperty _zColor;
        SerializedProperty _wColor;
        SerializedProperty _activeColor;
        SerializedProperty _inactiveColor;
        
        void OnEnable()
        {
            PopulateSerializedProperties();
        }
        
        void PopulateSerializedProperties()
        {
            var fields = GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (var fieldInfo in fields)
            {
                if (fieldInfo.FieldType == typeof(SerializedProperty))
                {
                    fieldInfo.SetValue(this, serializedObject.FindProperty(fieldInfo.Name.Remove(0, 1)));
                }
            }
        }
        
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(_isShowOwner);
            EditorGUILayout.PropertyField(_isConvertNicifyNames);
            EditorGUILayout.PropertyField(_nullColor);
            EditorGUILayout.PropertyField(_trueColor);
            EditorGUILayout.PropertyField(_falseColor);
            EditorGUILayout.PropertyField(_xColor);
            EditorGUILayout.PropertyField(_yColor);
            EditorGUILayout.PropertyField(_zColor);
            EditorGUILayout.PropertyField(_wColor);
            EditorGUILayout.PropertyField(_activeColor);
            EditorGUILayout.PropertyField(_inactiveColor);

            serializedObject.ApplyModifiedProperties();
        }
    }
}