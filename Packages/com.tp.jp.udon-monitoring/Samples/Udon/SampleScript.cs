using UdonSharp;
using UnityEngine;
using VRC.SDK3.Data;

namespace TpLab.UdonMonitoring.Udon.Samples
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class SampleScript : UdonSharpBehaviour
    {
        [SerializeField]
        string nullString;

        [SerializeField]
        UdonSharpBehaviour nullScript;
        
        [SerializeField]
        int[] nullIntArray;

        [SerializeField]
        bool falseValue = false;
        
        [SerializeField]
        bool trueValue = true;
        
        [SerializeField]
        int intValue = 1234;

        [SerializeField]
        float floatValue = 1.234f;

        [SerializeField]
        string stringValue = "Test";

        [SerializeField]
        Vector2 vector2Value = new Vector2(1.2f, 3.4f);
        
        [SerializeField]
        Vector3 vector3Value = new Vector3(1.234f, 2.345f, 3.456f);
        
        [SerializeField]
        Quaternion quaternionValue = Quaternion.identity;

        [SerializeField]
        int[] intArray = { 1, 2, 3, 4 };

        [SerializeField]
        DataList dataListValue = new DataList()
        {
            1, 2, 3, 4
        };
    }
}
