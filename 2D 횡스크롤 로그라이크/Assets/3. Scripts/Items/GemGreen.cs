using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemGreen : MonoBehaviour
{
    // 10초간 공속 50% 증가
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
        {
            Sword_Man swordMan = col.GetComponent<Sword_Man>();
            StartCoroutine(IncreaseAttackSpeed(swordMan));

            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;
        }
    }

    IEnumerator IncreaseAttackSpeed(Sword_Man swordMan)
    {
        float attackSpeed = swordMan.GetAttackSpeed();
        swordMan.SetAttackSpeed(attackSpeed * 1.5f);

        yield return new WaitForSeconds(10);

        swordMan.SetAttackSpeed(attackSpeed);
        Destroy(gameObject);
    }
}
