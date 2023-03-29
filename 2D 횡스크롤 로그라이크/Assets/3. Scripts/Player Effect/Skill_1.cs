using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_1 : MonoBehaviour {

    public float movingSpeed = 4f;
    public float SmoothDamp = 1f;
    // public float destroyTime = 3f;

    public bool ISMon = true;

    public float Damage = 0;
    public Vector3 TargetDir;
    //  public Vector3 TartPos;

    public GameObject TartgetObj;

    // public Vector3 TargetPos;

    private Rigidbody2D m_rigidbody;

    bool m_bUpdateCheck = true;
    void Awake()
    {
        m_rigidbody = this.transform.GetComponent<Rigidbody2D>();

       
    }

    public void Fire( float damage)
    {

        Damage = damage;
        TartgetObj = Demo_GM.Gm.CurrentPlayerObj;
        TargetDir = TartgetObj.transform.position - this.transform.position;
        this.transform.right = TargetDir;

    }

    private float UpdateTic = 0;

    void Update()
    {

        //타겟 동기화는 0.5초에 한번씩 해준다. 
        smoothMove();
    }

    private Vector2 Velocity = Vector2.zero;
    void smoothMove()
    {
        //회전

        if (TartgetObj == null)
        {
            Destroy(this.gameObject);
            return;

        }
     


        TargetDir = TartgetObj.transform.position - this.transform.position;
        this.transform.right = Vector2.SmoothDamp(this.transform.right, TargetDir, ref Velocity, SmoothDamp);
        this.transform.Translate(Vector2.right * Time.deltaTime * movingSpeed);

    }





    Collider2D[] colliderpoint = new Collider2D[1];
    void OnTriggerEnter2D(Collider2D other)
    {
       

        if (other.CompareTag("Player"))
        {



            //   Debug.Log("맞음" + Damage);
            if (other.CompareTag("Player")) // 맞는 처리는 서버에서만 보내준다. 
            {
                Destroy(this.gameObject);
                
            }


        }




    }


 

}
