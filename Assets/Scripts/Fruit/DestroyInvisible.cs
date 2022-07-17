using UnityEngine;

namespace Fruit
{
    public class DestroyInvisible : MonoBehaviour
    {
        private void OnBecameInvisible()
        {
            Destroy(gameObject);
        }
    }
}
