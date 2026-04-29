using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{

    [Header("Settings")]

    [SerializeField] Transform coinSpawnPoint;
    [SerializeField] Coin coin;
    [SerializeField] float spawnDelay = 0.25f;
    [SerializeField] float lookAheadDistance = 5f;

    [SerializeField] RoadPiece referenceRoadPiece;
    private Transform[] lanes;
    private int currentLaneIndex;

    private LevelManager levelManager;

    private void Awake()
    {
        levelManager = FindAnyObjectByType<LevelManager>();
    }

    private void Start()
    {
        //Get lanes from the reference road piece
        lanes = referenceRoadPiece.GetLanes();
        //first lane to start
        currentLaneIndex = 1;
        //Start coroutines
        StartCoroutine(SpawnCoins());
        StartCoroutine(LookAhead());
        StartCoroutine(HopBetweenLanes());

    }

    IEnumerator LookAhead()
    {
        while (levelManager.CurrentGameState == GameState.Running)
        {
            yield return new WaitForSeconds(0.016f);
            Debug.DrawRay(coinSpawnPoint.position, coinSpawnPoint.forward * lookAheadDistance, Color.red);

            //Is a lane available? If not, switch lanes.
            if (!IsLaneAvailable(currentLaneIndex))
            {
                TrytoChangeLane();
            }


        }
    }

    private bool IsLaneAvailable(int laneIndex)
    {
        //test position (where to fire the raycast from)
        Vector3 testPos = new Vector3(lanes[laneIndex].position.x, coinSpawnPoint.position.y, coinSpawnPoint.position.z);

        if(Physics.Raycast(testPos, coinSpawnPoint.forward,out RaycastHit hit, lookAheadDistance))
        {
            //We hit something, lane is not available
            Debug.Log($"I hit a {hit.collider.gameObject.name}!");
            return false;
        }

        //Yes, it is available
        return true;
    }

    IEnumerator HopBetweenLanes()
    {
        while (levelManager.CurrentGameState == GameState.Running)
        {
            yield return StartCoroutine(WaitForRandomRange(2f, 5f));
            TrytoChangeLane();
        }
    }

    IEnumerator WaitForRandomRange(float minTime, float maxTime)
    {
        float timeToWait = Random.Range(minTime, maxTime);
        yield return new WaitForSeconds(timeToWait);
    }


    private void TrytoChangeLane()
    {
        List<int> possibleLaneIndices = new List<int>();

        int LeftLane = currentLaneIndex - 1;
        int RightLane= currentLaneIndex + 1;

        //See if left lane is valid and available
        if (LeftLane >= 0 && LeftLane < lanes.Length)
        {
            if (IsLaneAvailable(LeftLane))
            {
                //YEs, we can go left, but remember... At that crossroads, don't turn left          King in yellow:"Please turn left dawg"
                possibleLaneIndices.Add(LeftLane);
            }
        }
        //Let's see if we can go right as well
        if (RightLane >= 0 && RightLane < lanes.Length)
        {

            //Yes, we can turn right... At the crossroads... keep moving forward
            possibleLaneIndices.Add(RightLane);

        }

        //No lanes are available
        if (possibleLaneIndices.Count == 0)
        {
            Debug.Log("I cannot do this shit no more, you are on your own");
            return;
        }

        //We have at least one possibility
        int chosenLane = possibleLaneIndices[Random.Range(0, possibleLaneIndices.Count)];

        UpdateSpawnerPosition(chosenLane);
    }

    private void UpdateSpawnerPosition(int laneIndex)
    {
        //Move spawner sideways
        coinSpawnPoint.position = new Vector3(lanes[laneIndex].position.x, coinSpawnPoint.position.y, coinSpawnPoint.position.z);

        //Update lane index
        currentLaneIndex = laneIndex;
    }


    IEnumerator SpawnCoins()
    {
        while (levelManager.CurrentGameState == GameState.Running)
        { 
           yield return new WaitForSeconds(spawnDelay);
           Instantiate(coin, coinSpawnPoint.position,Quaternion.identity);

        }
        
    }



}
