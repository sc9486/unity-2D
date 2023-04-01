using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class AnimController : MonoBehaviour 
{
    public playerController m_playerRoot;

    void Start ()
    {
        m_playerRoot = this.transform.root.transform.GetComponent<playerController>();
    }

    public void Anim_AttackSkill_2_Enter()
    {
        m_playerRoot.SkillAttack_Anim_2_Enter();
    }

    public void Anim_AttackSkill_2_Exit()
    {
        m_playerRoot.SkillAttack_Anim_2_Exit();
    }

    public void Anim_AttackSkill_3_Enter()
    {
        m_playerRoot.SkillAttack_Anim_3_Enter();
    }

    public void Anim_AttackSkill_3_Exit()
    {
        m_playerRoot.SkillAttack_Anim_3_Exit();
    }

    public void Anim_Die_Enter()
    {
        m_playerRoot.Anim_Die_Enter();
    }
}
