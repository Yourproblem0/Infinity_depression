using UnityEngine;

public class RoadMover : MonoBehaviour
{
    [SerializeField] Vector3 direction;
    [SerializeField] float MoveSpeed = 5f;

    //Moves road towards the player
    private void Update()
    {
        transform.Translate(direction * MoveSpeed * Time.deltaTime);
    }

}
