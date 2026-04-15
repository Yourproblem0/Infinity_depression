using TMPro;
using UnityEngine;

public class InGameUI : MonoBehaviour
{
    [Header("UI elements")]
    //text objects
    [SerializeField] TMP_Text timeSurvivedTextObject;
    [SerializeField] TMP_Text speedTextObject;
    [SerializeField] TMP_Text endStatsTextObject;

    //screen
    [SerializeField] GameObject hub;
    [SerializeField] GameObject gameOverScreen;

    //Other stuff
    private LevelManager levelManager;

    private void Awake()
    {
        //Fetch level manager from the scene
        levelManager = FindAnyObjectByType<LevelManager>();

    }

    private void Start()
    {
        //Hidegame over screen
        gameOverScreen.SetActive(false);
    }

    private void Update()
    {
        if (levelManager.CurrentGameState == GameState.Running)
        {
           UpdateTimeText();
           UpdateTheSpeedText();
        }
        
    }

    private void UpdateTimeText()
    {
        timeSurvivedTextObject.text = $"Time survived:\n{levelManager.GameTime:F2}";
    }

    private void UpdateTheSpeedText()
    {
        speedTextObject.text = $"Speed:{levelManager.GetSpeed()}";
        if (levelManager.GetSpeed() > 7) speedTextObject.color = Color.yellow;
        if (levelManager.GetSpeed() > 10) speedTextObject.color = Color.red;
    }

    public void DisplayGameOverScreen()
    {
        gameOverScreen.SetActive(true);
        hub.SetActive(false);

        endStatsTextObject.text = $"Time survived:{levelManager.GameTime:F2}";
    }

}
