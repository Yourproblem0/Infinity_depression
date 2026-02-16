using UnityEngine;

public class RoadDestroyer : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        //destroy anything that touches me
        Destroy(other.gameObject);
    }

}
