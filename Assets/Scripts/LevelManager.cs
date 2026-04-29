using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] float currentGameSpeed = 5f;

    private InGameUI inGameUI;

    public float GameTime { get; set; }

    public int Coins { get; private set; } //Only level manager can change how many coins, anyone can ask for the information tho

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

    public void AddCoin()
    {
        //Increment coins
        Coins++;

        //update UI
        inGameUI.UpdateCoins();
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

    public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }//67

    public void QuitGame()
    {
        //Tell Unity to use only Unity quit when unity and other when outside of unity
#if UNITY_EDITOR
        //Can only work in unity
        UnityEditor.EditorApplication.isPlaying = false;
#else
        //Can use in applications
        Application.Quit();
#endif
    }

}
