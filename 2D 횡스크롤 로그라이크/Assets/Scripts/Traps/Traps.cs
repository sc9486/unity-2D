using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thorns : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            UnitMgr Player = col.GetComponent<UnitMgr>();
            Player.status.nowHp -= 10;

            if (Player.status.nowHp < 0)
            {
                Player.status.nowHp = 0;
            }
        }
    }
}
