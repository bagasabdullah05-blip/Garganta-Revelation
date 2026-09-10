using UnityEngine;
using Garganta.Grid;

namespace Garganta.Core
{
    // Manual isometric camera (WASD/arrows + wheel zoom + drag pan). No Cinemachine in M1.
    [RequireComponent(typeof(Camera))]
    public class IsometricCamera : MonoBehaviour
    {
        public float PanSpeed = 8f;
        public float MinZoom = 3f;
        public float MaxZoom = 14f;

        Camera cam;
        Vector3 dragOrigin;
        bool dragging;

        void Awake() => cam = GetComponent<Camera>();

        void Update()
        {
            float hx = Input.GetAxisRaw("Horizontal");
            float hy = Input.GetAxisRaw("Vertical");
            transform.position += new Vector3(hx, hy, 0) * PanSpeed * Time.deltaTime;

            float wheel = Input.mouseScrollDelta.y;
            if (Mathf.Abs(wheel) > 0.01f && cam.orthographic)
                cam.orthographicSize = Mathf.Clamp(cam.orthographicSize - wheel, MinZoom, MaxZoom);

            if (Input.GetMouseButtonDown(2)) { dragOrigin = Input.mousePosition; dragging = true; }
            if (Input.GetMouseButtonUp(2)) dragging = false;
            if (dragging)
            {
                Vector3 d = Input.mousePosition - dragOrigin;
                transform.position -= new Vector3(d.x, d.y, 0) * 0.01f;
                dragOrigin = Input.mousePosition;
            }
        }
    }
}
