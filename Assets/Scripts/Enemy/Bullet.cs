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
        }
        if (hittTransform.CompareTag("Enemy"))
        {
            return;
        }
        Destroy(gameObject);
    }
}
