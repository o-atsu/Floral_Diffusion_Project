using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{

    Rigidbody2D rb2D;
    float speed;
    float direction;


    // Start is called before the first frame update
    void Start()
    {
        rb2D = this.gameObject.GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void ChangeSize(float size)
    {
        this.transform.localScale = new Vector3(size, size, size);
    }

    public void ChangeSpeed(float new_speed)
    {
        speed = new_speed;
        rb2D.velocity = new Vector2(speed * Mathf.Cos(direction), speed * Mathf.Sin(direction));
    }

    public void ChangeDirection(float new_direction)
    {
        direction = new_direction;
        rb2D.velocity = new Vector2(speed * Mathf.Cos(direction), speed * Mathf.Sin(direction));
    }

    public void Init(float init_x, float init_y, float init_speed,float init_direction,float init_size ,int init_kind , Vector3 color)
    {
        this.gameObject.transform.position = new Vector3(init_x, init_y, 0f);
        
        ChangeSpeed(init_speed);
        ChangeSpeed(init_direction);
        ChangeSize(init_size);

    }



}
