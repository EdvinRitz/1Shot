using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public Camera cam;
    private float xRotation = 0f;
    public float xSensetivity = 30f;
    public float ySensetivity = 30f;

    public PlayerInputHandler playerInputHandler;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerInputHandler = GetComponent<PlayerInputHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 lookInput = playerInputHandler.lookInput;
        ProcessLook(lookInput);
    }

    public void ProcessLook(Vector2 input){
        float mouseX = input.x;
        float mouseY = input.y;
        //Calculate camera rotation for looking up and down.
        xRotation -= mouseY * Time.deltaTime * ySensetivity;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);
        //Apply this to our camera transform.
        cam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        //rotate player to look left and right.
        transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * xSensetivity);
    }
}
