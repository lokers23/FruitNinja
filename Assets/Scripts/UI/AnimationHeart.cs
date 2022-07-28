using UnityEngine;

public class AnimationHeart : MonoBehaviour
{
    [SerializeField]public float max;
    [SerializeField] private float speed = 0.15f;
    
    private float _min;

    private void Awake()
    {
        _min = transform.localScale.x;
    }

    private void Update()
    {
        transform.localScale = new Vector3(Mathf.PingPong(Time.time * speed, (max-_min)) + max, Mathf.PingPong(Time.time * speed, max - _min) + max, 0);
    }
}
