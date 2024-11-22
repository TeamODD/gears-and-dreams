using UnityEngine;

namespace GearsAndDreams.Casting
{
    public class TargetLine : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer lineRenderer;

        public void SetHeight(float height)
        {
            transform.localPosition = new Vector3(
                transform.localPosition.x,
                height,
                transform.localPosition.z);
        }

        public void SetActive(bool active)
        {
            gameObject.SetActive(active);
        }
    }
}