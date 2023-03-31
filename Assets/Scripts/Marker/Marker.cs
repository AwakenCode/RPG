using UnityEngine;

namespace Marker
{
    public class Marker : MonoBehaviour
    {
        private static readonly Color _color = UnityEngine.Color.magenta;
        [field: SerializeField] public Color32 Color { get; private set; } = _color;
        [field: SerializeField] public float Size { get; private set; } = 0.3f;
    }
}