using UnityEngine;
using UnityEngine.UIElements;

public class UpgradeManager : MonoBehaviour
{
    private bool selectionActive;
    public PlayerInputHandler playerInputHandler;
    public WaveSpawner waveSpawner;
    public PlayerHealth playerHealth;
    public PlayerMotor playerMotor;
    public AimMode aimMode;
    public GameObject upgradePanel;
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

        upgradePanel.SetActive(true);

        if (playerInputHandler.upgrade1Pressed)
        {
            Debug.Log("Upgrade 1 selected");
            UpgradeHealth();
        }
        else if (playerInputHandler.upgrade2Pressed && playerMotor.dashCooldown > 0.2f)
        {
            Debug.Log("Upgrade 2 selected");
            UpgradeDash();
        }
        else if (playerInputHandler.upgrade3Pressed)
        {
            Debug.Log("Upgrade 3 selected");
            UpgradeSlowMo();
        }
        else
        {
            return;
        }
        upgradePanel.SetActive(false);
        selectionActive = false;
        waveSpawner.StartWave();
        
    }
    public void BeginSelection()
    {
        selectionActive = true;
        Debug.Log("Choose upgrade: 1, 2, or 3");
    }

    public void UpgradeHealth()
    {
        playerInputHandler.upgrade1Pressed = false;
        playerHealth.RestoreHealth(1f);
    }

        public void UpgradeDash()
    {
        playerInputHandler.upgrade2Pressed = false;
        playerMotor.dashCooldown -= 0.2f; 
    }
        public void UpgradeSlowMo()
    {
        playerInputHandler.upgrade3Pressed = false;
        aimMode.slowMoEnergyMax += 0.5f;
        aimMode.slowMoEnergy = aimMode.slowMoEnergyMax;
    }
}
