using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] float currentGameSpeed = 5f;

    private InGameUI inGameUI;

    public float GameTime { get; set; }

    //public property
    public GameState CurrentGameState => currentGameState;
    //hidden private field
    [SerializeField] GameState currentGameState;

    private void Awake()
    {
        inGameUI = FindFirstObjectByType<InGameUI>(); 
    }

    private void Start()
    {
        StartCoroutine(GraduallySpeedUpTime());
    }

    private void Update()
    {
        //increment game time
        GameTime += Time.deltaTime;
    }

    IEnumerator GraduallySpeedUpTime()
    {
        while (true)
        {
           //Wait a bit
           yield return new WaitForSeconds(3f);
           currentGameSpeed += 0.25f;

        }
       

    }
    public float GetSpeed()
    {
        return currentGameSpeed;
    }

    internal void CoinCollected()
    {
        throw new NotImplementedException();
    }

    public void DeclareGameOver()
    {
        currentGameState = GameState.Gameover;
        inGameUI.DisplayGameOverScreen();
    }

}
