using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOra : MonoBehaviour
{
    private Rigidbody orabody;
    public GameObject swordTr;
    // Start is called before the first frame update
    void Start()
    {
        swordTr = GameObject.FindGameObjectWithTag("Player");
        orabody = GetComponent<Rigidbody>();
        orabody.velocity = swordTr.transform.forward * 100;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
