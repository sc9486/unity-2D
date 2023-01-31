using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UnitMgr : MonoBehaviour
{
    public Animator animator;
    [HideInInspector]
    public Rigidbody2D rigid2D;
    [HideInInspector]
    public bool died = false;
    [HideInInspector]
    public Collider2D col2D;
    [HideInInspector]
    public bool attacked = false;

    public Status status;

    public UnitCode unitCode;

    void Start()
    {
        rigid2D = GetComponent<Rigidbody2D>();
        col2D = GetComponent<Collider2D>();

        // 스테이터스 설정
        status = new Status();
        status = status.SetUnitStatus(unitCode);
        SetAttackSpeed(status.atkSpeed);
        StartCoroutine(CheckDied());
    }

    IEnumerator CheckDied()
    {
        while(true)
        {
            // 땅 밑으로 떨어졌다면
            if(transform.position.y < -8)
            {
                if (unitCode.Equals(UnitCode.Player))
                    SceneManager.LoadScene("Level 1"); // Scene 재시작
                else Destroy(gameObject);
            }

            // 체력이 0이하일 때
            if (status.nowHp <= 0)
            {
                died = true;
                animator.SetTrigger("die");
                Destroy(rigid2D);
                Destroy(col2D);
                yield return new WaitForSeconds(2); // 2초 기다리기
                if (unitCode.Equals(UnitCode.Player))
                    SceneManager.LoadScene("Level 1"); // Scene 재시작
                else Destroy(gameObject);
            }
            yield return new WaitForEndOfFrame(); // 매 프레임의 마지막 마다 실행
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (tag.Equals("Enemy"))
        {
            if (col.CompareTag("Player"))
            {
                UnitMgr _unitMgr = col.GetComponentInParent<UnitMgr>();
                if (_unitMgr.attacked)
                {
                    status.nowHp -= _unitMgr.status.atkDmg;
                    _unitMgr.attacked = false;
                }
            }
        }
    }

    #region properties

    public void SetAttackSpeed(float speed)
    {
        animator.SetFloat("attackSpeed", speed);
        status.atkSpeed = speed;
    }

    public float GetAttackSpeed()
    {
        return status.atkSpeed;
    }

    public void SetMoveSpeed(float speed)
    {
        status.moveSpeed = speed;
    }
    public float GetMoveSpeed()
    {
        return status.moveSpeed;
    }

    void AttackTrue()
    {
        attacked = true;
    }
    void AttackFalse()
    {
        attacked = false;
    }

    #endregion

}
