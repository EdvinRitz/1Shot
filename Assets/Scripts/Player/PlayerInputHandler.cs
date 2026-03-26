using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    private InputSystem inputSystem;
    public InputSystem.PlayerActions playerActions;
    public bool jumpPressed;
    public bool shootPressed;
    private PlayerMotor playerMotor;
    public Vector2 moveInput { get; private set; }
    public Vector2 lookInput { get; private set; }
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Awake()
    {
        inputSystem = new InputSystem();
        playerActions = inputSystem.Player;

        playerMotor = GetComponent<PlayerMotor>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = playerActions.Move.ReadValue<Vector2>();
        if (moveInput != Vector2.zero)
        {
            //Debug.Log(moveInput);
        }

        lookInput = playerActions.Look.ReadValue<Vector2>();
        if (lookInput != Vector2.zero)
        {
            //Debug.Log(lookInput);
        }
        //Debug.Log(jumpPressed);
    }

    private void OnEnable()
    {
        playerActions.Enable();
        playerActions.Jump.performed += ctx => jumpPressed = true;
        playerActions.Shoot.performed += ctx => shootPressed = true;
        //JumpPressed = false;
    }

    private void OnDisable()
    {
        playerActions.Disable();
    }
}
