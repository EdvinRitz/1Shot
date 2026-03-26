using UnityEngine;

public class WeaponShoot : MonoBehaviour
{
    public PlayerInputHandler playerInputHandler;
    public Camera fpCamera;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(playerInputHandler.shootPressed == true)
        {
            //Debug.Log("shot");
            Shoot();
        }
    }

    public void Shoot()
    {
        RaycastHit hit;
        Physics.Raycast(fpCamera.transform.position, fpCamera.transform.forward, out hit);
        Debug.Log(hit.transform);
        playerInputHandler.shootPressed = false;
    }
}
