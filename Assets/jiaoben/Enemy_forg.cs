using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_forg : Enemy
{
    private Rigidbody2D rb;
    private new Animator Anim;
    private Collider2D Coll;
    public LayerMask Ground;

    public Transform leftpoint;
    public Transform rightpoint;
    public float Speed;
    public float JumpFore;
    private float leftx, rightx;

    private bool Faceleft = true;

    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();

        Anim = GetComponent<Animator>();
        Coll = GetComponent<Collider2D>();

        transform.DetachChildren();
        leftx = leftpoint.position.x;
        rightx = rightpoint.position.x;
        Destroy(leftpoint.gameObject);
        Destroy(rightpoint.gameObject);
    }


    void Update()
    {
        SwitchAnim();
    }

    //�����ƶ�
    void Movement()
    {
        //�����������
        if (Faceleft)
        {
            if (Coll.IsTouchingLayers(Ground))
            {
                Anim.SetBool("jumping", true);
                rb.velocity = new Vector2(-Speed, JumpFore);
            }
            //��������left��ͷ
            if (transform.position.x < leftx)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                Faceleft = false;
            }
        }
        //���������Ҳ�
        else
        {
            if (Coll.IsTouchingLayers(Ground))
            {
                Anim.SetBool("jumping", true);
                rb.velocity = new Vector2(Speed, JumpFore);
            }
            //�����Ҳ��right��ͷ
            if (transform.position.x > rightx)
            {
                transform.localScale = new Vector3(1, 1, 1);
                Faceleft = true;
            }
        }
    }

    //���˶���
    void SwitchAnim()
    {
        if (Anim.GetBool("jumping"))
        {
            if (rb.velocity.y < 0.1)
            {
                Anim.SetBool("jumping", false);
                Anim.SetBool("falling", true);
            }
        }
        if (Coll.IsTouchingLayers(Ground) && Anim.GetBool("falling"))
        {
            Anim.SetBool("falling", false);
        }
    }
}
