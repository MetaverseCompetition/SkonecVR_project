using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monsterSword : MonoBehaviour
{
    public GameObject Effect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Weapon")
        {
            ContactPoint Cp = collision.GetContact(0);

            GameObject Particle = Instantiate(Effect, Cp.point, Quaternion.identity);

            GetComponent<AudioSource>().Play();
            Destroy(Particle, 1f);
        }

        if (collision.gameObject.tag == "Body")
        {
            Debug.Log("body Ãæµ¹");
        }
    }
    //private void OnTriggerEnter(Collider other)
    //{
      
    //}
}
