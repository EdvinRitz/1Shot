using UnityEngine;

public class proximityDetector : MonoBehaviour
{
    public GameObject testBullet;
    public GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var hitArray = Physics.OverlapSphere(player.transform.position,3f);
        foreach(Collider hit in hitArray)
        {
            if (hit.CompareTag("Bullet"))
            {
                Debug.Log("proximity detected");
            }
        }

    }
}
