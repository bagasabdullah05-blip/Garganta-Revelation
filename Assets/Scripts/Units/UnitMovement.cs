using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Garganta.Grid;

namespace Garganta.Units
{
    public class UnitMovement : MonoBehaviour
    {
        public float StepTime = 0.15f;

        public IEnumerator FollowPath(List<Vector2Int> path, GridManager grid, Action onDone)
        {
            for (int i = 1; i < path.Count; i++)
            {
                Vector3 from = transform.position;
                Vector3 to = grid.CoordToWorld(path[i]);
                float t = 0f;
                while (t < 1f)
                {
                    t += Time.deltaTime / StepTime;
                    transform.position = Vector3.Lerp(from, to, Mathf.Min(1f, t));
                    yield return null;
                }
            }
            onDone?.Invoke();
        }
    }
}
