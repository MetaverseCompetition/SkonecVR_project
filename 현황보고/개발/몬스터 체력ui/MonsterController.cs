using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : MonoBehaviour
{
    private NavMeshAgent ag;
    private Animator Anim;
    public Transform playerTr;

    public GameObject SlashEffect;

    //Die Shader
    public Material Disolve;
    public MeshRenderer[] MeshRenders;
    public SkinnedMeshRenderer skinnedMeshRenderer;
    

    private bool isDie;

    //hitcount
    public int HitCount;
    public GameObject[] Hitui;

    //랜덤으로 죽는모션 위해
    public string[] strTrigger=new string[2];

    //애니메이션 속도 제어
    private float walkSpeed;
    private float AttackSpeed;

    //정찰지
    public Transform[] Destinations;
    public int RandomIdx;

    //target=player
    public Transform target;

    //죽었을 때 칼 collider 비활성화
    public MeshCollider Sword;

    // Start is called before the first frame update
    void Start()
    {
        RandomIdx = 0;

        walkSpeed = Random.Range(0.5f, 1f);
        AttackSpeed = Random.Range(0.5f, 1f);


        strTrigger[0] = "isDead";
        strTrigger[1] = "isDead2";

        ag = GetComponent<NavMeshAgent>();
        playerTr = GameObject.FindWithTag("Player").GetComponent<Transform>();
        Anim = GetComponent<Animator>();

        Anim.SetFloat("WalkSpeed", walkSpeed);
        Anim.SetFloat("AttackSpeed",AttackSpeed);

        //spawn shader
        StartCoroutine(SpawnSkinnedMeshRenderer(skinnedMeshRenderer));
        foreach(var mesh in MeshRenders)
        {
            StartCoroutine(SpawnMeshRenderer(mesh));
        }
       
        InvokeRepeating("MoveToNextPoint", 7f, 2f);


}

    // Update is called once per frame
    void Update()
    {
        

        if(target != null&&!isDie)
        {
            
            ag.SetDestination(playerTr.position);
            transform.LookAt(playerTr.position);

            if (ag.remainingDistance <= 2.5f)
            {
                Anim.SetBool("isAttack", true);
                // ag.isStopped = true;
            }

            else
            {
                Anim.SetBool("isAttack", false);
                //ag.isStopped = false;
            }

           
        }

        if (isDie)
        {
            target = null;
            ag.isStopped = true;

           

         
        }


    }

    private void OnTriggerEnter(Collider other)
    {
      

        if(other.gameObject.tag=="Weapon")
        {
            if(HitCount<3)
                Hitui[HitCount].SetActive(false);

            if(HitCount<3)
                ++HitCount;

          

            if(HitCount==3)
            {
                isDie = true;

                //충돌위치
                Vector3 cp = other.bounds.ClosestPoint(transform.position);
                int random = Random.Range(0, 2);
                Anim.SetTrigger(strTrigger[random]);


                ag.isStopped = true;

                GameObject slash = Instantiate(SlashEffect, cp, Quaternion.identity);
                slash.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360)));


                Destroy(slash, 1f);
                this.gameObject.transform.position += new Vector3(0f, 0.5f, 0f);
                this.gameObject.GetComponent<CapsuleCollider>().enabled = false;
                Sword.enabled = false;




                skinnedMeshRenderer.material = Disolve;

                foreach (var mesh in MeshRenders)
                {
                    mesh.material = Disolve;

                }



                foreach (var mesh in MeshRenders)
                {
                    StartCoroutine(DecreaseValueMeshRenderer(mesh));
                }
                StartCoroutine(DecreaseValueSkinnedMeshRenderer(skinnedMeshRenderer));



                StartCoroutine(Set_Enable());
            }
           



            
          
        }
    }

    public void SetTarget(Transform p_Target)
    {
        CancelInvoke();
        target = p_Target;
    }

    
   void MoveToNextPoint()
    {
        if(target ==null)
        {
            if (GetComponent<Rigidbody>().velocity == Vector3.zero)
            {
                ag.SetDestination(Destinations[RandomIdx++].position);

                if (RandomIdx >= Destinations.Length)
                    RandomIdx = 0;
            }
        }
        
    }
    IEnumerator Time()
    {
        yield return new WaitForSeconds(3f);
    }
    

    IEnumerator SpawnSkinnedMeshRenderer(SkinnedMeshRenderer Mesh)
    {
        float f = 1.23f;
        //float materials = Mesh.material.parent.GetVector("_DissolveOffset").y;

        while (f > -1f)
        {

            yield return new WaitForSeconds(0.03f);

            f -= 0.01f;

            
             Mesh.material.SetVector("_DissolveOffest", new Vector3(0,f,0));
            


        }
    }

    IEnumerator SpawnMeshRenderer(MeshRenderer Mesh)
    {
        float f = 1.23f;
        //float materials = Mesh.material.parent.GetVector("_DissolveOffset").y;

        while (f > -1f)
        {

            yield return new WaitForSeconds(0.03f);

            f -= 0.01f;


            Mesh.material.SetVector("_DissolveOffest", new Vector3(0, f, 0));



        }
    }

    IEnumerator DecreaseValueSkinnedMeshRenderer(SkinnedMeshRenderer Mesh)
    {
        float f = 0f;
        float materials = Mesh.material.GetFloat("_Dissolve");

        while (f < 1f)
        {

            yield return new WaitForSeconds(0.03f);

            f +=0.01f;
            Mesh.material.SetFloat("_Dissolve", f);
            

            
        }
    }

    IEnumerator DecreaseValueMeshRenderer(MeshRenderer Mesh)
    {
        float f = 0f;

        while (f < 1f)
        {
            yield return new WaitForSeconds(0.03f);

            f += 0.01f;
            Mesh.material.SetFloat("_Dissolve", f);

           
        }
    }
    
    IEnumerator Set_Enable()
    {
       

        yield return new WaitForSeconds(5f);

        this.gameObject.SetActive(false);

    }

    private void OnDisable()
    {

        Destroy(this.gameObject);
    }
}
