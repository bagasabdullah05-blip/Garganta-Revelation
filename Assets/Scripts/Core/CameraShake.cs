using UnityEngine;

namespace Garganta.Core
{
    public class CameraShake : MonoBehaviour
    {
        float trauma;
        Vector3 basePos;

        void LateUpdate()
        {
            if (trauma > 0f)
            {
                trauma = Mathf.Max(0f, trauma - Time.deltaTime * 1.5f);
                transform.position = basePos + (Vector3)Random.insideUnitCircle * trauma * 0.4f;
            }
            else basePos = transform.position;
        }

        public void AddShake(float amount) => trauma = Mathf.Min(1f, trauma + amount);
    }
}
