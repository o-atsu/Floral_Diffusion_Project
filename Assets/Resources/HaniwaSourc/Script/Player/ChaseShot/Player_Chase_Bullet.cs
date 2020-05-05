using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Chase_Bullet : Bullet
{
    private Rigidbody2D rb;
    float conv_rad = Mathf.PI / 180f;
    float conv_dierect = 180f / Mathf.PI;
    public float max_angle_speed;
    private float speed;

    private GameObject targetOb;

    void Awake()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        StartCoroutine("check_in_screen");
    }

    private void FixedUpdate()
    {
        targetOb = SerchEnemy();
        if (targetOb.activeInHierarchy == true) Chase();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
           
        }
    }

    protected void Chase()
    {
        Vector2 target_position = new Vector2(targetOb.transform.position.x, targetOb.transform.position.y);
        Vector2 dt = target_position - rb.position;
        float target_direction =Mathf.Atan2(dt.y,dt.x);
        SetDirection(target_direction);
    }

    private GameObject SerchEnemy()
    {
        StartCoroutine("check_in_screen");
        float min_dist=100000000;
        GameObject[] enemyOb = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject retunOb=null;
        for (int i = 0; i < enemyOb.Length; i++)
        {
            if (Vector2.Distance(rb.position, enemyOb[i].GetComponent<Rigidbody2D>().position) < min_dist)
            {
                retunOb = enemyOb[i];
                min_dist = Vector2.Distance(rb.position, enemyOb[i].GetComponent<Rigidbody2D>().position);
            }
        }

        return retunOb;
        
    }

    public override void Set_property(Vector2 pos, float dir, float spd)
    {
        speed = spd;
        transform.localPosition = new Vector3(pos.x, pos.y, 0f);
        transform.rotation = Quaternion.AngleAxis(dir, Vector3.forward);
        rb.velocity = new Vector2(Mathf.Cos(dir * conv_rad), Mathf.Sin(dir * conv_rad)) * speed;
    }

    public void SetDirection(float dir)
    {
        transform.rotation = Quaternion.AngleAxis(dir * conv_dierect, Vector3.forward);
        rb.velocity = new Vector2(Mathf.Cos(dir), Mathf.Sin(dir)) * speed;
    }

}
