using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    private float health;
    public float Health { get => health; }
    public float maxHealth = 5;
    //private float lerpTimer;
    public bool playerIsDead;
    public PlayerInputHandler playerInputHandler;

    void Start()
    {
        health = maxHealth;
        playerIsDead = false;
    }

    void Update()
    {
        if(playerInputHandler.restartPressed && playerIsDead)
        {
            Restart();
        }
    }

        public void TakeDamage(float damage)
    {
        if(health > 0)
        {
            health -= damage;
        }
        //lerpTimer = 0f;
        //Debug.Log(health);
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
        playerIsDead = true;
    }

    public void Restart()
    {
        Debug.Log("Restart");
        playerInputHandler.restartPressed = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
