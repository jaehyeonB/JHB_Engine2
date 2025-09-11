
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float speed = 5f;
    public jumpPower = 5f;
    public gravity = -9.8f;
    
    private CharactorController controller;
    private Vector3 velocity;
    public bool isGrounded;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        isGrounded = controller.isGrounded;
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = new Vector3 (x, 0, z);
        controller.Move(move * speed * Time.deltaTime);

        if(isGrounded && Input.GetKeyDown(KeyDown.Space))
        {
            velocity.y = jumpPower;
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
