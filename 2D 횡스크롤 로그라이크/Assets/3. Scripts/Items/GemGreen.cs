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
            Player player = col.GetComponent<Player>();
            StartCoroutine(IncreaseAttackSpeed(player));

            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;
        }
    }

    IEnumerator IncreaseAttackSpeed(Player player)
    {
        float attackSpeed = player.GetAttackSpeed();
        player.SetAttackSpeed(attackSpeed * 1.5f);

        yield return new WaitForSeconds(10);

        player.SetAttackSpeed(attackSpeed);
        Destroy(gameObject);
    }
}
