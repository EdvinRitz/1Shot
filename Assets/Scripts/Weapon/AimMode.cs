using UnityEngine;
using System.Collections.Generic;

public class AimMode : MonoBehaviour
{
    public PlayerInputHandler playerInputHandler;
    public Camera fpCamera;
    public float aimTimeScale;
    [HideInInspector]
    public int AimHitCount;
    private float originalFixedDeltaTime;
    [HideInInspector]
    public float slowMoEnergy;
    public float slowMoEnergyMax;
    public static bool XRayActive { get; private set; }

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

        //Constantly see how many enemies "in a row" are we aiming at right now?
        foreach (RaycastHit hit in orderedHitsByDistance)
        {
            if(hit.transform.gameObject.layer != LayerMask.NameToLayer("Enemy") && hit.transform.gameObject.layer != LayerMask.NameToLayer("Bullet"))
            {
                break; //Don't see behind obstacles like walls and stuff. But do see behind enemies and bullets.
            }
            else if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Bullet"))
            {
                //Do nothing if we look at a bullet.
            }
            else
            {
                if (hit.transform.TryGetComponent<BaseEnemy>(out var enemy))
                {
                    if(!enemy.EnemyIsDead){
                        validHits.Add(hit);
                    }
                }
                
            }
            
        }
        
        AimHitCount = validHits.Count;

        if (playerInputHandler.aimHeld && slowMoEnergy > 0)
        {
            // We can decide how slowmo the slowmo is by assigning Time.timescale our own value.
            Time.timeScale = aimTimeScale;
            Time.fixedDeltaTime = originalFixedDeltaTime * Time.timeScale;
            //slowMoEnergy drains "in normal time".
            slowMoEnergy -= Time.unscaledDeltaTime;
        }
        else if (playerInputHandler.aimHeld)
        {
            //No slowMo if no slowMoEnergy.
            Time.timeScale = 1f;
            Time.fixedDeltaTime = originalFixedDeltaTime;
        }
        else
        {
            Time.timeScale = 1f;
            Time.fixedDeltaTime = originalFixedDeltaTime;
            //slowMoEnergy regenerates slowly if you don't press the slowMo botton.
            if(slowMoEnergy < slowMoEnergyMax)
            {
                slowMoEnergy += Time.unscaledDeltaTime/4;
            }
        }

        slowMoEnergy = Mathf.Clamp(slowMoEnergy, 0, slowMoEnergyMax);
        XRayActive = playerInputHandler.aimHeld && slowMoEnergy > 0f;
    }

    private int SortByDistance(RaycastHit a, RaycastHit b)
    {
        return a.distance.CompareTo(b.distance);
    }
}
