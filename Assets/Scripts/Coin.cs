using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] Transform coinObject;

    LevelManager levelManager;

    private float amplityde = 0.3f; //wave height
    private float frequency = 2f; // wave density
    private Vector3 startPos;

    private void Awake()
    {
        levelManager = FindAnyObjectByType<LevelManager>();
    }
    private void Start()
    {
        //give inital spin
        coinObject.Rotate(new Vector3(0f, Random.value * 360, 0f));

        //store start position
        startPos = new Vector3(0f, coinObject.position.y, 0f);
        startPos.y += Mathf.Sin(Time.time * frequency) * amplityde;
       
    }
    private void Update()
    {
        if (levelManager.CurrentGameState != GameState.Running) return;
        //Spin
        coinObject.Rotate(new Vector3(0f, 25f * Time.deltaTime, 0f));

        //bob
        Vector3 pos = startPos;
        pos.y += Mathf.Sin(Time.time * frequency) * amplityde;
        coinObject.position = new Vector3(coinObject.position.x, pos.y, coinObject.position.z);
    }

    private void FixedUpdate()
    {
        if (levelManager.CurrentGameState != GameState.Running)
            return;
        transform.position += Vector3.back * levelManager.GetSpeed() * Time.fixedDeltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            
            levelManager.AddCoin();
            Destroy(this.gameObject);
        }
    }
}
