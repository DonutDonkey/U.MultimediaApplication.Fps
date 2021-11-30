using System;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Random = System.Random;

namespace Script {
    public class IdAttribute : PropertyAttribute { }
    
#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(IdAttribute))]
    public class IdAttributeDrawer : PropertyDrawer {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            GUI.enabled = false;

            Random rnd = new Random();
            string mono_id = $"{DateTime.Now}:MONO";
            string rand = new string(mono_id.ToCharArray().
                OrderBy(s => (rnd.Next(2) % 2) == 0).ToArray());
            
            if (string.IsNullOrEmpty(property.stringValue))
                property.stringValue = rand;
            
            GUI.enabled = true;
        }
    }
#endif
    
    public class UniqueScriptableObject : ScriptableObject { [IdAttribute] public string id; }
    public class UniqueMonoBehaviour : MonoBehaviour { [IdAttribute] public string id; }
}