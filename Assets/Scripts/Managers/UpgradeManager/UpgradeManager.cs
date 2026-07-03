using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    private bool selectionActive;
    public PlayerInputHandler playerInputHandler;
    public WaveSpawner waveSpawner;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!selectionActive)
        {
            playerInputHandler.upgrade1Pressed = false;
            playerInputHandler.upgrade2Pressed = false;
            playerInputHandler.upgrade3Pressed = false;
            return;
        }

        if (playerInputHandler.upgrade1Pressed)
        {
            Debug.Log("Upgrade 1 selected");
            playerInputHandler.upgrade1Pressed = false;
        }
        else if (playerInputHandler.upgrade2Pressed)
        {
            Debug.Log("Upgrade 2 selected");
            playerInputHandler.upgrade2Pressed = false;
        }
        else if (playerInputHandler.upgrade3Pressed)
        {
            Debug.Log("Upgrade 3 selected");
            playerInputHandler.upgrade3Pressed = false;
        }
        else
        {
            return;
        }

        selectionActive = false;
        waveSpawner.StartWave();
        
    }
    public void BeginSelection()
    {
        selectionActive = true;
        Debug.Log("Choose upgrade: 1, 2, or 3");
    }
}
