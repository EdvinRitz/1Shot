using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    private InputSystem inputSystem;
    public InputSystem.PlayerActions playerActions;
    public bool JumpPressed { get; private set; }
    private PlayerMotor playerMotor;
    public Vector2 moveInput { get; private set; }
    public Vector2 lookInput { get; private set; }
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Awake()
    {
        inputSystem = new InputSystem();
        playerActions = inputSystem.Player;

        playerMotor = GetComponent<PlayerMotor>();

        playerActions.Jump.performed += ctx => JumpPressed = true;
        playerActions.Jump.canceled += ctx => JumpPressed = false;
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
        //Debug.Log(JumpPressed);
    }

    private void OnEnable()
    {
        playerActions.Enable();
    }

    private void OnDisable()
    {
        playerActions.Disable();
    }
}
