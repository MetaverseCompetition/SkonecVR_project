using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class XRPlayerController : MonoBehaviour
{
    // Start is called before the first frame update

    private float InitHp=100;
    private float currHp;

    public Image HpBarImage;

    void Start()
    {
        currHp = InitHp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    private void OnCollisionEnter(Collision collision)
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Monster_Sword")
        {
            Debug.Log("Ãæµ¹!");
            currHp -= 10f;

            HpBarImage.fillAmount = currHp / InitHp;

        }


    }

}
