using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class weaponColliderEventSender : MonoBehaviour
{
    public enum Type
    {
        Mons,
        player,
        Soldier

    }

    public  enum AttackState
    {
        Default,
        Skill1,
        Skill2,
        Skill3,
        Skill4


    }

    public Type CharacterType = Type.player;


    public playerController m_playerRoot;
    public AttackState m_AttackState = AttackState.Default;

    public List<GameObject> HittedObjectList = new List<GameObject>();

    void Start()
    {


        switch (CharacterType)
        {
            case Type.Mons:      
                break;
            case Type.player:
                m_playerRoot = this.transform.root.transform.GetComponent<playerController>();
                break;
     
        }
    }

    void OnEnable()
    {
        if(HittedObjectList.Count>0)
            HittedObjectList.Clear();

    }

    void OnDisable()
    {
        HittedObjectList.Clear();

    }


    void OnTriggerStay2D(Collider2D other)
    {

    
         // Debug.Log("othe1111r::" + other.name);
        if (!HittedObjectList.Contains(other.gameObject))
        {
            HittedObjectList.Add(other.gameObject);
        }
        else
        {
            return;
        }
   //      Debug.Log("2222::" + other.name);

        switch (CharacterType)
        {
            case Type.Mons:


                switch (m_AttackState)
                {
              

                }



                break;
            case Type.player:


                switch (m_AttackState)
                {
                    case AttackState.Default:
                        m_playerRoot.DefaulAttack_Collider(other.gameObject);
                        break;
                    case AttackState.Skill1:
                        break;
                    case AttackState.Skill2:
                        m_playerRoot.Skill_2Attack_Collider(other.gameObject);
                        break;
                    case AttackState.Skill3:
                        m_playerRoot.Skill_3Attack_Collider(other.gameObject);
                        break;
                    case AttackState.Skill4:
                        m_playerRoot.Skill_4Attack_Collider(other.gameObject);
                        break;

                }





                break;
            case Type.Soldier:

                break;


        }


      //  m_MonsterRoot.OnAttackCollision(other.gameObject);



    }
    
}
