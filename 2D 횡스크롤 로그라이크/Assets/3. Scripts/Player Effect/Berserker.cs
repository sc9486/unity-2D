using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Berserker : playerController
{
    public Status status;


    private void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        m_CapsulleCollider  = this.transform.GetComponent<CapsuleCollider2D>();
        m_Anim = this.transform.Find("model").GetComponent<Animator>();
        m_rigidbody = this.transform.GetComponent<Rigidbody2D>();

        status = new Status();
        status = status.SetUnitStatus(UnitCode.player);

    }



    private void Update()
    {
        checkInput();

        if (m_rigidbody.velocity.magnitude > 30)
        {
            m_rigidbody.velocity = new Vector2(m_rigidbody.velocity.x - 0.1f, m_rigidbody.velocity.y - 0.1f);
        }
    }

    public void checkInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            m_Anim.Play("Demo_Skill_1");

        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            m_Anim.Play("Demo_Skill_2");

        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            m_Anim.Play("Demo_Skill_3");

        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            m_Anim.Play("Demo_Die");
        }

            if (m_Anim.GetCurrentAnimatorStateInfo(0).IsName("Demo_Skill_2"))
        {
            if (Is_Skill_2_Attack)
            {
                transform.transform.Translate(new Vector3(-transform.localScale.x * 25f * Time.deltaTime, 0, 0));
            }
            else
            {
                if (m_MoveX < 0)
                {
                    if (transform.localScale.x > 0)
                        transform.transform.Translate(new Vector3(m_MoveX * MoveSpeed * Time.deltaTime, 0, 0));
                }

                else if (m_MoveX > 0)
                {
                    if (transform.localScale.x < 0)
                        transform.transform.Translate(new Vector3(m_MoveX * MoveSpeed * Time.deltaTime, 0, 0));
                }
            }
        }

        if (m_Anim.GetCurrentAnimatorStateInfo(0).IsName("Demo_Skill_1") || m_Anim.GetCurrentAnimatorStateInfo(0).IsName("Demo_Skill_2")
            || m_Anim.GetCurrentAnimatorStateInfo(0).IsName("Demo_Skill_3"))
        {  
            return;
        }

        if (Input.GetKey(KeyCode.Mouse1))
        {
            m_Anim.Play("Demo_Guard");
            return;
        }


        if (Input.GetKeyDown(KeyCode.S))  //아래 버튼 눌렀을때. 
        {
            IsSit = true;
            m_Anim.Play("Demo_Sit");
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            m_Anim.Play("Demo_Idle");
            IsSit = false;
        }

        // sit나 die일때 애니메이션이 돌때는 다른 애니메이션이 되지 않게 한다. 
        if (m_Anim.GetCurrentAnimatorStateInfo(0).IsName("Demo_Sit") || m_Anim.GetCurrentAnimatorStateInfo(0).IsName("Demo_Die"))
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (currentJumpCount < JumpCount)  // 0 , 1
                {
                    DownJump();
                }
            }
            return;
        }

        m_MoveX = Input.GetAxis("Horizontal");

        GroundCheckUpdate();

        if (!m_Anim.GetCurrentAnimatorStateInfo(0).IsName("Demo_Attack"))
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                m_Anim.Play("Demo_Attack");
            }
            else
            {
                if (m_MoveX == 0)
                {
                    if (!OnceJumpRayCheck)
                        m_Anim.Play("Demo_Idle");
                }
                else
                {
                    m_Anim.Play("Demo_Run");
                }
            }
        }

        // 기타 이동 인풋.

        if (Input.GetKey(KeyCode.D))
        {

            if (isGrounded)  // 땅바닥에 있었을때. 
            {



                if (m_Anim.GetCurrentAnimatorStateInfo(0).IsName("Demo_Attack"))
                    return;

                transform.transform.Translate(Vector2.right* m_MoveX * MoveSpeed * Time.deltaTime);



            }
            else
            {

                transform.transform.Translate(new Vector3(m_MoveX * MoveSpeed * Time.deltaTime, 0, 0));

            }




            if (m_Anim.GetCurrentAnimatorStateInfo(0).IsName("Demo_Attack"))
                return;

            if (!Input.GetKey(KeyCode.A))
                Filp(false);


        }
        else if (Input.GetKey(KeyCode.A))
        {


            if (isGrounded)  // 땅바닥에 있었을때. 
            {



                if (m_Anim.GetCurrentAnimatorStateInfo(0).IsName("Demo_Attack"))
                    return;


                transform.transform.Translate(Vector2.right * m_MoveX * MoveSpeed * Time.deltaTime);

            }
            else
            {

                transform.transform.Translate(new Vector3(m_MoveX * MoveSpeed * Time.deltaTime, 0, 0));

            }


            if (m_Anim.GetCurrentAnimatorStateInfo(0).IsName("Demo_Attack"))
                return;

            if (!Input.GetKey(KeyCode.D))
                Filp(true);


        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (m_Anim.GetCurrentAnimatorStateInfo(0).IsName("Demo_Attack"))
                return;


            if (currentJumpCount < JumpCount)  // 0 , 1
            {

                if (!IsSit)
                {
                    prefromJump();


                }
                else
                {
                    DownJump();

                }

            }


        }



    }


  


    protected override void LandingEvent()
    {


        if (!m_Anim.GetCurrentAnimatorStateInfo(0).IsName("Demo_Run") && !m_Anim.GetCurrentAnimatorStateInfo(0).IsName("Demo_Attack"))
            m_Anim.Play("Demo_Idle");

    }

    public override void Damaged(float m_damged, Vector2 dir)
    {

        // 피 달게 한다. 

    }

    public override void DefaulAttack_Collider(GameObject obj) {





    }

    public override void Skill_2Attack_Collider(GameObject obj) {

   

    }
    public override void Skill_3Attack_Collider(GameObject obj) {

    }



    public GameObject Skill1Prefab;
    public GameObject Skill2Prefab;
    public GameObject Skill3Prefab;

    public override void SkillAttack_Anim_2_Enter()
    {
   
        Is_Skill_2_Attack = true;
        GameObject tmpobj = Instantiate(Skill2Prefab, transform.position, Quaternion.identity);
        tmpobj.transform.localScale = new Vector3(-1*transform.localScale.x, 1, 1);
        tmpobj.transform.SetParent(this.transform);
        tmpobj.transform.localPosition= new Vector3(-1.37f, 0.179f, 1);
        //Debug.Log("transform.localScale.x:" + transform.localScale.x);


    }

    public override void SkillAttack_Anim_2_Exit()
    {
     
        Is_Skill_2_Attack = false;


    }

 
    public override void SkillAttack_Anim_3_Enter()
    {
     

        GameObject tmpobj = Instantiate(Skill3Prefab, transform.position, Quaternion.identity);
        Vector3 tmpDir = transform.localScale.x * this.transform.right;
        tmpobj.GetComponent<Skill_3>().Fire(tmpDir,20);
      //  tmpWindCut.transform.localScale = new Vector3(1* transform.localScale.x, 1, 1);
    }


    public override void Anim_Die_Enter()
    {
        Instantiate(BloodPrefab, this.transform.localPosition, Quaternion.identity);       
    }

}
