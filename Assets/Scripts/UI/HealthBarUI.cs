using UnityEngine;
using Garganta.Grid;
using Garganta.Units;

namespace Garganta.UI
{
    // World-space HP bar (bg + fg sprites) floating above each unit.
    public class HealthBarUI : MonoBehaviour
    {
        GameObject bg;
        GameObject fg;
        Unit unit;

        void Start()
        {
            unit = GetComponent<Unit>();
            bg = new GameObject("HPBg");
            bg.transform.SetParent(transform);
            bg.transform.localPosition = new Vector3(0, 0.55f, 0);
            var bsr = bg.AddComponent<SpriteRenderer>();
            bsr.sprite = GridVisualizer.WhiteSquare;
            bsr.color = Color.black;
            bg.transform.localScale = new Vector3(1f, 0.12f, 1f);
            bsr.sortingOrder = 20;

            fg = new GameObject("HPFg");
            fg.transform.SetParent(transform);
            fg.transform.localPosition = new Vector3(0, 0.55f, 0);
            var fsr = fg.AddComponent<SpriteRenderer>();
            fsr.sprite = GridVisualizer.WhiteSquare;
            fsr.color = Color.green;
            fsr.sortingOrder = 21;
        }

        void Update()
        {
            if (unit == null || fg == null) return;
            float f = (float)unit.HP / unit.Stats.MaxHP;
            fg.transform.localScale = new Vector3(Mathf.Max(0.001f, f), 0.08f, 1f);
            fg.transform.localPosition = new Vector3(-(1f - f) * 0.5f, 0.55f, 0);
            fg.GetComponent<SpriteRenderer>().color = f > 0.6f ? Color.green : f > 0.3f ? Color.yellow : Color.red;
        }
    }
}
