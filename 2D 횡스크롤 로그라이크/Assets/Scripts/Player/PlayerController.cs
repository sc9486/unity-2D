using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    SpriteRenderer rend;
    Animator animator;
    UnitMgr unitMgr;
    Rigidbody2D myrigidbody;

    [SerializeField]
    float _speed = 5.0f;

    void Start()
    { 
        Managers.Input.KeyAction -= OnKeyboard; // 어떠한 키가 눌리면 함수 실행
        Managers.Input.KeyAction += OnKeyboard;
        animator = GetComponent<Animator>();
        myrigidbody = GetComponent<Rigidbody2D>();
        rend = GetComponent<SpriteRenderer>();
        unitMgr = GetComponent<UnitMgr>();
        animator = unitMgr.animator;
    }

    void Update()
    {
    }

    void OnKeyboard()
    {
        if (unitMgr.died) return;

        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * Time.deltaTime * _speed);
            rend.flipX = true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * Time.deltaTime * _speed);
            rend.flipX = false;
        }
        if (Input.GetMouseButton(0) && !animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            animator.SetTrigger("attack");
            SFXManager.Instance.PlaySound(SFXManager.Instance.playerAttack);
        }
    }
}
