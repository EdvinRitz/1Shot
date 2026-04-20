using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    private InputSystem inputSystem;
    public InputSystem.PlayerActions playerActions;
    [HideInInspector]
    public bool jumpPressed;
    [HideInInspector]
    public bool shootPressed;
    [HideInInspector]
    public bool dashPressed;
    public Vector2 MoveInput { get; private set; }
    public Vector2 LookInput { get; private set; }

    void Awake()
    {
        inputSystem = new InputSystem();
        playerActions = inputSystem.Player;
    }

    void Update()
    {
        MoveInput = playerActions.Move.ReadValue<Vector2>();
        LookInput = playerActions.Look.ReadValue<Vector2>();
    }

    private void OnEnable()
    {
        playerActions.Enable();
        playerActions.Jump.performed += ctx => jumpPressed = true;
        playerActions.Shoot.performed += ctx => shootPressed = true;
        playerActions.Dash.performed += ctx => dashPressed = true;
    }

    private void OnDisable()
    {
        playerActions.Disable();
    }
}
