using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public int maxHp;
    public int nowHp;
    public int atkDmg;
    public bool attacked = false;
    public Image nowHpbar;

    void AttackTrue()
    {
        attacked = true;
    }
    void AttackFalse()
    {
        attacked = false;
    }

    SpriteRenderer rend;
    Animator animator;
    Rigidbody2D myrigidbody;

    [SerializeField]
    float _speed = 10.0f;
    float jump_speed = 50f;


    void Start()
    {
        maxHp = 50;
        nowHp = 50;
        atkDmg = 10;

        Managers.Input.KeyAction -= OnKeyboard; // 어떠한 키가 눌리면 함수 실행
        Managers.Input.KeyAction += OnKeyboard;
        animator = GetComponent<Animator>();
        myrigidbody = GetComponent<Rigidbody2D>();
        rend = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
    }

    void OnKeyboard()
    {

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
        if (Input.GetKey(KeyCode.Space))
        {
            transform.Translate(Vector2.up * Time.deltaTime * jump_speed);
        }
        if (Input.GetMouseButton(0) && !animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            animator.SetTrigger("attack");
        }
    }
}
