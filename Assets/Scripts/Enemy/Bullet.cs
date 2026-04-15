using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Transform hittTransform = collision.transform;
        if (hittTransform.CompareTag("Player"))
        {
            Debug.Log("Hit Player");
            //hittTransform.GetComponent<PlayerHealth>().TakeDamage(10);
            Destroy(gameObject);
        }
        if (hittTransform.CompareTag("Enemy") || hittTransform.CompareTag("Bullet"))
        {
            return;
        }
        Destroy(gameObject);
    }
}
