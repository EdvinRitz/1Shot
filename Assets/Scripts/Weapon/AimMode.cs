using UnityEngine;
using System.Collections.Generic;

public class AimMode : MonoBehaviour
{
    public PlayerInputHandler playerInputHandler;
    public Camera fpCamera;
    public float aimTimeScale = 0.6f;
    public int AimHitCount;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInputHandler.aimHeld)
        {
            Time.timeScale = aimTimeScale;

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
            
                validHits.Add(hit);
            }
            AimHitCount = validHits.Count;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    private int SortByDistance(RaycastHit a, RaycastHit b)
    {
        return a.distance.CompareTo(b.distance);
    }
}
