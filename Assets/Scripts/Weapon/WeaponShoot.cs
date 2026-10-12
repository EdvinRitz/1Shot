using UnityEngine;
using System.Collections.Generic;
using System.Collections;



public class WeaponShoot : MonoBehaviour
{
    public WaveSpawner waveSpawner;
    public PlayerInputHandler playerInputHandler;
    public ScoreManager scoreManager;
    public Camera fpCamera;
    public LineRenderer lineRenderer;
    public GameObject muzzle;
    [HideInInspector]
    public int shotsRemaining = 0;
    public List<RaycastHit> validHits = new();

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
        // RaycastNonAlloc and shooting is not gonna be something that happens too often (maybe).
        var hits = Physics.RaycastAll(fpCamera.transform.position, fpCamera.transform.forward);
        Vector3 rayEnd = muzzle.transform.position;
        List<RaycastHit> orderedHitsByDistance = new(hits);
        validHits.Clear();
        orderedHitsByDistance.Sort(SortByDistance);
        foreach (RaycastHit hit in orderedHitsByDistance)
        {
            //The ray does not stop for enemies or bullets. But it stops for everything else.
            if(hit.transform.gameObject.layer != LayerMask.NameToLayer("Enemy") && hit.transform.gameObject.layer != LayerMask.NameToLayer("Bullet"))
            {
                rayEnd = hit.point;
                break;
            }
            
            if(hit.transform.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                validHits.Add(hit);
            }
        }

        //To make sure that the ray gets into objects and stuff. The ray can be a bit shy if you for example hit a square obstacle in a corner. The ray does not fully go into the object.
        rayEnd -= fpCamera.transform.forward * 0.03f;
        DrawRay(muzzle.transform.position, rayEnd);
        
        //Kill all enemies we hit.
        foreach (RaycastHit hitValid in validHits)
        {
            if (hitValid.transform.TryGetComponent<BaseEnemy>(out var enemy))
            {
                enemy.Die();
            }
        }
        playerInputHandler.shootPressed = false;
        scoreManager.addScoreForEnemiesShot(validHits.Count);
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
