using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playeryidong : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    public Collider2D coll;
    public Collider2D DisColl;

    public Transform CellingCheck;

    public AudioSource jumpAudio;

    public Joystick joystick;

    public float speed;
    public float jumpforce;
    public LayerMask ground;
    public int Cheery = 0;

    public Text CherryNumber;
    private bool IsHurt;//默认是false

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (!IsHurt)
        {
            Movement();
        }
        SwitchAnim();
    }

    //调用下蹲,跳跃
    private void Update()
    {
        Crouch();
        Jump();
    }

    //玩家移动
    void Movement()
    {
        float horizontalmpve = Input.GetAxis("Horizontal");
        float facedircetion = Input.GetAxisRaw("Horizontal");
        /* float horizontalmpve = joystick.Horizontal;
         float facedircetion = joystick.Horizontal;*/

        //角色移动
        if (horizontalmpve != 0)
        {
            rb.velocity = new Vector2(horizontalmpve * speed * Time.fixedDeltaTime, rb.velocity.y);
            anim.SetFloat("running", Mathf.Abs(facedircetion));
        }
        if (facedircetion != 0)
        {
            transform.localScale = new Vector3(facedircetion, 1, 1);
        }
        /*if (facedircetion > 0f)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        if (facedircetion < 0f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }*/
    }

    //玩家跳跃
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && coll.IsTouchingLayers(ground))
        /*if (joystick.Vertical > 0.5f && coll.IsTouchingLayers(ground))*/
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpforce * Time.fixedDeltaTime);
            jumpAudio.Play();
            anim.SetBool("jumping", true);
        }
    }

    //玩家下蹲
    void Crouch()
    {
        if (!Physics2D.OverlapCircle(CellingCheck.position, 0.2f, ground))
        {
            if (Input.GetButton("Crouch"))
            /*if(joystick.Vertical<-0.5f)*/
            {
                anim.SetBool("crouching", true);
                DisColl.enabled = false;
            }
            else
            {
                anim.SetBool("crouching", false);
                DisColl.enabled = true;
            }
        }
    }

    //玩家移动，跳跃的动画
    void SwitchAnim()
    {
        //anim.SetBool("idle", false);

        if (rb.velocity.y < 0.01f && !coll.IsTouchingLayers(ground))
        {
            anim.SetBool("falling", true);
        }
        if (anim.GetBool("jumping"))
        {
            if (rb.velocity.y < 0)
            {
                anim.SetBool("jumping", false);
                anim.SetBool("falling", true);
            }
        }
        else if (IsHurt)
        {
            anim.SetBool("hurt", true);
            anim.SetFloat("running", 0);
            if (Mathf.Abs(rb.velocity.x) < 0.1f)
            {
                anim.SetBool("hurt", false);
                //anim.SetBool("idle", true);
                IsHurt = false;
            }
        }
        else if (coll.IsTouchingLayers(ground))
        {
            anim.SetBool("falling", false);
            //anim.SetBool("idle", true);
        }
    }

    //碰撞触发器
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //收集物品
        if (collision.tag == "Collection")
        {
            //Destroy(collision.gameObject);
            //Cheery += 1;
            collision.GetComponent<Animator>().Play("isGot");
            //CherryNumber.text = Cheery.ToString();
        }
        if (collision.tag == "GameOver")
        {
            //停止音乐
            GetComponent<AudioSource>().enabled = false;
            Invoke("Restart", 2f);
        }
    }

    //消灭敌人
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Enemy en = collision.gameObject.GetComponent<Enemy>();
            if (anim.GetBool("falling"))
            {
                en.JumpOn();
                rb.velocity = new Vector2(rb.velocity.x, jumpforce * (Time.fixedDeltaTime / 2));
                anim.SetBool("jumping", true);
            }
            else if (transform.position.x < collision.gameObject.transform.position.x)
            {
                rb.velocity = new Vector2(-5, rb.velocity.y);
                IsHurt = true;
            }
            else if (transform.position.x > collision.gameObject.transform.position.x)
            {
                rb.velocity = new Vector2(5, rb.velocity.y);
                IsHurt = true;
            }
        }
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void CherruCount()
    {
        Cheery++;
        CherryNumber.text = Cheery.ToString();
    }
}
