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
            var anim = GetComponent<Garganta.Art.UnitAnimator>();
            if (anim != null) anim.PlayWalk(true);
            for (int i = 1; i < path.Count; i++)
            {
                Vector3 from = transform.position;
                Vector3 to = grid.TileTop(path[i]);
                if (anim != null) anim.SetFacing(to.x - from.x);
                float t = 0f;
                while (t < 1f)
                {
                    t += Time.deltaTime / StepTime;
                    transform.position = Vector3.Lerp(from, to, Mathf.Min(1f, t));
                    yield return null;
                }
            }
            if (anim != null) anim.PlayWalk(false);
            onDone?.Invoke();
        }
    }
}
