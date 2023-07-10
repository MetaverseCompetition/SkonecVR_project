using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnMonster : MonoBehaviour
{
    public GameObject monsterPrefeb; // 몬스터 프리팹 저장 변수
    public Transform[] spawnPoints; // 스폰포인트 저장 변수
    private int pointNum; // 랜덤한 포인트 결정
    private int spawnNum;
    private float timer;
    private float spawnTime = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        // 각 스폰포인트 위치 저장
        spawnPoints = GetComponentsInChildren<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        //랜덤한 위치에 몬스터 생성
        if (spawnNum < 50 && timer > spawnTime && ObjectGrip.isGripPressed)
        {
            pointNum = Random.Range(0, spawnPoints.Length);
            GameObject monster = Instantiate(monsterPrefeb, spawnPoints[pointNum]);
            spawnNum++;
            timer = 0;
        }
    }
}
