using UnityEngine;

public class DemoPlayerController : MonoBehaviour
{
    [SerializeField] float playerSpeed = 5f;
    //GetKey = continous, ja GetKeyDown = When key goes down, ja GetKeyUp = When key is released, something happens

    private void Update()
    {
        //are moving
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(-playerSpeed, 0, 0) * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += new Vector3(playerSpeed, 0, 0) * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.position += new Vector3(0, 0, playerSpeed) * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.position += new Vector3(0, 0, -playerSpeed) * Time.deltaTime;
        }
    }

}
