using UnityEngine;

[CreateAssetMenu(menuName = "Managers/GameManager", fileName = "GameManager")]
public class GameManagerSO : ScriptableObject
{
    public int collectedCoins;

    public void Reset()
    {
        //Reset settings
        collectedCoins = 0;
    }
}
