using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;
using UnityEngine;

public class SwordSkill : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;
    public VelocityEstimator velocityEstimator;
    public XRController controller = null;
    public GameObject oraEffect;
    public Transform oraSpawn;
    private Transform playerTr;
    public bool isReadyToShot;
    public bool CanCharge;
    public float GetChargetime; //��¡ ��Ÿ��

    //��¡ǥ��
    public GameObject CanChargeUI;
    public GameObject WaitChargeUI;
    public Image ChargeImage;

    private Rigidbody orabody;
    private float ChargingTime;     //��¡�������ִ½ð�
    //public Transform player;
    public GameObject ChargeEffect;
    private int count = 0;


    // Start is called before the first frame update
    void Start()
    {
       playerTr = GameObject.FindWithTag("MainCamera").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        //��¡�ڵ� �ۼ�
        GetChargetime += Time.deltaTime;
        ChargeImage.fillAmount = ChargingTime / 2f;

        if(GetChargetime>2f)
        {
            CanChargeUI.SetActive(true);
            WaitChargeUI.SetActive(false);
        }

        if (controller.inputDevice.TryGetFeatureValue(CommonUsages.secondaryButton, out bool primaryButtonButtonValue))
        {
            //�˱� ������ 10���Ŀ��� ��¡����
            if(primaryButtonButtonValue&&GetChargetime>2f)
            {
                

                ChargingTime += Time.deltaTime;
                ChargeEffect.SetActive(true);

                //����Ʈ������Ʈ������
                ChargeEffect.transform.localScale = Vector3.Lerp(new Vector3(0.08f, 0.08f, 0.08f), new Vector3(0.3f, 0.08f, 0.3f), ChargingTime);

                //(2�� ������ ��¡)
                if (ChargingTime > 2f)
                {
                    isReadyToShot = true;
                    ChargingTime = 2.01f;
                }
                    
            }

            else
            {
                ChargingTime = 0;
                ChargeEffect.SetActive(false);
            }

        }

       

        if (velocityEstimator.GetVelocityEstimate().magnitude > Vector3.Magnitude(new Vector3(5, 5, 5))&& isReadyToShot)
        {
            createOra();
            isReadyToShot = false;

            CanChargeUI.SetActive(false);
            WaitChargeUI.SetActive(true);
            ChargeEffect.SetActive(false);

            GetChargetime = 0f;
            ChargingTime = 0f;
        }

        //if (!isReadyToShot)
        //{

        //    CanChargeUI.SetActive(false);
        //    WaitChargeUI.SetActive(true);
        //    ChargeEffect.SetActive(false);
        //}
           
          

    }

    public void createOra()
    {
        //Vector3 velocity = velocityEstimator.GetVelocityEstimate();
        //Vector3 planeNormal = Vector3.Cross(endPoint.position - startPoint.position, velocity);
        //planeNormal.Normalize();

        GameObject Ora=Instantiate(oraEffect,oraSpawn.position, oraSpawn.rotation);
        //Ora.transform.position = startPoint.parent.localPosition;
        //Ora.transform.rotation = startPoint.parent.localRotation;
        Vector3 direction = oraSpawn.position - playerTr.position;
        Ora.GetComponent<Rigidbody>().AddForce(direction * 500);

        
    }
}
