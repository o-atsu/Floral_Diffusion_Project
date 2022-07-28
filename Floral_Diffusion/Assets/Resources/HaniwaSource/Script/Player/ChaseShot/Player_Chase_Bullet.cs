using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Chase_Bullet : Bullet
{
    private Rigidbody2D rb;
    float conv_rad = Mathf.PI / 180f;
    float conv_dierect = 180f / Mathf.PI;
    public float max_angle_speed;
    private float speed;
    public int power;
    

    private int missing_count = 0;
    private float pre_diret = 0; 
    private float now_diret = 0; 


    private GameObject targetOb;

    void Awake()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        StartCoroutine("check_in_screen");
    }

    private void OnEnable()
    {
        missing_count = 0;
    }

    private void Update()
    {
        CheckMissing();
    }

    private void FixedUpdate()
    {
        targetOb = SerchEnemy();
        if(targetOb!=null)if (targetOb.activeInHierarchy == true) Chase();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Enemy enemy_script = collision.gameObject.GetComponent<Enemy>();
            if (Player_controll.invincible_count >= 0.1f && enemy_script.Get_phase() == 1 && SceneManager.GetActiveScene().name == "Zone_E")
            {
                return;
            }
            enemy_script.Hit(power);
            this.gameObject.SetActive(false);
        }

        if (collision.gameObject.tag == "Enemy_Bullet_Invincible")
        {
            this.gameObject.SetActive(false);
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
        now_diret = dir*conv_dierect;
    }

    private void CheckMissing()
    {

        if (Mathf.Abs(now_diret - pre_diret)>150f) missing_count++;
        else missing_count = 0;

        if (missing_count > 3)
        {
            this.gameObject.SetActive(false);
        }

        pre_diret = now_diret;

    }

}
