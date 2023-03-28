using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemRed : MonoBehaviour
{
    // ü�� 50 ȸ��
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            Sword_Man swordMan = col.GetComponent<Sword_Man>();
            swordMan.status.nowHp += 50;
            if (swordMan.status.nowHp > swordMan.status.maxHp) 
                swordMan.status.nowHp = swordMan.status.maxHp;
            Destroy(gameObject);
        }
    }
}