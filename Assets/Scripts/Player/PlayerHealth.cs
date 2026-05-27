using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    private float health;
    public float maxHealth = 5;
    //private float lerpTimer;
    public bool isDead;
    public PlayerInputHandler playerInputHandler;

    void Start()
    {
        health = maxHealth;
        isDead = false;
    }

    void Update()
    {
        if(playerInputHandler.restartPressed && isDead)
        {
            Restart();
        }
    }

        public void TakeDamage(float damage)
    {
        health -= damage;
        //lerpTimer = 0f;
        Debug.Log(health);
        if(health <= 0)
        {
            GameOver();
        }
    }

        public void RestoreHealth(float healAmount)
    {
        health += healAmount;
        //lerpTimer = 0f;
        Debug.Log(health);
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
        isDead = true;
    }

    public void Restart()
    {
        Debug.Log("Restart");
        playerInputHandler.restartPressed = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
