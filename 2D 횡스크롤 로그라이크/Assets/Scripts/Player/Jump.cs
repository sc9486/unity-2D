using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    public float jumpPower = 45;
    bool inputJump = false;

    UnitMgr unitMgr;
    Rigidbody2D rigid2D;
    Animator animator;
    Collider2D col2D;

    void Start()
    {
        unitMgr = GetComponent<UnitMgr>();
        animator = unitMgr.animator;
        rigid2D = unitMgr.rigid2D;
        col2D = unitMgr.col2D;
    }

    void Update()
    {
        if (unitMgr.died) return;

        if (Input.GetKeyDown(KeyCode.Space) && !animator.GetBool("jumping"))
        {
            inputJump = true;
        }

        RaycastHit2D raycastHit = Physics2D.BoxCast(col2D.bounds.center, col2D.bounds.size, 0f, Vector2.down, 0.02f, LayerMask.GetMask("Ground")); // 8 : Ground

    }

    private void FixedUpdate()
    {
        if (unitMgr.died) return;

        if (inputJump)
        {
            inputJump = false;
            rigid2D.AddForce(Vector2.up * jumpPower);
            //rigid2D.velocity = new Vector2(rigid2D.velocity.x, jumpPower);
        }

        if (rigid2D.velocity.y <= -15) rigid2D.velocity = new Vector2(rigid2D.velocity.x, -15);
    }
}
