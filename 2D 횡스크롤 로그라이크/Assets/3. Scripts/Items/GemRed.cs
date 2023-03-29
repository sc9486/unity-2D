using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemRed : MonoBehaviour
{
    // ü�� 50 ȸ��
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("player"))
        {
            Player player = col.GetComponent<Player>();
            player.status.nowHp += 50;
            if (player.status.nowHp > player.status.maxHp) 
                player.status.nowHp = player.status.maxHp;
            Destroy(gameObject);
        }
    }
}