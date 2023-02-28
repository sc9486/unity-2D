using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemBlue : MonoBehaviour
{
    // 10초간 이동속도 100% 증가 
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
        {
            Sword_Man swordMan = col.GetComponent<Sword_Man>();
            StartCoroutine(IncreaseMoveSpeed(swordMan));
            
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;
        }
    }

    IEnumerator IncreaseMoveSpeed(Sword_Man swordMan)
    {
        float moveSpeed = swordMan.GetMoveSpeed();
        swordMan.SetMoveSpeed(moveSpeed * 2f);

        yield return new WaitForSeconds(10);

        swordMan.SetMoveSpeed(moveSpeed);
        Destroy(gameObject);
    }
}