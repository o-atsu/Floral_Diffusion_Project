using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reflect_Bullet : Bullet
{
    private Rigidbody2D rb;
    float conv_rad = Mathf.PI / 180f;
    float conv_dierect = 180f / Mathf.PI;
    public float direction;
    private float speed;
    private float stay_time = 0f;
    public int power;
    private int refrect_count;
    public int refrect_times; 

    private float r_left;
    private float r_right;
    private float r_top;
    private float r_bottom;
    private Renderer renderer;
    
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        r_left = -6f;
        r_right = 2.5f;
        r_top = 5.1f;
        r_bottom = -4.9f;
    }

    private void Start()
    {

        renderer = this.gameObject.GetComponent<Renderer>();
    }

    private void FixedUpdate()
    {
        if (stay_time > 0) stay_time -= Time.deltaTime;
        Reflect();
        SetColor();
    }

    private void OnEnable()
    {
  
        //refrect_count = refrect_times;
    }

    private void OnDisable()
    {
        StopCoroutine("check_in_screen");
        this.transform.position = Vector2.zero;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }


    public override void Set_property(Vector2 pos, float dir, float spd)
    {
        speed = spd;
        transform.position = new Vector3(pos.x, pos.y, 0f);
        transform.rotation = Quaternion.AngleAxis(dir, Vector3.forward);
        rb.velocity = new Vector2(Mathf.Cos(dir * conv_rad), Mathf.Sin(dir * conv_rad)) * speed;
        direction = dir;
    }

    public void Init_Property(Vector2 pos, float dir, float spd, int refrect_time)
    {
        speed = spd;
        transform.position = new Vector3(pos.x, pos.y, 0f);
        transform.rotation = Quaternion.AngleAxis(dir, Vector3.forward);
        rb.velocity = new Vector2(Mathf.Cos(dir * conv_rad), Mathf.Sin(dir * conv_rad)) * speed;
        direction = dir;
        refrect_times = refrect_time;
        refrect_count = refrect_times;
        StartCoroutine("check_in_screen");
    }

    public void SetDirection(float dir)
    {
        transform.rotation = Quaternion.AngleAxis(dir, Vector3.forward);
        rb.velocity = new Vector2(Mathf.Cos(dir * conv_rad), Mathf.Sin(dir * conv_rad)) * speed;
    }

   private void Reflect()
    {
        if (stay_time <= 0)
        {
            if (refrect_count > 0)
            {
                if (this.transform.position.x <= r_left)
                {
                    direction -= 180f;
                    direction *= -1f;
                    SetDirection(direction);
                    DecreaseRefrectCount();
                }
            }

            if (refrect_count > 0)
            {
                if (this.transform.position.x >= r_right)
                {
                    direction *= -1f;
                    direction += 180f;
                    SetDirection(direction);
                    DecreaseRefrectCount();
                }
            }

            if (refrect_count > 0)
            {
                if (this.transform.position.y >= r_top)
                {
                    direction *= -1f;
                    SetDirection(direction);
                    DecreaseRefrectCount();
                }
            }

            if (refrect_count > 0)
            {
                if (this.transform.position.y <= r_bottom)
                {
                    direction *= -1f;
                    SetDirection(direction);
                    DecreaseRefrectCount();
                }
            }

        }

    }

    private void DecreaseRefrectCount()
    {
        refrect_count--;
        stay_time = 0.1f;
        SetColor();
    }

    private void SetColor()
    {
        if (refrect_count == 2)
        {
            renderer.material.color = Color.green;
        }
        else if (refrect_count == 1)
        {
            renderer.material.color = Color.cyan;
        }
        else if (refrect_count == 0)
        {
            renderer.material.color = Color.white;
        }
    }


}
