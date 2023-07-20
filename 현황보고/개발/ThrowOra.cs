using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowOra : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;
    public VelocityEstimator velocityEstimator;
    public GameObject oraEffect;
    private Rigidbody orabody;
    public Transform player;
    private int count = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       if (velocityEstimator.GetVelocityEstimate().magnitude > Vector3.Magnitude(new Vector3(10,10,10)) )
        {
            createOra();
        }
        
    }

    public void createOra()
    {
        Vector3 velocity = velocityEstimator.GetVelocityEstimate();
        Vector3 planeNormal = Vector3.Cross(endPoint.position - startPoint.position, velocity);
        planeNormal.Normalize();

        Instantiate(oraEffect);
        oraEffect.transform.position = startPoint.parent.localPosition;
        oraEffect.transform.rotation = startPoint.parent.localRotation * Quaternion.EulerAngles(90, 0, 0);
    }

}
