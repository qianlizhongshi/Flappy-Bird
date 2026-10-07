using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player : MonoBehaviour
{
    public Rigidbody2D RigidBodyBird;
    public Animator ani;
    public float force = 100f;
    private bool death = false;
    public bool canJump = false;
    public delegate void DeathNoitfy();
    public event DeathNoitfy OnDeath;
    private Vector3 initPos;
    void Start()
    {
        initPos = this.transform.position;
        this.Idle();
    }
    public void Init()
    {
        this.transform.position = initPos;
        this.Idle();
    }
    void Update()
    {
        if (this.death)
            return;
        if (canJump && Input.GetMouseButtonDown(0))
        {
            RigidBodyBird.velocity = Vector2.zero;
            RigidBodyBird.AddForce(new Vector2(0, force), ForceMode2D.Force);
        }
    }
    public void Idle()
    {
        this.RigidBodyBird.simulated = false;
        this.ani.SetTrigger("Idle");
        this.death = false;
    }
    public void Fly()
    {
        this.RigidBodyBird.simulated = true;
        this.ani.SetTrigger("Fly");
    }
    public void Die()
    {
        if (death) return;
        this.death = true;
        OnDeath?.Invoke();
    }

    // 只保留触发器检测，增加Tag过滤！
    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log(col.gameObject.name + ":" + gameObject.name + ":" + Time.time);
        // ✅ 仅当碰撞物体Tag是Pipe（水管、地面）才死亡
        if (col.CompareTag("Pipe"))
        {
            this.Die();
        }
        // Untagged的ScoreArea不会进入Die逻辑，直接跳过
    }

    // 必须删除旧的实体碰撞函数
    // private void OnCollisionEnter2D(Collision2D col)
    // {
    //     Debug.Log("OnCollisionEnter2D :" + col.gameObject.name + ":" + gameObject.name + ":" + Time.time);
    //     this.Die();
    // }
}
