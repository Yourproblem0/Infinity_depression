using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // Get access to the game manager
    [SerializeField] GameManagerSO gameManager;
    private UIManager uiManager;

    private void Awake()
    {
        uiManager = FindFirstObjectByType<UIManager>();
    }

    private void Start()
    {
        //reset settings
        gameManager.Reset();
    }

    public void CoinCollected()
    {
        //Player has collected a coin, let's inform game manager and the ui manager
        gameManager.collectedCoins += 1;
        uiManager.UpdateCoinsText(gameManager.collectedCoins);
    }
}
