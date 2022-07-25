using UnityEngine;

namespace Blot
{
    public class BlotController : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private float timeLife = 3f;
        [SerializeField] private float speedDisappearance = 0.2f;

        private void Start()
        {
            Destroy(gameObject, timeLife);
        }

        private void Update()
        {
            var color =  spriteRenderer.color;
            color.a -= speedDisappearance * Time.deltaTime;
            spriteRenderer.color = color;
        }
    }
}
