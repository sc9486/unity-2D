﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    float _speed = 10.0f;

    public GameObject prfHpBar;
    public GameObject canvas;
    RectTransform hpBar;
    public float height = 1.7f;

    public Player player;
    Image nowHpbar;
    public Animator enemyAnimator;

    public Status status;
    public UnitCode unitCode;

    void Start()
    {
        hpBar = Instantiate(prfHpBar, canvas.transform).GetComponent<RectTransform>();
        nowHpbar = hpBar.transform.GetChild(0).GetComponent<Image>();

        status = new Status();
        status = status.SetUnitStatus(unitCode); // enemy 마다 선택할 수 있도록
        
        SetAttackSpeed(status.atkSpeed);
    }

    void Update()
    {
        Vector3 _hpBarPos = Camera.main.WorldToScreenPoint
            (new Vector3(transform.position.x, transform.position.y + height, 0));
        hpBar.position = _hpBarPos;
        nowHpbar.fillAmount = (float)status.nowHp / status.maxHp;
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Weapon"))
        {
            if (player.attacked)
            {
                status.nowHp -= player.status.atkDmg;
                player.attacked = false;
                if (status.nowHp <= 0) // 적 사망
                {
                    Die();
                }
            }
            if (player.sting)
            {
                status.nowHp -= player.status.atkDmg * 5;
                player.sting = false;
                if (status.nowHp <= 0) // 적 사망
                {
                    Die();
                }
            }
        }
    }

    void Die()
    {
        enemyAnimator.SetTrigger("die");
        GetComponent<EnemyAI>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        Destroy(GetComponent<Rigidbody2D>());
        Destroy(gameObject, 3);
        Destroy(hpBar.gameObject, 3);
    }

    void SetAttackSpeed(float speed)
    {
        enemyAnimator.SetFloat("attackSpeed", speed);
        status.atkSpeed = speed;
    }
}