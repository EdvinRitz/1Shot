using UnityEngine;
using TMPro;

public class PlayerHUD : MonoBehaviour
{
    public TextMeshProUGUI playerHealthHUD;
    public TextMeshProUGUI dashCooldownHUD;
    public TextMeshProUGUI gameOverHUD;
    public TextMeshProUGUI enemiesAmiedAtHUD;
    public TextMeshProUGUI slowMoEnergyHUD;
    public TextMeshProUGUI ammoHUD;
    public TextMeshProUGUI roundHUD;
    public TextMeshProUGUI scoreHUD;
    public PlayerHealth playerHealth;
    public PlayerMotor playerMotor;
    public PlayerInputHandler playerInputHandler;
    public AimMode aimMode;
    public WeaponShoot weaponShoot;
    public WaveSpawner waveSpawner;
    public ScoreManager scoreManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerHealthHUD.text = "HP: " + playerHealth.Health;

        scoreHUD.text = "SCORE: " + scoreManager.playerScore;

        if(playerMotor.dashCooldownTimer > 0)
        {
            dashCooldownHUD.text = "" + playerMotor.dashCooldownTimer.ToString("F2");
        }
        else
        {
            dashCooldownHUD.text = "";
        }

        if (playerHealth.playerIsDead)
        {
            //gameOverHUD.gameObject.SetActive(true);
            gameOverHUD.enabled = true;
        }
        else
        {
            //gameOverHUD.gameObject.SetActive(false);
            gameOverHUD.enabled = false;
        }

        //if (playerInputHandler.aimHeld)
        //{
            //enemiesAmiedAtHUD.text = "" + aimMode.AimHitCount;
        //}
        //else
        //{
            //enemiesAmiedAtHUD.text = "";
        //}
        if(waveSpawner.currentWaveIndex < waveSpawner.waves.Length){
        enemiesAmiedAtHUD.text = "" + aimMode.AimHitCount + "/" + waveSpawner.waves[waveSpawner.currentWaveIndex].enemiesToSpawn.Length; 
        }
        else
        {
            enemiesAmiedAtHUD.text = "" + aimMode.AimHitCount + "/" + waveSpawner.enemyCount;
        }

        slowMoEnergyHUD.text = "" + aimMode.slowMoEnergy.ToString("F1");

        ammoHUD.text = "Ammo: " + weaponShoot.shotsRemaining;

        if (!waveSpawner.waveActive)
        {
            if(waveSpawner.currentWaveIndex < waveSpawner.waves.Length){
                roundHUD.text = "ROUND " + (waveSpawner.currentWaveIndex + 1) + "\n" + waveSpawner.waves[waveSpawner.currentWaveIndex].enemiesToSpawn.Length + " enemies";
            }
            else
            {
                roundHUD.text = "ROUND " + (waveSpawner.currentWaveIndex + 1) + "\n" + waveSpawner.enemyCount + " enemies";
            }
            
        }
        else
        {
            roundHUD.text = "";
        }
    }
}
