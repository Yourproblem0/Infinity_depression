using UnityEngine;

public class Spawnzone : MonoBehaviour
{
    //reference to road spawner, so we can talk to it
    [SerializeField] RoadSpawner spawner;

    private void OnTriggerExit(Collider other)
    {
        //Sense when something has left the collider zone
        //And tell the spawner to create a new piece
        spawner.SpawnPiece();

    }
}
