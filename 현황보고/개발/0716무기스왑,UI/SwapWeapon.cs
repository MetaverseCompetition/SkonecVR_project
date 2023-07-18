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
    public XRBaseInteractor Rayinteractor;  //맨손일 시 Ray가 있는 오른손사용
    public XRBaseInteractor DirectInteractor;   //맨손이 아닐 시 Ray가 없고 Direct인 오른손 사용
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

        //맨처음 기본 : 빈 오브젝트를 들고있다(손 출력)
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
        //플레이어가 잡은 적이 있고 스왑가능한 목록에 없다면(같은 오브젝트가 중복으로 추가되는것을 막는다)
        for(int i=0;i<Weapons.Length;++i)
        {
            if (Weapons[i].GetComponent<ObjectGrip>().isHave && !(SwapWeapons.Contains(Weapons[i])))
            {
                SwapWeapons.Add(Weapons[i]);

                //무기 게임 중 새로얻었을때. (항상 weapon배열의 마지막 무기만 얻으므로 weapon 배열의 마지막일때만 처리)
                if(i== Weapons.Length - 1)
                {

                    SwapWeapons[i].GetComponent<ObjectGrip>().interactor = DirectInteractor;
                    SwapWeapons[i].GetComponent<ObjectGrip>().isGripPressed = true;
                    Idx = i + 1;
                   // WeaponUI[i].SetActive(true);
                    //몬스터 등장 ui
                    UI.SetActive(true);

                }
                

                break;

            }
        }

        //이전걸 자동으로 비활성화 -> 무기를2개드는것을 방지.
        for (int i = Idx-1; i > -1; --i)
        {
            SwapWeapons[i].GetComponent<ObjectGrip>().interactor = null;
            SwapWeapons[i].transform.position = Vector3.zero;
           // WeaponUI[i].SetActive(false);
        }

        //무기교체 함수 호출
        SwapingWeapon();


    }

   
    void SwapingWeapon()
    {

        //왼손 컨트롤러의 y버튼누를때 무기교체(계속 증가하므로 뗐을때 +하게만듬)
        if (controller.inputDevice.TryGetFeatureValue(CommonUsages.secondaryButton, out bool primaryButtonButtonValue))
        {
            if (primaryButtonButtonValue && !isButtonPressed)
            {
                //이전꺼 비활성화 (이건 y눌렀을때 실행됨)
                if (Idx != -1 && SwapWeapons[Idx].gameObject != null)
                {
                    SwapWeapons[Idx].GetComponent<ObjectGrip>().interactor = null;
                    SwapWeapons[Idx].transform.position = Vector3.zero;
                    //WeaponUI[Idx].SetActive(false);
                }

                ++Idx;
                isButtonPressed = true;

                //idx가 범위를 넘지않도록 설정
                if (Idx > SwapWeapons.Count - 1)
                    Idx = 0;

                if (SwapWeapons[Idx].gameObject != null)
                {
                    //맨손일떈 Ray로충돌, 맨손이아닐땐 direct로잡도록 해야함
                    //맨손이 아닐때
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
