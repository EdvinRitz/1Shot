using UnityEngine;
using System.Collections.Generic;



public class WeaponShoot : MonoBehaviour
{
    public PlayerInputHandler playerInputHandler;
    public Camera fpCamera;

    void Update()
    {
        if(playerInputHandler.shootPressed == true)
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        //Using RaycastAll instead of RaycastNonAlloc since I don't know how big the premade array should be for 
        // RaycastNonAlloc and shooting is not gonna be something that happens too often 
        var hits = Physics.RaycastAll(fpCamera.transform.position, fpCamera.transform.forward);
        List<RaycastHit> orderedHitsByDistance = new(hits);
        List<RaycastHit> validHits = new();
        orderedHitsByDistance.Sort(SortByDistance);
        foreach (RaycastHit hit in orderedHitsByDistance)
        {
            if(hit.transform.gameObject.layer != LayerMask.NameToLayer("Enemy"))
            {
                break;
            }
            
            validHits.Add(hit);
        }
        foreach (RaycastHit hitValid in validHits)
        {
            Debug.Log(hitValid.transform.name);
            if (hitValid.transform.TryGetComponent<BaseEnemy>(out var enemy))
            {
                enemy.Die();
            }
        }
        playerInputHandler.shootPressed = false;
    }

    private int SortByDistance(RaycastHit a, RaycastHit b)
    {
        return a.distance.CompareTo(b.distance);
    }
}
