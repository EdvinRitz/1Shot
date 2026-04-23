using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private float health;
    public float maxHealth = 5;
    private float lerpTimer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

        public void TakeDamage(float damage)
    {
        health -= damage;
        lerpTimer = 0f;
        Debug.Log(health);
    }

        public void RestoreHealth(float healAmount)
    {
        health += healAmount;
        lerpTimer = 0f;
        Debug.Log(health);
    }
}
