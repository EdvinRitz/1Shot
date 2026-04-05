using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMotor : MonoBehaviour
{
    public Vector3 moveDirection = Vector3.zero;
    public Vector3 dashDirection = Vector3.zero;
    private CharacterController controller;
    public PlayerInputHandler playerInputHandler;
    private Vector3 playerVelocity;
    public float speed = 5f;
    private bool isGrounded;
    public float gravity = -9.8f;
    public float jumpHeight = 0.75f;
    public bool isDashing;
    public float dashTimer;

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
        if (!isDashing)
        {
            ProcessMove(playerInputHandler.moveInput);
        }
       
        isGrounded = controller.isGrounded;
        if(playerInputHandler.jumpPressed == true)
        {
            Jump();
        }
        if(playerInputHandler.dashPressed == true || isDashing == true)
        {
            Dash(playerInputHandler.moveInput);
        }
        
    }

    public void ProcessMove(Vector2 input)
    {
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

    public void Dash(Vector2 input)
    {
        if(!isDashing){
        dashDirection.x = input.x;
        dashDirection.z = input.y;
        dashDirection = transform.TransformDirection(dashDirection);
        dashTimer = 0.2f;
        }
        isDashing = true;
        if(dashDirection == Vector3.zero)
        {
            isDashing = false;
            playerInputHandler.dashPressed = false;
            return;
        }
        dashTimer -= Time.deltaTime;
        if(dashTimer > 0)
        {
            controller.Move(speed * 4 * Time.deltaTime * dashDirection);
        }
        else
        {
            isDashing = false;
        }
        //Debug.Log("dashed");
        playerInputHandler.dashPressed = false;
    }
}
