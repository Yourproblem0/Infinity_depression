using UnityEngine;

public class RoadSpawner : MonoBehaviour
{

    [SerializeField] Transform spawnPoint;
    [SerializeField] RoadPiece roadPiece;
   
    RoadPiece leadingPiece;

    private void Start()
    {
        leadingPiece = SpawnStartAt(spawnPoint.position);
        //RoadPiece backPiece = leadingPiece;
        //for (int i=0; i<8; i++)
        //{
        //    backPiece = SpawnEndAt(backPiece.StartPos.position);
        //}
    }

    public void Update()
    {
        if (leadingPiece == null) return;
        if (leadingPiece.EndPos.position.z <= spawnPoint.position.z)
        {
            leadingPiece = SpawnStartAt(leadingPiece.EndPos.position);
        }
    }
    RoadPiece SpawnStartAt(Vector3 startPosition)
    {
        RoadPiece newPiece = Instantiate(roadPiece, startPosition, spawnPoint.rotation);
        newPiece.transform.position += startPosition - newPiece.StartPos.position;
        return newPiece;
    }
    RoadPiece SpawnEndAt(Vector3 endPosition)
    {
        RoadPiece newPiece = Instantiate(roadPiece, endPosition, spawnPoint.rotation);
        newPiece.transform.position += endPosition - newPiece.EndPos.position;
        return newPiece;
    }
   
}
