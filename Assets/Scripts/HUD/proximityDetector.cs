using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class proximityDetector : MonoBehaviour
{
    public GameObject testBullet;
    public GameObject player;
    public Image proximityIndicator;
    private Color proximityIndicatorColor;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        proximityIndicatorColor = proximityIndicator.color;
        proximityIndicatorColor.a = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        proximityIndicator.color = proximityIndicatorColor;
        var hitArray = Physics.OverlapSphere(player.transform.position, 3f);
        foreach(Collider hit in hitArray)
        {
            Vector3 relativePosition = player.transform.InverseTransformPoint(hit.transform.position);
            float relativeDistance = Vector3.Distance(player.transform.position, hit.transform.position);
            if (hit.CompareTag("Bullet"))
            {
                //Debug.Log("proximity detected");
                if(relativePosition.z < 0)
                {
                    float angle = Mathf.Atan2(relativePosition.x, relativePosition.z) * Mathf.Rad2Deg; //Getting the angle on the horizontal plane.
                    float angleRad = Mathf.Atan2(relativePosition.x, relativePosition.z);
                    Debug.Log(angle);
                    //Debug.Log(relativeDistance);
                    proximityIndicatorColor.a = 1f;
                    proximityIndicator.rectTransform.anchoredPosition = new Vector2(Mathf.Sin(angleRad) * 200, Mathf.Cos(angleRad) * 200);
                    proximityIndicator.rectTransform.localRotation = Quaternion.Euler(0f, 0f, Mathf.Sin(angleRad) * 90);
                }
            }
        }

    }
}
