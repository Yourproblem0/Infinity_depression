using UnityEngine;

public class Coin : MonoBehaviour
{

    LevelManager levelManager;

    private void Awake()
    {
        levelManager = FindAnyObjectByType<LevelManager>();
    }

    private void FixedUpdate()
    {
        if (levelManager.CurrentGameState != GameState.Running)
            return;
        transform.position += Vector3.back * levelManager.GetSpeed() * Time.fixedDeltaTime;
    }

}
