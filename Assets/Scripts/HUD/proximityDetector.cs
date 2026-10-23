using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class proximityDetector : MonoBehaviour
{
    public GameObject player;
    public Image proximityIndicator;
    public Camera povCamera;
    private Color proximityIndicatorColor;
    [Header("Variables")]
    public float distanceFromAimCenter;
    public float sizeBaseMultiplier;
    private GameObject proximityIndicatorPrefab;
    public GameObject ProximityIndicatorPrefab { get => proximityIndicatorPrefab; }
    private List<Image> proximityIndicatorPool = new();
    private int usedCount;

    void Update()
    {
        foreach(Image proximityIndicator in proximityIndicatorPool)
        {
            proximityIndicatorColor = proximityIndicator.color;
            proximityIndicatorColor.a = 0f;
            proximityIndicator.color = proximityIndicatorColor;
        }
        var hitArray = Physics.OverlapSphere(player.transform.position, 4f);
        foreach(Collider hit in hitArray)
        {
            if (hit.CompareTag("Bullet"))
            {
                Vector3 relativePosition = player.transform.InverseTransformPoint(hit.transform.position);

                Vector3 toPlayer = (player.transform.position - hit.transform.position).normalized; 
                float towardPlayer = Vector3.Dot(hit.attachedRigidbody.linearVelocity.normalized, toPlayer); //Determine if bullet is heading towards player using Dot.

                if(relativePosition.z < 0 && towardPlayer > 0.5f)
                {
                    Image tempProximityIndicator;
                    float angleRad = Mathf.Atan2(relativePosition.x, relativePosition.z);
                    float relativeDistance = Vector3.Distance(player.transform.position, hit.transform.position);

                    if (usedCount < proximityIndicatorPool.Count)
                    {
                        tempProximityIndicator = proximityIndicatorPool[usedCount];
                        usedCount++;
                    }
                    else //If no indicator in pool: create a new indicator and add to pool.
                    {
                        Image instantiatedProximityIndicator = Instantiate(proximityIndicator, proximityIndicator.transform.parent);
                        tempProximityIndicator = instantiatedProximityIndicator;
                        tempProximityIndicator.gameObject.SetActive(true);
                        proximityIndicatorPool.Add(instantiatedProximityIndicator);
                    }
                    proximityIndicatorColor = tempProximityIndicator.color;
                    proximityIndicatorColor.a = 1f;
                    tempProximityIndicator.color = proximityIndicatorColor;
                    tempProximityIndicator.rectTransform.anchoredPosition = new Vector2(Mathf.Sin(angleRad) * distanceFromAimCenter, Mathf.Cos(angleRad) * distanceFromAimCenter);
                    tempProximityIndicator.rectTransform.localRotation = Quaternion.Euler(0f, 0f, Mathf.Sin(angleRad) * 90);
                    tempProximityIndicator.rectTransform.sizeDelta = new Vector2(sizeBaseMultiplier / relativeDistance, sizeBaseMultiplier / relativeDistance); //Bigger the closer the bullet is.
                }
            }
        }
        usedCount = 0;
    }
}
