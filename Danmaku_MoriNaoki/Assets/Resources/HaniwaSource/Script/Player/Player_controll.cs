using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_controll : MonoBehaviour
{

    public int hp;
    private int bomb;

    float speed;
    float slow_speed;
    float actual_speed;
    int push_count;
    Rigidbody2D rb;

    float min_x = -8f;
    float max_x = 8f;
    float min_y = -5f;
    float max_y = 5f;

    private int invincible_frame=200;//定数
    private int  invincible_count=0;


    // Start is called before the first frame update
    void Start()
    {
        Init(0.0f, 0.0f, 5.0f, 2.5f);
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        hp = 4;
        bomb = 2;
    }

    // Update is called once per frame
    void Update()
    {
        invincible_count--;
    }

    private void FixedUpdate()
    {

        PlayerMove();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (invincible_count <= 0)
        {
            if (collision.gameObject.tag == "Enemy_Bullet")
            {
                hp--;
                invincible_count = invincible_frame;
            }
        }

    }

    void PlayerMove()
    {
        push_count = 0;
        if (Input.GetKey(KeyCode.LeftArrow)) push_count++;
        if (Input.GetKey(KeyCode.RightArrow)) push_count++;
        if (Input.GetKey(KeyCode.UpArrow)) push_count++;
        if (Input.GetKey(KeyCode.DownArrow)) push_count++;

        if (push_count == 0) actual_speed = 0;
        else if (Input.GetKey(KeyCode.LeftShift)) actual_speed = slow_speed / Mathf.Sqrt((float)push_count);
        else actual_speed = speed / Mathf.Sqrt((float)push_count);

        rb.velocity = new Vector2(0.0f, 0.0f);
        if (Input.GetKey(KeyCode.LeftArrow)) rb.velocity += new Vector2(-1 * actual_speed, 0.0f);
        if (Input.GetKey(KeyCode.RightArrow)) rb.velocity += new Vector2(actual_speed, 0.0f);
        if (Input.GetKey(KeyCode.UpArrow)) rb.velocity += new Vector2(0.0f, actual_speed);
        if (Input.GetKey(KeyCode.DownArrow)) rb.velocity += new Vector2(0.0f, -1 * actual_speed);

        if (this.transform.position.x < min_x && rb.velocity.x<0f) rb.velocity = new Vector2(0.0f, rb.velocity.y);

        if (this.transform.position.x > max_x && rb.velocity.x > 0f) rb.velocity = new Vector2(0.0f, rb.velocity.y);
        if (this.transform.position.y < min_y && rb.velocity.y<0f) rb.velocity = new Vector2(rb.velocity.x, 0.0f);
        if (this.transform.position.y > max_y && rb.velocity.y>0f) rb.velocity = new Vector2(rb.velocity.x, 0.0f);
    }

    public void Init(float x,float y,float init_speed,float init_slow_speed)
    {
        this.gameObject.transform.position = new Vector3(x, y, 0.0f);
        speed = init_speed;
        slow_speed = init_slow_speed;
    }

}
