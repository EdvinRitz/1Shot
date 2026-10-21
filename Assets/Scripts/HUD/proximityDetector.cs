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
        if (Vector3.Distance(testBullet.transform.position, player.transform.position) < 1f)
                {
                    Debug.Log("proximity detected");
                }
    }
}
