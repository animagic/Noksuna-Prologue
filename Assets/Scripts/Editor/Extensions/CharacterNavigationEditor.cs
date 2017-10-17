using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

[CustomEditor(typeof(CharacterNavigation))]
public class CharacterNavigationEditor : Editor {

    static bool visualizePath = false;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        visualizePath = EditorGUILayout.Toggle("Visualize Path", visualizePath);
    }

    [DrawGizmo(GizmoType.Selected | GizmoType.NonSelected)]
    static void DrawLinesForNavigation(NavMeshAgent agent, GizmoType gizmoType)
    {
        if (!visualizePath || agent.path == null)
            return;
        if(agent.path == null)
            return;
        Gizmos.color = Color.red;

        List<Vector3> agentPathlines = new List<Vector3>();
        agentPathlines = agent.path.corners.ToList();

        for(int i = 0; i < agentPathlines.Count - 1; i++)
        {
            Gizmos.DrawLine(agentPathlines[i], agentPathlines[i + 1]);
        }

    }
}
