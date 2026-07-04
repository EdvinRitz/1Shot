using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    public bool selectionActive;
    public PlayerInputHandler playerInputHandler;
    public WaveSpawner waveSpawner;
    public PlayerHealth playerHealth;
    public PlayerMotor playerMotor;
    public AimMode aimMode;
    public GameObject upgradePanel;
    public Button dashUpgradeButton;
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
        
    }
    public void BeginSelection()
    {
        selectionActive = true;
        Debug.Log("Choose upgrade: 1, 2, or 3");
        dashUpgradeButton.interactable = playerMotor.dashCooldown > 0.2f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void UpgradeHealth()
    {
        playerInputHandler.upgrade1Pressed = false;
        playerHealth.RestoreHealth(1f);
        StartNextWave();
    }

    public void UpgradeDash()
    {
        playerInputHandler.upgrade2Pressed = false;
        playerMotor.dashCooldown = Mathf.Max(0.2f, playerMotor.dashCooldown - 0.2f);
        StartNextWave();
    }
    public void UpgradeSlowMo()
    {
        playerInputHandler.upgrade3Pressed = false;
        aimMode.slowMoEnergyMax += 0.5f;
        aimMode.slowMoEnergy = aimMode.slowMoEnergyMax;
        StartNextWave();
    }

    public void StartNextWave()
    {
        upgradePanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        selectionActive = false;
        waveSpawner.StartWave();
    }

}
