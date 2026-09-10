using System.Collections.Generic;
using UnityEngine;

namespace Garganta.Core
{
    // Object Pool pattern: reused for damage numbers (M1) and projectiles (M2+).
    public class ObjectPool : MonoBehaviour
    {
        public static ObjectPool Instance { get; private set; }
        readonly Dictionary<string, Queue<GameObject>> pools = new Dictionary<string, Queue<GameObject>>();
        readonly Dictionary<string, GameObject> prefabs = new Dictionary<string, GameObject>();

        void Awake()
        {
            if (Instance != null && Instance != this) { Destroy(gameObject); return; }
            Instance = this;
        }

        public void Register(string key, GameObject prefab, int size)
        {
            if (pools.ContainsKey(key)) return;
            prefabs[key] = prefab;
            var q = new Queue<GameObject>();
            for (int i = 0; i < size; i++)
            {
                var o = Instantiate(prefab, transform);
                o.SetActive(false);
                q.Enqueue(o);
            }
            pools[key] = q;
        }

        public GameObject Get(string key, Vector3 pos)
        {
            if (!pools.ContainsKey(key)) return null;
            GameObject o = pools[key].Count > 0 ? pools[key].Dequeue() : Instantiate(prefabs[key], transform);
            o.transform.position = pos;
            o.SetActive(true);
            return o;
        }

        public void Release(string key, GameObject o)
        {
            o.SetActive(false);
            if (pools.ContainsKey(key)) pools[key].Enqueue(o);
            else Destroy(o);
        }
    }
}
