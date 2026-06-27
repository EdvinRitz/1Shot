using UnityEngine;
using System.Collections.Generic;

public class AimMode : MonoBehaviour
{
    public PlayerInputHandler playerInputHandler;
    public Camera fpCamera;
    public float aimTimeScale = 0.6f;
    public int AimHitCount;
    private float originalFixedDeltaTime;
    public float slowMoEnergy;
    public float slowMoEnergyMax;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        originalFixedDeltaTime = Time.fixedDeltaTime;
        slowMoEnergy = slowMoEnergyMax;
    }

    // Update is called once per frame
    void Update()
    {
        var hits = Physics.RaycastAll(fpCamera.transform.position, fpCamera.transform.forward);
        List<RaycastHit> orderedHitsByDistance = new(hits);
        List<RaycastHit> validHits = new();
        orderedHitsByDistance.Sort(SortByDistance);
        foreach (RaycastHit hit in orderedHitsByDistance)
        {
            if(hit.transform.gameObject.layer != LayerMask.NameToLayer("Enemy") && hit.transform.gameObject.layer != LayerMask.NameToLayer("Bullet"))
            {
                break;
            }
            else if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Bullet"))
            {
                
            }
            else
            {
                validHits.Add(hit);
            }
            
        }
        
        AimHitCount = validHits.Count;

        if (playerInputHandler.aimHeld && slowMoEnergy > 0)
        {
            Time.timeScale = aimTimeScale;
            Time.fixedDeltaTime = originalFixedDeltaTime * Time.timeScale;
            slowMoEnergy -= Time.unscaledDeltaTime;
        }
        else if (playerInputHandler.aimHeld)
        {
            Time.timeScale = 1f;
            Time.fixedDeltaTime = originalFixedDeltaTime;
        }
        else
        {
            Time.timeScale = 1f;
            Time.fixedDeltaTime = originalFixedDeltaTime;
            if(slowMoEnergy < slowMoEnergyMax)
            {
                slowMoEnergy += Time.unscaledDeltaTime/4;
            }
        }

        slowMoEnergy = Mathf.Clamp(slowMoEnergy, 0, slowMoEnergyMax);
    }

    private int SortByDistance(RaycastHit a, RaycastHit b)
    {
        return a.distance.CompareTo(b.distance);
    }
}
