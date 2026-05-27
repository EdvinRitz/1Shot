using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    private InputSystem inputSystem;
    public InputSystem.PlayerActions playerActions;
    public PlayerHealth playerHealth;
    [HideInInspector]
    public bool jumpPressed;
    [HideInInspector]
    public bool shootPressed;
    [HideInInspector]
    public bool dashPressed;
    public bool restartPressed;
    public Vector2 MoveInput { get; private set; }
    public Vector2 LookInput { get; private set; }

    void Awake()
    {
        inputSystem = new InputSystem();
        playerActions = inputSystem.Player;
    }

    void Update()
    {
        if (playerHealth.isDead)
        {
            ResetInput();
            return;
        }
        MoveInput = playerActions.Move.ReadValue<Vector2>();
        LookInput = playerActions.Look.ReadValue<Vector2>();
    }

    private void OnEnable()
    {
        playerActions.Enable();
        playerActions.Jump.performed += ctx => jumpPressed = true;
        playerActions.Shoot.performed += ctx => shootPressed = true;
        playerActions.Dash.performed += ctx => dashPressed = true;
        playerActions.Restart.performed += ctx => restartPressed = true;
    }

    private void ResetInput()
    {
        jumpPressed = false;
        shootPressed = false;
        dashPressed = false;
        MoveInput = Vector2.zero;
        LookInput = Vector2.zero;
    }

    private void OnDisable()
    {
        playerActions.Disable();
    }
}
