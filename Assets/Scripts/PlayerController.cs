using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]float MovementSpeed = 5f;

    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        animator.SetBool("IsRunning", true);
    }

    private void Update()
    {
        // Read in horizontal input (between -1 and 1)
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        //Apply movement
        transform.position += new Vector3(horizontalInput*MovementSpeed * Time.deltaTime, 0f, 0f);

        //Player jump animation
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("Jump");
        }
    }




}
