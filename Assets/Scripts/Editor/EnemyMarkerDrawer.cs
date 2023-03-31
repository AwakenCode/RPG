using UnityEditor;
using UnityEngine;
using Mark = Marker.Marker;

namespace Editor
{
    [CustomEditor(typeof(Mark))]
    public class EnemyMarkerDrawer : UnityEditor.Editor
    {
        [DrawGizmo(GizmoType.NonSelected | GizmoType.Active | GizmoType.Pickable)]
        public static void DrawGizmo(Mark mark, GizmoType type)
        {
            Gizmos.color = mark.Color;  
            Gizmos.DrawSphere(mark.transform.position, mark.Size);
        }
    }
}