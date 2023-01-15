using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 1. 위치 벡터
// 2. 방향 벡터

public class PlayerController : MonoBehaviour
{
    SpriteRenderer rend;
    Animator animator;
    Rigidbody2D myrigidbody;

    [SerializeField]
    float _speed = 40.0f;
    float jump_speed = 150f;


    void Start()
    {
        Managers.Input.KeyAction -= OnKeyboard; // 어떠한 키가 눌리면 함수 실행
        Managers.Input.KeyAction += OnKeyboard;
        animator = GetComponent<Animator>();
        myrigidbody = GetComponent<Rigidbody2D>();
        rend = GetComponent<SpriteRenderer>();
    }

    private float curTime;
    public float coolTime = 0.5f;

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
        if (Input.GetMouseButton(0))
        {
            animator.SetBool("attack", true);
        }
        if (Input.GetMouseButtonUp(0))
        {
            animator.SetBool("attack", false);
        }
        else
        {
            curTime -= Time.deltaTime;
        }
    }
}
