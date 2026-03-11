using UnityEngine;

public class RoadPiece : MonoBehaviour
{
    [SerializeField] Vector3 direction;
    [SerializeField] float moveSpeed = 5f;

    public Transform StartPos;
    public Transform EndPos;

    [SerializeField] Transform[] buildingSpawnPoints;
    [SerializeField] Transform[] treeSpawnPoints;

    //Moves road towards the player
    private void FixedUpdate()
    {
        //transform.Translate(direction * MoveSpeed * Time.deltaTime);
        transform.position += direction * moveSpeed * Time.fixedDeltaTime;
    }

    public Transform[] GetBuildingSpawnPoint()
    {
        return buildingSpawnPoints;
    }

    public Transform[] GetTreeSpawnPoints()
    {
        return treeSpawnPoints;
    }

}
