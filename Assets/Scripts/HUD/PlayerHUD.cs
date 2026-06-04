using UnityEngine;
using TMPro;

public class PlayerHUD : MonoBehaviour
{
    public TextMeshProUGUI playerHealthHUD;
    public TextMeshProUGUI dashCooldownHUD;
    public TextMeshProUGUI gameOverHUD;
    public TextMeshProUGUI enemiesAmiedAtHUD;
    public PlayerHealth playerHealth;
    public PlayerMotor playerMotor;
    public PlayerInputHandler playerInputHandler;
    public AimMode aimMode;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerHealthHUD.text = "HP: " + playerHealth.Health;

        if(playerMotor.dashCooldownTimer > 0)
        {
            dashCooldownHUD.text = "" + playerMotor.dashCooldownTimer.ToString("F2");
        }
        else
        {
            dashCooldownHUD.text = "";
        }

        if (playerHealth.isDead)
        {
            //gameOverHUD.gameObject.SetActive(true);
            gameOverHUD.enabled = true;
        }
        else
        {
            //gameOverHUD.gameObject.SetActive(false);
            gameOverHUD.enabled = false;
        }

        if (playerInputHandler.aimHeld)
        {
            enemiesAmiedAtHUD.text = "" + aimMode.AimHitCount;
        }
        else
        {
            enemiesAmiedAtHUD.text = "";
        }
    }
}
