using System.Collections;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGrip : XRGrabInteractable
{
    public XRBaseInteractor interactor; // 현재 상호작용 중인 인터랙터
    private Quaternion initialAttachRotation; // 초기 attach transform의 회전값 저장 변수
    private XRInteractorLineVisual LineRender;

    public bool isGripPressed = false; // grip 버튼이 눌려있는지 여부 확인 변수
    public bool isHave;
    public bool ThisTurn;

    private void Start()
    {
       
    }

    protected override void OnSelectEntered(XRBaseInteractor interactor)  //interactor에는 손 컨트롤러가 들어간다.
    {
        base.OnSelectEntered(interactor);

        // 초기 attach transform의 회전값 저장
        initialAttachRotation = attachTransform.localRotation;

        this.interactor = interactor; // 현재 상호작용 중인 인터랙터 저장

        if(interactor.gameObject.GetComponent<XRInteractorLineVisual>()!=null)
        {
            LineRender = interactor.gameObject.GetComponent<XRInteractorLineVisual>();
            LineRender.enabled = false;
        }
           


        isGripPressed = true; // grip 버튼이 눌려있음을 설정
        isHave = true;
    }
    protected override void OnSelectEntering(XRBaseInteractor interactor)
    {
        base.OnSelectEntering(interactor);

        this.interactor = interactor; // 현재 상호작용 중인 인터랙터 저장


        Scaling();
    }

    protected override void OnSelectExited(XRBaseInteractor interactor)
    {
        base.OnSelectExited(interactor);

        this.interactor = interactor; // 인터랙터 계속할당해주자.
        isGripPressed = true;
        //오른손 
        Scaling();

    }

    private void Update()
    {
        if (isHave&&ThisTurn&&this.interactor!=null)
        {
            Scaling();
            isGripPressed = true;
            OnSelectEntered(this.interactor);


        }

    


        if (isGripPressed)
        {

            if (interactor!=null)
            {
                // 인터랙터 위치에 물체를 고정
                transform.position = interactor.transform.position;

                if (GetComponent<MovementRecognizer>() != null)
                {
                    GetComponent<MovementRecognizer>().enabled = true;
                    GetComponent<MoveSt>().enabled = false;
                }
                
                if(GetComponent<SwordSkill>()!=null)
                {
                    GetComponent<SwordSkill>().enabled = true;
                }

                // 인터랙터의 회전과 초기 attach transform의 회전값을 합성하여 물체에 회전을 적용
                Quaternion newRotation = interactor.transform.rotation * Quaternion.Inverse(initialAttachRotation);
                transform.rotation = newRotation;
            }
         



        }

    }

    private void Scaling()
    {
        transform.localScale = attachTransform.localScale;
    }
}