using UnityEngine;

public class Bullet : MonoBehaviour
{
    bool hasHit = false;
    private void OnCollisionEnter(Collision collision)
    {
        if (hasHit)
        {
            Destroy(gameObject);
            return;
        }
        Transform hittTransform = collision.transform;
        if (hittTransform.CompareTag("Player"))
        {
            hasHit = true;
            //TODO implement Player damage logic
            //hittTransform.GetComponent<PlayerHealth>().TakeDamage(10);
            Destroy(gameObject);
        }
        if (hittTransform.CompareTag("Enemy") || hittTransform.CompareTag("Bullet") || hittTransform.CompareTag("RayCastStop"))
        {
            return;
        }
        Destroy(gameObject);
    }
}
