using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class proximityDetector : MonoBehaviour
{
    public GameObject testBullet;
    public GameObject player;
    public Image proximityIndicator;
    public Camera povCamera;
    private Color proximityIndicatorColor;
    public float distanceFromAimCenter;
    public float sizeBaseMultiplier;
    private GameObject proximityIndicatorPrefab;
    public GameObject ProximityIndicatorPrefab { get => proximityIndicatorPrefab; }
    private List<Image> proximityIndicatorPool = new();
    private int usedCount;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //proximityIndicatorColor = proximityIndicator.color;
        //proximityIndicatorColor.a = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        foreach(Image proximityIndicator in proximityIndicatorPool)
        {
            proximityIndicatorColor = proximityIndicator.color;
            proximityIndicatorColor.a = 0f;
            proximityIndicator.color = proximityIndicatorColor;
        }
        //proximityIndicator.color = proximityIndicatorColor;
        //proximityIndicatorColor.a = 0f;
        var hitArray = Physics.OverlapSphere(player.transform.position, 4f);
        foreach(Collider hit in hitArray)
        {
            
            if (hit.CompareTag("Bullet"))
            {
                Vector3 relativePosition = player.transform.InverseTransformPoint(hit.transform.position);
                float relativeDistance = Vector3.Distance(player.transform.position, hit.transform.position);
                
                Vector3 toPlayer = (player.transform.position - hit.transform.position).normalized;
                float towardPlayer = Vector3.Dot(hit.attachedRigidbody.linearVelocity.normalized, toPlayer);
                //Debug.Log(povCamera.WorldToViewportPoint(hit.transform.position).ToString());
                if(relativePosition.z < 0 && towardPlayer > 0.5f)
                {
                    //float angle = Mathf.Atan2(relativePosition.x, relativePosition.z) * Mathf.Rad2Deg; //Getting the angle on the horizontal plane.
                    float angleRad = Mathf.Atan2(relativePosition.x, relativePosition.z);

                    if (usedCount < proximityIndicatorPool.Count)
                    {
                        proximityIndicatorColor = proximityIndicatorPool[usedCount].color;
                        proximityIndicatorColor.a = 1f;
                        proximityIndicatorPool[usedCount].color = proximityIndicatorColor;
                        proximityIndicatorPool[usedCount].gameObject.SetActive(true);
                        proximityIndicatorPool[usedCount].rectTransform.anchoredPosition = new Vector2(Mathf.Sin(angleRad) * distanceFromAimCenter, Mathf.Cos(angleRad) * distanceFromAimCenter);
                        proximityIndicatorPool[usedCount].rectTransform.localRotation = Quaternion.Euler(0f, 0f, Mathf.Sin(angleRad) * 90);
                        proximityIndicatorPool[usedCount].rectTransform.sizeDelta = new Vector2(sizeBaseMultiplier / relativeDistance, sizeBaseMultiplier / relativeDistance);
                        usedCount++;
                    }
                    else
                    {
                        Image instantiatedProximityIndicator = Instantiate(proximityIndicator, proximityIndicator.transform.parent);
                        //Debug.Log(angle);
                        //Debug.Log(relativeDistance);
                        //proximityIndicatorColor.a = 1f;
                        instantiatedProximityIndicator.gameObject.SetActive(true);
                        instantiatedProximityIndicator.rectTransform.anchoredPosition = new Vector2(Mathf.Sin(angleRad) * distanceFromAimCenter, Mathf.Cos(angleRad) * distanceFromAimCenter);
                        instantiatedProximityIndicator.rectTransform.localRotation = Quaternion.Euler(0f, 0f, Mathf.Sin(angleRad) * 90);
                        instantiatedProximityIndicator.rectTransform.sizeDelta = new Vector2(sizeBaseMultiplier / relativeDistance, sizeBaseMultiplier / relativeDistance); 
                        proximityIndicatorPool.Add(instantiatedProximityIndicator);
                    }
                }
            }
        }
        usedCount = 0;
    }
}
