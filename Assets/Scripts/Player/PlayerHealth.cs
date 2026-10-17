using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    private float health;
    public float Health { get => health; }
    public float startingHealth = 5;
    [HideInInspector]
    public bool playerIsDead;
    public PlayerInputHandler playerInputHandler;
    public UpgradeManager upgradeManager;
    
    [Header("Damage Overlay")]
    public Image damageOverlay;
    private Color damageOverlayColor;
    //also used for Heal Overlay.
    [Tooltip("Also used for Heal Overlay")]
    public float duration;
    [Tooltip("Also used for Heal Overlay")]
    public float fadeSpeed;
    private float durationTimer;

    [Header("Heal Overlay")]
    public Image healOverlay;
    private Color healOverlayColor;

    void Start()
    {
        health = startingHealth;
        playerIsDead = false;
        //No overlays visible
        damageOverlayColor = damageOverlay.color;
        damageOverlayColor.a = 0f;
        healOverlayColor = healOverlay.color;
        healOverlayColor.a = 0f;
    }

    void Update()
    {
        damageOverlay.color = damageOverlayColor;
        healOverlay.color = healOverlayColor;

        if(playerInputHandler.restartPressed && playerIsDead)
        {
            Restart();
        }

        if(damageOverlay.color.a > 0)
        {
            durationTimer += Time.deltaTime;
            if(durationTimer > duration)
            {
                float tempAlpha = damageOverlay.color.a;
                tempAlpha -= Time.deltaTime * fadeSpeed;
                damageOverlayColor.a = tempAlpha;
            }
        }
        else if(healOverlay.color.a > 0)
        {
            durationTimer += Time.deltaTime;
            if(durationTimer > duration)
            {
                float tempAlpha = healOverlay.color.a;
                tempAlpha -= Time.deltaTime * fadeSpeed;
                healOverlayColor.a = tempAlpha;
            }
        }
    }
        public void TakeDamage(float damage)
    {
        durationTimer = 0f;
        healOverlayColor.a = 0f;
        damageOverlayColor.a = 1f;
        
        if(health > 0 && !upgradeManager.selectionActive)
        {
            health -= damage;
        }
        if(health <= 0)
        {
            GameOver();
        }
    }

        public void RestoreHealth(float healAmount)
    {
        durationTimer = 0f;
        damageOverlayColor.a = 0f;
        healOverlayColor.a = 1f;
        
        health += healAmount;
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
        playerIsDead = true;
    }

    public void Restart()
    {
        Debug.Log("Restart");
        playerInputHandler.restartPressed = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
