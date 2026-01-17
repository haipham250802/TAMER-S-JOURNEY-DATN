using System.Collections;
using System.Collections.Generic;
using UnityEditor;
#if UNITY_EDITOR
using Sirenix.OdinInspector.Editor;
#endif
using UnityEngine;

public class EditorPointSetup : MonoBehaviour
{

    public Vector3[] PathEditor;

    public Vector3[] GetPath()
    {
        Vector3[] tmp = new Vector3[PathEditor.Length];
        for (int i = 0; i < PathEditor.Length; i++)
        {
            tmp[i] = PathEditor[i] + transform.position;
        }
        return tmp;
    }

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        if (PathEditor.Length < 2) return;
        Gizmos.color = Color.red;
        for (int i = 0; i < PathEditor.Length - 1; i ++)
        {
            Gizmos.DrawLine(PathEditor[i] + transform.position, PathEditor[i + 1] + transform.position);
        }
    }
#endif
}

#if UNITY_EDITOR
[CustomEditor(typeof(EditorPointSetup)), CanEditMultipleObjects]
public class PhongNHEditor : OdinEditor
{

    protected GUIStyle style = new GUIStyle();

    protected string typeName;


    protected override void OnEnable()
    {
        style.normal.textColor = Color.white;
        typeName = nameof(EditorPointSetup);
    }

    protected void DrawEnemyInfo(EditorPointSetup _target)
    {
        DrawButtons(_target);
        if (!Application.isPlaying)
        {
            Vector3 pos = _target.transform.position;            
            for (int i = 0; i < _target.PathEditor.Length; i++)
            {
                _target.PathEditor[i] = Handles.PositionHandle(_target.PathEditor[i] + pos, Quaternion.identity) - pos;
            }
        }
    }

    protected void DrawButtons(EditorPointSetup _target)
    {
        Handles.BeginGUI();
        GUILayout.BeginVertical(new GUILayoutOption[] { GUILayout.Width(150) });

        GUILayout.EndVertical();
        Handles.EndGUI();
    }
    protected virtual void OnSceneGUI()
    {
        var _target = target as EditorPointSetup;
        if (_target == null)
        {
            return;
        }
        string info = $"{typeName} - {_target.name}";
        Undo.RecordObject(_target, info);

        DrawEnemyInfo(_target);
    }
}
#endif
