using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Transform canvas;

    RectTransform hpBarTf;

    public GameObject prfHpBar;
    
    Image nowHpbar;

    UnitMgr unitMgr;
    Status status;

    public Vector3 hpBarOffset = new Vector3(0, 1.7f, 0);

    Vector3 hpBarPos;
    public Vector3 hpBarScale = new Vector3(1, 1, 1);
    void Start()
    {
        hpBarTf = Instantiate(prfHpBar, canvas).GetComponent<RectTransform>();
        nowHpbar = hpBarTf.transform.GetChild(0).GetComponent<Image>();
        unitMgr = GetComponent<UnitMgr>();
        status = unitMgr.status;
    }

    void LateUpdate()
    {
        if (unitMgr.died)
        {
            Destroy(hpBarTf.gameObject);
            Destroy(this);
        }

        hpBarPos = Camera.main.WorldToScreenPoint(transform.position + hpBarOffset);
        hpBarTf.position = hpBarPos;
        hpBarTf.localScale = hpBarScale;

        nowHpbar.fillAmount = (float)status.nowHp / status.maxHp;
    }

    private void OnDisable()
    {
        if (hpBarTf != null)
            hpBarTf.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        if(hpBarTf != null)
            hpBarTf.gameObject.SetActive(true);
    }
}
