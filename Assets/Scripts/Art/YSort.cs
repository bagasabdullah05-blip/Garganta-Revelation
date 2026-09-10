using UnityEngine;

namespace Garganta.Art
{
    // Depth sort for 2D: lower on screen (smaller y) draws on top.
    public class YSort : MonoBehaviour
    {
        public int BaseOrder = 100;
        public int Offset;
        SpriteRenderer sr;

        void Awake() => sr = GetComponent<SpriteRenderer>();

        void LateUpdate()
        {
            if (sr != null) sr.sortingOrder = BaseOrder + Offset - (int)(transform.position.y * 10f);
        }
    }
}
