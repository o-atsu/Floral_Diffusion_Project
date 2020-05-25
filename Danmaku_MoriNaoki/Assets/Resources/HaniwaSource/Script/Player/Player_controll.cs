using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_controll : MonoBehaviour
{
    private int init_hp;
    private int init_bomb;
    private int hp;
    private int bomb;

    float speed;
    float slow_speed;
    float actual_speed;
    int push_count;
    Rigidbody2D rb;

    float min_x = -5.6f;
    float max_x = 2.1f;
    float min_y = -4.25f;
    float max_y = 4.65f;

    private float invincible_time=11f;//定数
    private float  invincible_count=-2f;

    private float damaging_time = 3.6f;//定数
    private float damaging_count = -2f;
    private float damaging_move_count = 2f;

    private AudioSource audiosource;


    // Start is called before the first frame update
    void Start()
    {
        Init(this.transform.position.x, this.transform.position.y, 5.0f, 2.5f);
        rb = this.gameObject.GetComponent<Rigidbody2D>();

        init_hp = 4;
        init_bomb = 3;
        hp = init_hp;
        bomb = init_bomb;

        Application.targetFrameRate = 60;

        if (GetComponent<AudioSource>())
        {
            audiosource = GetComponent<AudioSource>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(invincible_count>=-1)invincible_count-=Time.deltaTime;
        if (damaging_count >= -0.05)
        {
            damaging_count -= Time.deltaTime;
            float level = Mathf.Abs(Mathf.Sin(Time.time * 7));
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, level);
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        }

    }

    private void FixedUpdate()
    {

        if(damaging_move_count>=0.98)PlayerMove();
    }

    private void SetInvincible()
    {
        invincible_count = invincible_time;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (invincible_count <= 0 && damaging_count<=0)
        {
            if (collision.gameObject.tag == "Enemy_Bullet")
            {
                EndPhase.CountMiss(); // フェイズ毎の評価用にミス数をカウントする
//              invincible_count = invincible_frame;
                PlayerDamaged();
                collision.gameObject.SetActive(false);
            }
            else if (collision.gameObject.tag == "Enemy_Bullet_Not_Delete")
            {
                PlayerDamaged();
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

    private void PlayerDamaged()
    {
        
        hp--;
        bomb = init_bomb;
        damaging_count = damaging_time;
        audiosource.Play();
        StartCoroutine("ReturnField");
    }

    public void Init(float x,float y,float init_speed,float init_slow_speed)
    {
        this.gameObject.transform.position = new Vector3(x, y, 0.0f);
        speed = init_speed;
        slow_speed = init_slow_speed;
    }

    public int GetLifeCount()
    {
        return hp;
    }

    public int GetBombCount()
    {
        return bomb;
    }

    public void DecreaseBomb()
    {
        EndPhase.CountBomb(); // フェイズ毎の評価用にボム数をカウントする
        bomb--;
    }

    private IEnumerator ReturnField()
    {
        min_y = -10f;
        this.transform.position = new Vector3(-2f, -6f, 0f);
        rb.velocity = new Vector3(0f, 0f, 0f);
        damaging_move_count = 0;
        while (damaging_move_count <= 1f)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(-2.0f, -3f, 0f), damaging_move_count*0.2f);
            damaging_move_count += 0.8f * Time.deltaTime;
            yield return null;
        }
        min_y = -4.25f;
        yield break;
    }

    public float Get_Damaging_Move_Count()
    {
        return damaging_move_count;
    }

}
