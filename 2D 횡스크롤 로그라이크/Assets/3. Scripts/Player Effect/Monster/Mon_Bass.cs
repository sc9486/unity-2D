﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Mon_Bass : MonoBehaviour {

    public enum StateAnim
    {
        Idle,
        Run,
        Hitted,
        Die,
        Attack,
    }



    public bool Isdie = false;

    [Header("[Setting]")]
 


    public Transform m_Canvas_Trans;

    [Header("[State]")]
    public StateAnim m_StateAnim = StateAnim.Idle;


   
    public float AttackDis = 10;
    public Animator m_Anim;
  
  
    [Header("[currentState]")]

    public float Hp = 30;
    public float m_HP;
    public float m_moveSpeed = 2;
    public float m_Damage = 5;
   

    protected Image HpBarImage;
   
    
    public GameObject Current_Tartget;
    public GameObject TouchSensorObj;
    public CapsuleCollider2D m_collider2d;
    protected Coroutine StateCo;
    protected Vector3 selfPos;
    protected Vector3 selfScale;
  


    protected Rigidbody2D m_rigidbody2D;
    
    protected  bool bLeft = false;



    #region Corountine

    public float skill_1_CoolTime = 5;
    public float skill_2_CoolTime = 5;
    public float skill_3_CoolTime = 5;
    public float skill_4_CoolTime = 5;


    private void Start()
    {
      
    //    Current_Tartget = GameObject.Find("Player");


        Init();


        string tmpId = this.name.Replace("(Clone)", "");
   

        m_Anim = this.transform.Find("model").GetComponent<Animator>();
        m_Canvas_Trans = this.transform.Find("Canvas").transform;
        m_collider2d = this.transform.GetComponent<CapsuleCollider2D>();

        HpBarImage = m_Canvas_Trans.Find("HpBar").Find("HpImage").GetComponent<Image>();
        m_rigidbody2D = this.transform.GetComponent<Rigidbody2D>();

        

        TouchSensorObj = this.transform.Find("TouchSensor").gameObject;


        float  RandomMovespeed = Random.Range(0, 0.5f);


        m_HP = Hp;

        Demo_GM.Gm.MonsterList.Add(this.gameObject);
    }

    

    public IEnumerator SkillCoolTimmer(int skillID)
    {

        // 초기화 .
        float tmpSkillCooltime = 0;
        float tmpSkilltic = 0;
        switch (skillID)
        {
            case 0:
                tmpSkillCooltime = skill_1_CoolTime;
                break;

            case 1:
                tmpSkillCooltime = skill_2_CoolTime;
                break;

            case 2:
                tmpSkillCooltime = skill_3_CoolTime;
                break;

            case 3:
                tmpSkillCooltime = skill_4_CoolTime;
                break;
        }


        while (true)

        {
            tmpSkilltic += 1f;
            //Debug.Log("tmpTime:" + tmpSkilltic);
            // 스킬 불값. 
            if (tmpSkilltic > tmpSkillCooltime)
            {
                switch (skillID)
                {
                    case 0:
                        b_Skill[0] = true;
                   
                        break;

                    case 1:
                        b_Skill[1] = true;
                 
                        break;

                    case 2:
                        b_Skill[2] = true;
                
                        break;

                    case 3:
                        b_Skill[3] = true;
              
                        break;
                }


                yield break;
            }

            yield return new WaitForSeconds(1f);
        }
    }
    #endregion



    #region Function


    public void Filp()
    {
        if (Current_Tartget == null)
            return;

      

        float tmpValue = Current_Tartget.transform.position.x - this.transform.position.x;

        if (tmpValue > 0)
        {
            bLeft = false;

        }
        else
        {
            bLeft = true;

        }

        transform.localScale = new Vector3(bLeft ? 1 : -1, 1, 1);

        Vector3 tmplocalScale = new Vector3(m_Canvas_Trans.localScale.x, m_Canvas_Trans.localScale.y, m_Canvas_Trans.localScale.z);
        m_Canvas_Trans.localScale = new Vector3((bLeft ? 0.15f : -0.15f), tmplocalScale.y, tmplocalScale.z);
    }
    public Vector2 MoveDir;
    public void Move()
    {
        if (Current_Tartget == null)
            return;

        Filp();
        Vector2 tmpDir = Current_Tartget.transform.position - this.transform.position;
        tmpDir = new Vector2(tmpDir.x, 0);

        MoveDir = tmpDir.normalized * 1 * m_moveSpeed;
        transform.transform.Translate(tmpDir.normalized * 1 * m_moveSpeed * Time.deltaTime);

    }

    

  



  


    public int Skill_Chance = 30;   // 스킬 확률
    public bool[] b_Skill = new bool[4] { true, true, true,true };   // 현재 사용 가능한 스킬. 
    public List<int> usable_Skill_Id = new List<int>();

    public bool Search_usable_Skill(int[] userSkillID)  // 트루면 스킬 써주고 펄스면 기본 공격으로 넘긴다. 
    {
        usable_Skill_Id.Clear();
       
        for (int i = 0; i < userSkillID.Length; i++)
        {
            if (b_Skill[userSkillID[i]] == true)
            {
                Debug.Log("usable_Skill_Id" + "[" + i + "]" + userSkillID[i]);
                usable_Skill_Id.Add(userSkillID[i]);
            }
        }




        if (usable_Skill_Id.Count > 0)
        {

            return true;
        }
        else
        {
            return false;

        }


    }


    bool LayerIgnoreChecker = false;
    public  void Touch_SensorEnter(Collider2D obj)
    {
   
        
        if (obj.CompareTag("Player"))
        {
            Current_Tartget = obj.gameObject;
        }

        if (LayerIgnoreChecker)
            return;
        if (obj.CompareTag("Player") )
        {
            Physics2D.IgnoreCollision(m_collider2d, Demo_GM.Gm.CurrentPlayerObj.GetComponent<CapsuleCollider2D>());
            for (int i = 0; i < Demo_GM.Gm.MonsterList.Count; i++)
            {
                Physics2D.IgnoreCollision(m_collider2d, Demo_GM.Gm.MonsterList[i].GetComponent<CapsuleCollider2D>());
                //
              

            }
            LayerIgnoreChecker = true;
            Debug.Log("Ignore the collider in the script (because you lose layer information when uploading the unity pakage).Please Users set layers and ignore them to Edit->projectSettings->physics2D :)");
        }



    }
    public void Touch_SensorExit(Collider2D obj)
    {

        if (obj.CompareTag("Player"))
        {
            Current_Tartget = null;
        }

     
   
    }


    #endregion

        #region RPC


    public GameObject BloodEffect;

    public void SetCreateBloodEffect(float Damage)
    {
        Instantiate(BloodEffect, transform.position, Quaternion.identity);

        float RandomX = Random.Range(0, 0.5f);
       // Debug.Log("아앙:" + BloodEffect);
    }

    


 


    public void SyncHp(float SyncHP)
    {
        m_HP = SyncHP;

        if (m_HP > Hp)
        {
            m_HP = Hp;

        }

        else if (m_HP < 0)
            m_HP = 0;


        //Debug.Log("m_HP::" + m_HP);
        //Debug.Log("Hp::" + Hp);
        HpBarImage.fillAmount = m_HP / Hp;

        if (m_HP <= 0)
        {
            m_rigidbody2D.velocity = new Vector2(0, 0);
            Isdie = true;
            TouchSensorObj.SetActive(false);
            m_Canvas_Trans.gameObject.SetActive(false);

        }
       
    }


    public void SetAnim(string animName)
    {
        //  Debug.Log("Anim:" + animName);

        if (m_Anim == null)
            m_Anim = this.transform.Find("model").GetComponent<Animator>();

     //  Debug.Log("m_Anim:" + animName);
        m_Anim.Play(animName);
       
        switch (animName)
        {
            case "Run":
                m_StateAnim = StateAnim.Run;
                break;
            case "Attack":
                m_StateAnim = StateAnim.Attack;
                break;
            case "Idle":
                m_StateAnim = StateAnim.Idle;
                break;
            case "Die":
                m_StateAnim = StateAnim.Die;
                break;
            case "Hitted":
                m_StateAnim = StateAnim.Hitted;
                break;
        }


    }

    #endregion



    #region virtualFunc


    public abstract void Init();
    public abstract void Damaged(float DamageValue, Vector2 dir, float stunTime);

    public abstract void DefaulAttack_Collider(GameObject obj);
    public abstract void Skill_1Attack_Collider(GameObject obj);
    public abstract void Skill_2Attack_Collider(GameObject obj);
    public abstract void Skill_3Attack_Collider(GameObject obj);
    public abstract void Skill_4Attack_Collider(GameObject obj);



    public virtual void HittedFuc(float StunTime) { }

  //  public bool Is_OnceAttack = true;
    public virtual void DefaultAttack_Anim_1_Enter() { }
    public virtual void DefaultAttack_Anim_1_Exit() { }

    public virtual void SkillAttack_Anim_1_Enter() { }
    public virtual void SkillAttack_Anim_1_Exit() { }

    public virtual void SkillAttack_Anim_2_Enter() { }
    public virtual void SkillAttack_Anim_2_Exit() { }


    public virtual void SkillAttack_Anim_3_Enter() { }
    public virtual void SkillAttack_Anim_3_Exit() { }

    public virtual void SkillAttack_Anim_4_Enter() { }
    public virtual void SkillAttack_Anim_4_Exit() { }


    #endregion



}
