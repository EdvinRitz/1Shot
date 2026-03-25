using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    public PlayerInputHandler playerInputHandler;
    private Vector3 playerVelocity;
    public float speed = 5f;
    private bool isGrounded;
    public float gravity = -9.8f;
    public float jumpHeight = 0.75f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {

    }
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveInput = playerInputHandler.moveInput;
        ProcessMove(moveInput);

        isGrounded = controller.isGrounded;
        if(playerInputHandler.jumpPressed == true)
        {
            Jump();
        }
        
    }

    public void ProcessMove(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;
        controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);
        playerVelocity.y += gravity * Time.deltaTime;
        if (isGrounded && playerVelocity.y < 0){
            playerVelocity.y = -2f;
        }
        controller.Move(playerVelocity * Time.deltaTime);
        //Vector3 worldMove = transform.TransformDirection(moveDirection) * speed;
        //Debug.Log($"input: {input}, move magnitude: {worldMove.magnitude}");

    }

        public void Jump(){
        if(isGrounded){
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }
        playerInputHandler.jumpPressed = false;
    }
}
