using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private float health;
    public float maxHealth = 5;
    //private float lerpTimer;
    public bool isDead;

    void Start()
    {
        health = maxHealth;
        isDead = false;
    }

    void Update()
    {
        
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
}
