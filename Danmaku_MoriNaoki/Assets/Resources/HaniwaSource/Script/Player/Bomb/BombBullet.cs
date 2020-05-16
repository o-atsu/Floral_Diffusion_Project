using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBullet : Bullet
{
    private Rigidbody2D rb;
    float conv_rad = Mathf.PI / 180f;
    float conv_direct = 180f / Mathf.PI;
    public int power;

    private float radius = 0;
    private float radius_change;
    private float r_c_mentenance=1.0f;
    private float direction = 0f;
    private float direction_change;
    private float d_c_mentenance=1.0f;
    private float first_to_radius = 1.0f;
    private float stay_time=2.5f;
    private Vector3 center_position;

    private bool damage_flag=false;

    public void Init()
    {
        r_c_mentenance = 1.0f;
        d_c_mentenance = 1.0f;
        radius = 0f;
        stay_time = 2.5f;
        damage_flag = false;
        center_position = Vector3.zero;
        first_to_radius = 1.0f + Random.Range(0f, 0.4f);
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        this.transform.position = new Vector3(0f, 0f, 0f);
    }

    private void Start()
    {
        StartCoroutine("check_in_screen");
        Set_Radius(0f);
        Set_Radius_Change(1.2f);
        LEFT -= 15f;
        RIGHT += 15f;
        TOP += 15f;
        BOTTOM -= 15f;
    }

    private void FixedUpdate()
    {
        SetPosition(direction,radius);
        Change_Direction();
        Change_Radius();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Enemy enemy_script = collision.gameObject.GetComponent<Enemy>();
            if (damage_flag == false)
            {
                enemy_script.Hit(power);
                damage_flag = true;
            }
            //this.gameObject.SetActive(false);
        }

        if (collision.gameObject.tag == "Enemy_Bullet")
        {
            collision.gameObject.SetActive(false);
        }

    }


    public override void Set_property(Vector2 pos, float dir, float spd=0)
    {
        this.transform.position = pos;
        direction = dir * conv_rad;
        this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    public void Set_Radius(float radi)
    {
        radius = radi;
    }

    public void Set_Radius_Change(float radius_C)
    {
        radius_change = radius_C;
    }

    public void Set_Direction(float dir)
    {
        direction = dir;
    }

    public void Set_Direction_Change(float dir_C)
    {
        direction_change = dir_C;
    }

    private void Change_Direction()
    {
        direction += direction_change * conv_rad * Time.deltaTime/d_c_mentenance;
        if(stay_time<=0f && d_c_mentenance<2f)
        {
            d_c_mentenance += 1f * Time.deltaTime;
        }
    }

    private void Change_Radius()
    {
        if (radius < first_to_radius)
        {
            radius += radius_change * Time.deltaTime;
        }
        else if (stay_time > 0f)
        {
            stay_time -= Time.deltaTime;
        }
        else
        {
            radius += radius_change * r_c_mentenance * Time.deltaTime;
            r_c_mentenance += 1f * Time.deltaTime;
        }
    }

    public void SetPosition(float dir,float radi)
    {
        if (stay_time > 0f)
        {
            this.transform.localPosition = new Vector3(radi * Mathf.Cos(dir), radi * Mathf.Sin(dir));
            center_position = this.transform.parent.position;
        }
        else
        {
            this.transform.position=center_position + new Vector3(radi * Mathf.Cos(dir), radi * Mathf.Sin(dir));
        }
    }

}
