using UnityEngine;
[CreateAssetMenu(menuName = "DemoAssets/EnemySO", fileName = "EnemySO")]

public class DemoEnemySettingSO : ScriptableObject
{
    public enum weaponType
    {
        meele, ranged, magic
    }
   
    public string enemyName = "Reggie";
    public string enemyType = "Brainrotted";
    public float MaxHealth = 50f;
    public float KillReawrd = 25f;
    public float Speed = 10f;
    public float damage = 20f;

    public weaponType primaryWeapon = weaponType.meele;



}
