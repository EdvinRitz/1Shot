using UnityEngine;
using System.Collections.Generic;
using UnityEditor;
using System.Collections;



public class WeaponShoot : MonoBehaviour
{
    public WaveSpawner waveSpawner;
    public PlayerInputHandler playerInputHandler;
    public Camera fpCamera;
    public LineRenderer lineRenderer;
    public GameObject muzzle;
    public int shotsRemaining = 0;

    void Update()
    {
        if(playerInputHandler.shootPressed == true)
        {
            if(shotsRemaining <= 0)
            {
                playerInputHandler.shootPressed = false;
            }
            else if(shotsRemaining > 0)
            {
                Shoot();
            }
            
        }
    }

    public void Shoot()
    {
        shotsRemaining--;
        //Using RaycastAll instead of RaycastNonAlloc since I don't know how big the premade array should be for 
        // RaycastNonAlloc and shooting is not gonna be something that happens too often 
        var hits = Physics.RaycastAll(fpCamera.transform.position, fpCamera.transform.forward);
        Vector3 rayEnd = muzzle.transform.position;
        List<RaycastHit> orderedHitsByDistance = new(hits);
        List<RaycastHit> validHits = new();
        orderedHitsByDistance.Sort(SortByDistance);
        foreach (RaycastHit hit in orderedHitsByDistance)
        {
            if(hit.transform.gameObject.layer != LayerMask.NameToLayer("Enemy") && hit.transform.gameObject.layer != LayerMask.NameToLayer("Bullet"))
            {
                rayEnd = hit.point;
                break;
            }
            
            validHits.Add(hit);
        }

        rayEnd -= fpCamera.transform.forward * 0.03f;
        DrawRay(muzzle.transform.position, rayEnd);

        foreach (RaycastHit hitValid in validHits)
        {
            Debug.Log(hitValid.transform.name);
            if (hitValid.transform.TryGetComponent<BaseEnemy>(out var enemy))
            {
                enemy.Die();
            }
        }
        playerInputHandler.shootPressed = false;

        waveSpawner.ResolveWave();
    }

    private void DrawRay(Vector3 rayStart, Vector3 rayEnd)
    {
        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, rayStart);
        lineRenderer.SetPosition(1, rayEnd);
        StartCoroutine(DisableAfterDelay());
    }

    private int SortByDistance(RaycastHit a, RaycastHit b)
    {
        return a.distance.CompareTo(b.distance);
    }
    IEnumerator DisableAfterDelay()
    {
        yield return new WaitForSeconds(0.5f);
        lineRenderer.enabled = false;
    }
}
