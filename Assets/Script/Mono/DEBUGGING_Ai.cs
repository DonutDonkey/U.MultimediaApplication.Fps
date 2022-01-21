using Script.Mono.AI;
using UnityEngine;

public class DEBUGGING_Ai : MonoBehaviour {
    [SerializeField] private Vector3 height_offset = Vector3.up;
    private AI_Brain[] brains;
    
    [SerializeField] private Color color = Color.green;
    [SerializeField] private GUIStyle style;

    private void Start() => style = new GUIStyle();

    private void OnDrawGizmos() {
        style.normal.textColor = color;
        
        if (brains == null)
            brains = FindObjectsOfType<AI_Brain>();

        foreach (var b in brains) {
            string text = b.name.ToString() + 
                          "\n" + b.ToString() + 
                          "\n" + b.ACurrentPoi?.ToString();
            UnityEditor.Handles.Label(b.transform.position + height_offset, text, style);
        }
    }
}
