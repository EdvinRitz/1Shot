using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private float health;
    public float maxHealth = 5;
    //private float lerpTimer;

    void Start()
    {
        health = maxHealth;
    }

    void Update()
    {
        
    }

        public void TakeDamage(float damage)
    {
        health -= damage;
        //lerpTimer = 0f;
        Debug.Log(health);
    }

        public void RestoreHealth(float healAmount)
    {
        health += healAmount;
        //lerpTimer = 0f;
        Debug.Log(health);
    }
}
