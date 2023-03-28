using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thorns : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            Sword_Man swordMan = col.GetComponent<Sword_Man>();
            swordMan.status.nowHp -= 10;
            if (swordMan.status.nowHp < 0) swordMan.status.nowHp = 0;
        }
    }
}
