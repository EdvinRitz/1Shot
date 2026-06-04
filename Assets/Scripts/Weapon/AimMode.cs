using UnityEngine;

public class AimMode : MonoBehaviour
{
    public PlayerInputHandler playerInputHandler;
    public Camera fpCamera;
    public float aimTimeScale = 0.6f;
    public int AimHitCount;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInputHandler.aimHeld)
        {
            Time.timeScale = aimTimeScale;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }
}
