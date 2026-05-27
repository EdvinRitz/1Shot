using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMotor : MonoBehaviour
{
    private Vector3 moveDirection = Vector3.zero;
    private Vector3 dashDirection = Vector3.zero;
    private CharacterController controller;
    public PlayerInputHandler playerInputHandler;
    private Vector3 playerVelocity;
    public float speed = 5f;
    private bool isGrounded;
    private float gravity = -9.8f;
    public float jumpHeight = 0.75f;
    private bool isDashing;
    private float dashTimer;
    public float dashCooldown = 1f;
    private float dashCooldownTimer;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (dashCooldownTimer > 0)
        {
            dashCooldownTimer -= Time.deltaTime;
            playerInputHandler.dashPressed = false;
        }

        if (!isDashing)
        {
            ProcessMove(playerInputHandler.MoveInput);
        }
       
        isGrounded = controller.isGrounded;
        if(playerInputHandler.jumpPressed == true)
        {
            Jump();
        }
        if((playerInputHandler.dashPressed == true && dashCooldownTimer <= 0) || isDashing == true)
        {
            Dash(playerInputHandler.MoveInput);
        }
    }

    public void ProcessMove(Vector2 input)
    {
        moveDirection.x = input.x;
        moveDirection.z = input.y;
        controller.Move(speed * Time.deltaTime * transform.TransformDirection(moveDirection));
        playerVelocity.y += gravity * Time.deltaTime;
        if (isGrounded && playerVelocity.y < 0){
            playerVelocity.y = -2f;
        }
        controller.Move(playerVelocity * Time.deltaTime);
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
        //Can't dash if we are not moving
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
            dashCooldownTimer = dashCooldown;
        }
        playerInputHandler.dashPressed = false;
    }
}
