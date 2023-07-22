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

    private Quaternion initialRotation; // 초기 캔버스 회전 상태를 저장하기 위한 변수
    void Start()
    {
        offset = new Vector3(0, 0, -2f);
        initialRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()

    {

        // 플레이어를 기준으로 회전할 중심점 계산
        Vector3 pivotPoint = player.transform.position + Vector3.up * heightOffset;

        // 플레이어 주변의 위치를 계산
        Vector3 desiredPosition = pivotPoint - player.transform.forward * -2;

        // UI를 플레이어 주변의 위치로 이동
        transform.position = desiredPosition;

        // 캔버스의 초기 회전 상태를 기준으로 플레이어의 고개 회전에 따라 회전
        transform.rotation = Quaternion.Euler(initialRotation.eulerAngles.x, player.transform.eulerAngles.y, initialRotation.eulerAngles.z);
    }

}

