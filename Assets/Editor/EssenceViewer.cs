using UnityEngine;
using UnityEditor;
using UnityEditor.UIElements;
using System;

[CustomEditor(typeof(Enemy))]
[CanEditMultipleObjects]
public class EssenceViewer : Editor
{

    public override void OnInspectorGUI()
    {
        Enemy myTarget = (Enemy)target;

        base.OnInspectorGUI();
        serializedObject.Update();

        ProgressBar(myTarget.CurrentHealth / myTarget.MaxHealth, $"Health {myTarget.CurrentHealth} / {myTarget.MaxHealth}");

        if (GUILayout.Button("Init Parameters"))
            myTarget.UpdateParameters();

        //EditorGUILayout.IntSlider(Convert.ToInt32(_health), 0, 100);
        //meTarget.MaxHealt = EditorGUI.IntSlider(GUILayoutUtility.GetRect(18, 18, "TextField"), "Max Health", Mathf.RoundToInt(meTarget.MaxHealth), 0, 100);
    }

    private void ProgressBar(float value, string label)
    {
        EditorGUILayout.Space(10);
        Rect rect = GUILayoutUtility.GetRect(18, 18, "TextField");
        EditorGUI.ProgressBar(rect, value, label);
        EditorGUILayout.Space();
    }

}
