using UnityEngine;

public class TrapTrigger : MonoBehaviour
{
    public Transform respawnPoint;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.position = respawnPoint.position;
            Debug.Log("You hit a trap! Back to start.");
        }
    }
}
