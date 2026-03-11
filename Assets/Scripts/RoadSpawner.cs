using UnityEngine;

public class RoadSpawner : MonoBehaviour
{

    [SerializeField] Transform spawnPoint;
    [SerializeField] RoadPiece roadPiece;
    [SerializeField] GameObject building;
    [SerializeField] GameObject tree;
   
    RoadPiece leadingPiece; // Remember the last piece created

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
            SpawnBuildings(leadingPiece); //spawn buildings over the leading piece
            SpawnTrees(leadingPiece);
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

    private void SpawnBuildings(RoadPiece roadPiece)
    {
        
        //For each building spawn point, instantiate a bulding
        foreach(Transform buildingSpawnpoint in roadPiece.GetBuildingSpawnPoint())
        {

            //spawn probability
            if (Random.value < 0.25f) return;//Get out of here if we get less than 25%


            //Building rotation
            int randomNumber = Random.Range(0, 3);
            Quaternion buildingRotation = Quaternion.Euler(0f, randomNumber * 90f, 0f);

            //Building scale
            Vector3 buildingScale = Vector3.one * Random.Range(0.5f, 0.75f);

            GameObject newBuilding = Instantiate(building, buildingSpawnpoint.position, buildingRotation, roadPiece.transform);

            //Apply building scale
            newBuilding.transform.localScale = buildingScale;
        }

    }
   
    private void SpawnTrees(RoadPiece roadpiece)
    {
        foreach(Transform treeSpawnPoint in roadpiece.GetTreeSpawnPoints())
        {
            if (Random.value < 0.25f) return;
            int randomNumber = Random.Range(0, 3);

            Quaternion treeRotation = Quaternion.Euler(0f, randomNumber * 360f, 0f);

            Vector3 treeScale = Vector3.one * Random.Range(0.5f, 0.75f);
            GameObject newTree = Instantiate(tree, treeSpawnPoint.position, treeRotation, roadpiece.transform);
            newTree.transform.localScale = treeScale;


        }
    }


}
