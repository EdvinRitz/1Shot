using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;


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
        //RaycastHit hit;
        var hits = Physics.RaycastAll(fpCamera.transform.position, fpCamera.transform.forward);
        List<RaycastHit> orderedHitsByDistance = new(hits);
        List<RaycastHit> validHits = new();
        orderedHitsByDistance.Sort(SortByDistance);
        foreach (RaycastHit hit in orderedHitsByDistance)
        {
            //Debug.Log(hit.transform.name);
            if(hit.transform.gameObject.layer != LayerMask.NameToLayer("Enemy"))
            {
                break;
            }

            validHits.Add(hit);
        }
        foreach (RaycastHit hitValid in validHits)
        {
            Debug.Log(hitValid.transform.name);
        }
        //Debug.Log(hits);
        playerInputHandler.shootPressed = false;
    }

    private int SortByDistance(RaycastHit a, RaycastHit b)
    {
        return a.distance.CompareTo(b.distance);
    }
}
