using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnMonster : MonoBehaviour
{
    public GameObject monsterPrefeb; // ���� ������ ���� ����
    public Transform[] spawnPoints; // ��������Ʈ ���� ����
    private int pointNum; // ������ ����Ʈ ����
    private int spawnNum;
    private float timer;
    private float spawnTime = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        // �� ��������Ʈ ��ġ ����
        spawnPoints = GetComponentsInChildren<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        //������ ��ġ�� ���� ����
        if (spawnNum < 50 && timer > spawnTime && ObjectGrip.isGripPressed)
        {
            pointNum = Random.Range(0, spawnPoints.Length);
            GameObject monster = Instantiate(monsterPrefeb, spawnPoints[pointNum]);
            spawnNum++;
            timer = 0;
        }
    }
}
