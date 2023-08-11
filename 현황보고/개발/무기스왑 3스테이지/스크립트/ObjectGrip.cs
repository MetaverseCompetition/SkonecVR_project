using System.Collections;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGrip : XRGrabInteractable
{
    public XRBaseInteractor interactor; // ���� ��ȣ�ۿ� ���� ���ͷ���
    private Quaternion initialAttachRotation; // �ʱ� attach transform�� ȸ���� ���� ����
    private XRInteractorLineVisual LineRender;

    public bool isGripPressed = false; // grip ��ư�� �����ִ��� ���� Ȯ�� ����
    public bool isHave;
    public bool ThisTurn;

    private void Start()
    {
       
    }

    protected override void OnSelectEntered(XRBaseInteractor interactor)  //interactor���� �� ��Ʈ�ѷ��� ����.
    {
        base.OnSelectEntered(interactor);

        // �ʱ� attach transform�� ȸ���� ����
        initialAttachRotation = attachTransform.localRotation;

        this.interactor = interactor; // ���� ��ȣ�ۿ� ���� ���ͷ��� ����

        if(interactor.gameObject.GetComponent<XRInteractorLineVisual>()!=null)
        {
            LineRender = interactor.gameObject.GetComponent<XRInteractorLineVisual>();
            LineRender.enabled = false;
        }
           


        isGripPressed = true; // grip ��ư�� ���������� ����
        isHave = true;
    }
    protected override void OnSelectEntering(XRBaseInteractor interactor)
    {
        base.OnSelectEntering(interactor);

        this.interactor = interactor; // ���� ��ȣ�ۿ� ���� ���ͷ��� ����


        Scaling();
    }

    protected override void OnSelectExited(XRBaseInteractor interactor)
    {
        base.OnSelectExited(interactor);

        this.interactor = interactor; // ���ͷ��� ����Ҵ�������.
        isGripPressed = true;
        //������ 
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
                // ���ͷ��� ��ġ�� ��ü�� ����
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

                // ���ͷ����� ȸ���� �ʱ� attach transform�� ȸ������ �ռ��Ͽ� ��ü�� ȸ���� ����
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