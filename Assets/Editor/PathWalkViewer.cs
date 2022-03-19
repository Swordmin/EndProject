using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PathWalk))]
[CanEditMultipleObjects]
public class PathWalkViewer : Editor
{
    public override void OnInspectorGUI()
    {
        PathWalk myTarget = (PathWalk)target;

        base.OnInspectorGUI();
        serializedObject.Update();

        if (GUILayout.Button("Create points"))
            myTarget.CreatePoints();

        //EditorGUILayout.IntSlider(Convert.ToInt32(_health), 0, 100);
        //meTarget.MaxHealt = EditorGUI.IntSlider(GUILayoutUtility.GetRect(18, 18, "TextField"), "Max Health", Mathf.RoundToInt(meTarget.MaxHealth), 0, 100);
    }
}
