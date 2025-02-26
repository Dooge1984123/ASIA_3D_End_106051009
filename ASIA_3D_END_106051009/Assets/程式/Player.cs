﻿
using UnityEngine;
using Invector.vCharacterController;

public class Player : MonoBehaviour
{
    private float hp = 100;
    private Animator ani;
    private int atkCount;
    private float timer;
    [Header("聯集間隔時間"), Range(0, 3)]
    public float interval =1;
    [Header("攻擊中心點")]
    public Transform ATKpoint;
    [Header("攻擊長度"), Range(0f, 5f)]
    public float ATKLength;
    [Header("攻擊力"), Range(0, 500)]
    public float atk = 30;
    private RaycastHit hit;
    private void Awake()
    {
        ani = GetComponent<Animator>();
    }

    private void Update()
    {
        Attack();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(ATKpoint.position, ATKpoint.forward * ATKLength);
    }
    private void Attack()
    {
        if (atkCount<3 )
        {

            if (timer < interval)
            {
                timer += Time.deltaTime;

                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    atkCount++;
                    timer = 0;
                    
                    if (Physics.Raycast(ATKpoint.position, ATKpoint.forward, out hit, ATKLength, 1 << 9))
                    {
                        hit.collider.GetComponent<Enemy>().Damage(atk);
                    }

                }
            }
            else
            {
                atkCount = 0;
                timer = 0;
            }
        }

        if (atkCount == 3) atkCount = 0;
       
        ani.SetInteger("combo", atkCount);

    }
    public void Damage(float damage)
    {
        hp -= damage;
        ani.SetTrigger("受傷觸發");

        if (hp<=0 )       
            Dead();
        
    }

    private void Dead()
    {
        ani.SetTrigger("死亡");
       vThirdPersonController vt=  GetComponent<vThirdPersonController>();
        vt.lockMovement = true;
        vt.lockRotation = true;
    }
}
