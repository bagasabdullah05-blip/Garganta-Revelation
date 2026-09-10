using System.Collections;
using UnityEngine;
using Garganta.Core;
using Garganta.Grid;

namespace Garganta.UI
{
    // Floating damage numbers via pooled TextMesh objects (no font asset needed).
    public class CombatUI : MonoBehaviour
    {
        void Start()
        {
            var prefab = new GameObject("DmgNumPrefab");
            prefab.SetActive(false);
            var tm = prefab.AddComponent<TextMesh>();
            tm.fontSize = 48;
            tm.characterSize = 0.08f;
            tm.anchor = TextAnchor.MiddleCenter;
            var pool = FindAnyObjectByType<ObjectPool>();
            if (pool == null) pool = gameObject.AddComponent<ObjectPool>();
            pool.Register("dmg", prefab, 12);
        }

        public void SpawnText(Vector3 worldPos, string text, Color color)
        {
            var pool = ObjectPool.Instance;
            if (pool == null) return;
            var o = pool.Get("dmg", worldPos + new Vector3(0, 0.7f, 0));
            if (o == null) return;
            var tm = o.GetComponent<TextMesh>();
            tm.text = text;
            tm.color = color;
            StartCoroutine(FloatUp(o));
        }

        IEnumerator FloatUp(GameObject o)
        {
            float t = 0f;
            Vector3 start = o.transform.position;
            while (t < 1f)
            {
                t += Time.deltaTime;
                o.transform.position = start + new Vector3(0, t * 0.8f, 0);
                yield return null;
            }
            ObjectPool.Instance.Release("dmg", o);
        }
    }
}
