using UnityEngine;

namespace Spawner
{
    public class ObjectOutBorder : MonoBehaviour
    {
        [SerializeField] private Camera mainCamera;
        [SerializeField] private bool isAxisX;
        [SerializeField] private bool isAxisY;
        
        private const float DistanceObjBorder = 1f;
        
        private void OnEnable()
        {
            PushingObjectOutCameraBorder();
        }
        
        private void PushingObjectOutCameraBorder()
        {
            Vector2 leftBottomCorner = mainCamera.ViewportToWorldPoint (new Vector2 (0,0));
            Vector2 rightTopCorner = mainCamera.ViewportToWorldPoint (new Vector2 (1,1));
            
            var position = transform.position;
            var x = position.x;
            var y = position.y;

            if (isAxisX)
            {
                PushingOutAlongX(x, y, leftBottomCorner, rightTopCorner);
            }
            else if(isAxisY)
            {
                PushingOutAlongY(x, y, leftBottomCorner);
            }
        }

        private void PushingOutAlongX(float x, float y, Vector2 bottomCorner, Vector2 rightTopCorner)
        {
            if (rightTopCorner.x / 2 < x)
            {
                transform.position = new Vector2(rightTopCorner.x + DistanceObjBorder, y);
            }
            else
            {
                transform.position = new Vector2(bottomCorner.x - DistanceObjBorder, y);
            }
        }
        
        private void PushingOutAlongY(float x, float y, Vector2 bottomCorner)
        {
            if (bottomCorner.y < y)
            {
                transform.position = new Vector2(x, bottomCorner.y - 1f);
            }
        }
    }
}
