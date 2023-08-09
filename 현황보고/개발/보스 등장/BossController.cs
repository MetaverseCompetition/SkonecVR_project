using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController: MonoBehaviour
{
    private Animator bossAnim;
    public GameObject bossMonster;
    private Rigidbody bossRb;
    public float hit;
    public GameObject bress;

    public GameObject bossSpawn;
    private Transform[] bossPath;
    private float bossSpeed = 10f;
    private int pathNum = 1;

    // Start is called before the first frame update
    void Start()
    {
        bossMonster.SetActive(false);
        bossAnim = bossMonster.GetComponent<Animator>();
        bossPath = bossSpawn.GetComponentsInChildren<Transform>();
        bossRb = bossMonster.GetComponent<Rigidbody>();

        bossMonster.transform.position = bossPath[pathNum].position;
    }

    // Update is called once per frame
    void Update()
    {
        if (hit >= 3.0f)
        {
            bossAnim.SetTrigger("Die");
        }

        if (BossSpawner.isSpawn)
        {
            bossMonster.SetActive(true);
            Debug.Log("보스 생성!");

            if (pathNum < bossPath.Length) {
                MovePath();
            }

        }
    }

    IEnumerator UseBress()
    {
        yield return new WaitForSeconds(7.0f);
        bossAnim.SetTrigger("Bress");
        yield return new WaitForSeconds(1.5f);
        bress.SetActive(true);
    }

    public void MovePath()
    {
        bossMonster.transform.position = Vector3.MoveTowards
            (bossMonster.transform.position, bossPath[pathNum].transform.position, bossSpeed * Time.deltaTime);
        if (bossMonster.transform.position == bossPath[pathNum].transform.position)
        {
            pathNum++;
        }

        if (pathNum < 6)
        {
            bossMonster.transform.LookAt(bossPath[pathNum].transform.position);
        }

        if (pathNum == 5)
        {
            bossAnim.SetTrigger("FlyIdle");
            StartCoroutine("UseBress");
        }
    }
}
