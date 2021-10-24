using System;
using UnityEditor;
using UnityEngine;

namespace Script {
    public class IdAttribute : PropertyAttribute { }
    
#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(IdAttribute))]
    public class IdAttributeDrawer : PropertyDrawer {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            GUI.enabled = false;

            if (string.IsNullOrEmpty(property.stringValue))
                property.stringValue = $"{Guid.NewGuid()}";
            
            GUI.enabled = true;
        }
    }
#endif
    
    public class UniqueScriptableObject : ScriptableObject { [IdAttribute] public string id; }
    public class UniqueMonoBehaviour : MonoBehaviour { [IdAttribute] public string id; }
}