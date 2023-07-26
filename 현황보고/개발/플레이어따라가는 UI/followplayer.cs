using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followplayer : MonoBehaviour
{
    // Start is called before the first frame update\

    public GameObject player;
    private Vector3 offset;
    private Vector3 pivotPoint;
    public float heightOffset = 5f;

    private Quaternion initialRotation; // �ʱ� ĵ���� ȸ�� ���¸� �����ϱ� ���� ����
    void Start()
    {
        offset = new Vector3(0, 0, -2f);
        initialRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()

    {

        // �÷��̾ �������� ȸ���� �߽��� ���
        Vector3 pivotPoint = player.transform.position + Vector3.up * heightOffset;

        // �÷��̾� �ֺ��� ��ġ�� ���
        Vector3 desiredPosition = pivotPoint - player.transform.forward * -2;

        // UI�� �÷��̾� �ֺ��� ��ġ�� �̵�
        transform.position = desiredPosition;

        // ĵ������ �ʱ� ȸ�� ���¸� �������� �÷��̾��� �� ȸ���� ���� ȸ��
        transform.rotation = Quaternion.Euler(initialRotation.eulerAngles.x, player.transform.eulerAngles.y, initialRotation.eulerAngles.z);
    }

}

