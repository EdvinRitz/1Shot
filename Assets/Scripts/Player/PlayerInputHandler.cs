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
    public bool upgrade1Pressed;
    public bool upgrade2Pressed;
    public bool upgrade3Pressed;
    public bool aimHeld;
    public Vector2 MoveInput { get; private set; }
    public Vector2 LookInput { get; private set; }

    void Awake()
    {
        inputSystem = new InputSystem();
        playerActions = inputSystem.Player;
    }

    void Update()
    {
        if (playerHealth.playerIsDead)
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

        playerActions.Upgrade1.performed += ctx => upgrade1Pressed = true;
        playerActions.Upgrade2.performed += ctx => upgrade2Pressed = true;
        playerActions.Upgrade3.performed += ctx => upgrade3Pressed = true;

        playerActions.Aim.performed += ctx => aimHeld = true;
        playerActions.Aim.canceled += ctx => aimHeld = false;
    }

    private void ResetInput()
    {
        jumpPressed = false;
        shootPressed = false;
        dashPressed = false;
        MoveInput = Vector2.zero;
        LookInput = Vector2.zero;
        aimHeld = false;
    }

    private void OnDisable()
    {
        playerActions.Disable();
    }
}
