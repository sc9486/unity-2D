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
            Player player = col.GetComponent<Player>();
            StartCoroutine(IncreaseMoveSpeed(player));
            
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;
        }
    }

    IEnumerator IncreaseMoveSpeed(Player player)
    {
        float moveSpeed = player.GetMoveSpeed();
        player.SetMoveSpeed(moveSpeed * 2f);

        yield return new WaitForSeconds(10);

        player.SetMoveSpeed(moveSpeed);
        Destroy(gameObject);
    }
}