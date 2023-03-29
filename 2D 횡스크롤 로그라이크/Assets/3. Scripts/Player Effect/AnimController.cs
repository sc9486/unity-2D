using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class AnimController : MonoBehaviour 
{
    public PlayerController m_PlayerRoot;

    void Start ()
    {
        m_PlayerRoot = this.transform.root.transform.GetComponent<PlayerController>();
    }

    public void Anim_AttackSkill_2_Enter()
    {
        m_PlayerRoot.SkillAttack_Anim_2_Enter();
    }

    public void Anim_AttackSkill_2_Exit()
    {
        m_PlayerRoot.SkillAttack_Anim_2_Exit();
    }

    public void Anim_AttackSkill_3_Enter()
    {
        m_PlayerRoot.SkillAttack_Anim_3_Enter();
    }

    public void Anim_AttackSkill_3_Exit()
    {
        m_PlayerRoot.SkillAttack_Anim_3_Exit();
    }

    public void Anim_Die_Enter()
    {
        m_PlayerRoot.Anim_Die_Enter();
    }
}
