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
            UnitMgr swordMan = col.GetComponent<UnitMgr>();
            StartCoroutine(IncreaseAttackSpeed(swordMan));

            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;
        }
    }

    IEnumerator IncreaseAttackSpeed(UnitMgr swordMan)
    {
        float attackSpeed = swordMan.GetAttackSpeed();
        swordMan.SetAttackSpeed(attackSpeed * 1.5f);

        yield return new WaitForSeconds(10);

        swordMan.SetAttackSpeed(attackSpeed);
        Destroy(gameObject);
    }
}
