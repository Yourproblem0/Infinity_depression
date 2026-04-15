using UnityEngine;
using System.Collections;

public class RoadSpawner : MonoBehaviour
{

    [SerializeField] Transform spawnPoint;
    [SerializeField] RoadPiece[] roadPieces;
    private int previousRoadPieceIdx;
    [SerializeField] GameObject building;
    [SerializeField] GameObject tree;
    [SerializeField] Obstacle[] obstacles;
   
    RoadPiece leadingPiece; // Remember the last piece created

    LevelManager levelmanager;

    private void Awake()
    {
        levelmanager = FindFirstObjectByType<LevelManager>();
    }

    private void Start()
    {

        leadingPiece = SpawnStartAt(spawnPoint.position);
        RoadPiece backPiece = leadingPiece;
        for (int i = 0; i < 8; i++)
        {
            backPiece = SpawnEndAt(backPiece.StartPos.position);
        }

        StartCoroutine(SpawnObstacles());

    }

    IEnumerator SpawnObstacles()
    {
        //Only spawn obstagles WHILE the current game state IS set on running
        while (levelmanager.CurrentGameState==GameState.Running)
        {
            if (leadingPiece == null)
            {
                //Do nothing this turn
                yield return null;
                continue;
            }
            //Choose random lane
            var lanes = leadingPiece.GetLanes();
            int randomLaneIndex = Random.Range(0, lanes.Length);
            Transform chosenLane = leadingPiece.GetLanes()[randomLaneIndex];
                //lanes[randomLaneIndex];

            //Choose random obstacle
            int obstacleIdx = Random.Range(0, obstacles.Length);

            //Instatiate an obstacle out of the array usinf the index
            Instantiate(obstacles[obstacleIdx], chosenLane.position, Quaternion.identity);

            //Wait until respawn
            yield return new WaitForSeconds(2f);
        }
    }

    public void Update()
    {
        if (levelmanager.CurrentGameState != GameState.Running)
            return;

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
        int roadPieceIdx = 0; //Assume we ude index zero but...
        //If we previously already had a clean bit of road
        //And we have to wait for 3 sec
        if (previousRoadPieceIdx == 0 && Time.time>3f)
        {
            //Then choose a random road piece
            roadPieceIdx = Random.Range(0, roadPieces.Length);
            
        }
        //Update previous piece index
        previousRoadPieceIdx = roadPieceIdx;

        

        RoadPiece newPiece = Instantiate(roadPieces[roadPieceIdx], startPosition, spawnPoint.rotation);
        newPiece.transform.position += startPosition - newPiece.StartPos.position;
        return newPiece;
    }
    RoadPiece SpawnEndAt(Vector3 endPosition)
    {
        int roadPieceIdx = 0;

        roadPieceIdx = 0; //Assume we ude index zero but...
        //If we previously already had a clean bit of road
        if (previousRoadPieceIdx == 0 && Time.time>3f)
        {
            //Then choose a random road piece
            roadPieceIdx = Random.Range(0, roadPieces.Length);
            
        }
        
        previousRoadPieceIdx = roadPieceIdx;

        RoadPiece newPiece = Instantiate(roadPieces[roadPieceIdx], endPosition, spawnPoint.rotation);
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
