using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    public float timer = 0.0f;
    public float directionChangeDelay = 2.0f;
    public Vector3 movement;

    void Start()
    {
        
    }

    void Update()
    {
	   timer += Time.deltaTime;
        if (timer > directionChangeDelay)
        {
            movement *= -1.0f;
            timer = 0.0f;
        }
        transform.position += Time.deltaTime * movement;
    }
}
