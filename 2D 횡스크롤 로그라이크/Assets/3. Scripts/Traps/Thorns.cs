using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thorns : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            Player player = col.GetComponent<Player>();
            player.status.nowHp -= 10;
            if (player.status.nowHp < 0) player.status.nowHp = 0;
        }
    }
}
