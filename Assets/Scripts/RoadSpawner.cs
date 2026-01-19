using UnityEngine;

public class RoadSpawner : MonoBehaviour
{

    [SerializeField] Transform spawnPoint;
    [SerializeField] GameObject roadpiece;

    private void Start()
    {
        SpawnPiece(); //call spawnpiece
    }

    public void SpawnPiece()
    {
        Instantiate(roadpiece, spawnPoint.position, Quaternion.identity);
    }
   
}
