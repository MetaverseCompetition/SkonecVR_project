using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class SwapWeapon : MonoBehaviour
{
    // Start is called before the first frame update
  
    
    private InputDevice targetDevice;
    private bool isButtonPressed = false;
    private int Idx;

    public XRController controller = null;
    public XRBaseInteractor Rayinteractor;  //�Ǽ��� �� Ray�� �ִ� �����ջ��
    public XRBaseInteractor DirectInteractor;   //�Ǽ��� �ƴ� �� Ray�� ���� Direct�� ������ ���
    public GameObject[] Weapons;
    public GameObject[] WeaponUI;
    public List<GameObject> SwapWeapons = new List<GameObject>();
    public GameObject UI;

    private void Start()
    {



        Idx = -1;
        StartCoroutine(InitSwapWeapon());

       
    }
    // Update is called once per frame

    IEnumerator InitSwapWeapon()
    {
        yield return new WaitForSeconds(1f);

        //��ó�� �⺻ : �� ������Ʈ�� ����ִ�(�� ���)
        if (SwapWeapons[0].gameObject != null)
        {
            DirectInteractor.gameObject.SetActive(false);
            SwapWeapons[0].GetComponent<ObjectGrip>().interactor = Rayinteractor;
            SwapWeapons[0].GetComponent<ObjectGrip>().isGripPressed = true;
           // WeaponUI[0].SetActive(true);
        }
            

    }
    void Update()
    {
        //�÷��̾ ���� ���� �ְ� ���Ұ����� ��Ͽ� ���ٸ�(���� ������Ʈ�� �ߺ����� �߰��Ǵ°��� ���´�)
        for(int i=0;i<Weapons.Length;++i)
        {
            if (Weapons[i].GetComponent<ObjectGrip>().isHave && !(SwapWeapons.Contains(Weapons[i])))
            {
                SwapWeapons.Add(Weapons[i]);

                //���� ���� �� ���ξ������. (�׻� weapon�迭�� ������ ���⸸ �����Ƿ� weapon �迭�� �������϶��� ó��)
                if(i== Weapons.Length - 1)
                {

                    SwapWeapons[i].GetComponent<ObjectGrip>().interactor = DirectInteractor;
                    SwapWeapons[i].GetComponent<ObjectGrip>().isGripPressed = true;
                    Idx = i + 1;
                   // WeaponUI[i].SetActive(true);
                    //���� ���� ui
                    UI.SetActive(true);

                }
                

                break;

            }
        }

        //������ �ڵ����� ��Ȱ��ȭ -> ���⸦2����°��� ����.
        for (int i = Idx-1; i > -1; --i)
        {
            SwapWeapons[i].GetComponent<ObjectGrip>().interactor = null;
            SwapWeapons[i].transform.position = Vector3.zero;
           // WeaponUI[i].SetActive(false);
        }

        //���ⱳü �Լ� ȣ��
        SwapingWeapon();


    }

   
    void SwapingWeapon()
    {

        //�޼� ��Ʈ�ѷ��� y��ư������ ���ⱳü(��� �����ϹǷ� ������ +�ϰԸ���)
        if (controller.inputDevice.TryGetFeatureValue(CommonUsages.secondaryButton, out bool primaryButtonButtonValue))
        {
            if (primaryButtonButtonValue && !isButtonPressed)
            {
                //������ ��Ȱ��ȭ (�̰� y�������� �����)
                if (Idx != -1 && SwapWeapons[Idx].gameObject != null)
                {
                    SwapWeapons[Idx].GetComponent<ObjectGrip>().interactor = null;
                    SwapWeapons[Idx].transform.position = Vector3.zero;
                    //WeaponUI[Idx].SetActive(false);
                }

                ++Idx;
                isButtonPressed = true;

                //idx�� ������ �����ʵ��� ����
                if (Idx > SwapWeapons.Count - 1)
                    Idx = 0;

                if (SwapWeapons[Idx].gameObject != null)
                {
                    //�Ǽ��ϋ� Ray���浹, �Ǽ��̾ƴҶ� direct���⵵�� �ؾ���
                    //�Ǽ��� �ƴҶ�
                    if (Idx != 0)
                    {
                        DirectInteractor.gameObject.SetActive(true);

                        SwapWeapons[Idx].GetComponent<ObjectGrip>().interactor = DirectInteractor;
                        SwapWeapons[Idx].GetComponent<ObjectGrip>().isGripPressed = true;
                       // WeaponUI[Idx].SetActive(true);

                        Rayinteractor.gameObject.SetActive(false);

                    }

                    else
                    {
                        Rayinteractor.gameObject.SetActive(true);


                        SwapWeapons[Idx].GetComponent<ObjectGrip>().interactor = Rayinteractor;
                        SwapWeapons[Idx].GetComponent<ObjectGrip>().isGripPressed = true;
                       // WeaponUI[Idx].SetActive(true);

                        DirectInteractor.gameObject.SetActive(false);
                    }


                }


            }

            else if (!primaryButtonButtonValue && isButtonPressed)
                isButtonPressed = false;







        }
    }

}
