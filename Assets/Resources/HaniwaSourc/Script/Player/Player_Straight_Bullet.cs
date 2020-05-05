using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Straight_Bullet : Bullet
{
    private Rigidbody2D rb;
    float conv_direct = Mathf.PI / 180f;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public override void Set_property(Vector2 pos, float dir, float spd)
    {
        transform.localPosition = new Vector3(pos.x, pos.y, 0f);
        transform.rotation = Quaternion.AngleAxis(dir , Vector3.forward);
        rb.velocity = new Vector2(Mathf.Cos(dir*conv_direct),Mathf.Sin(dir*conv_direct)) * spd;
    }
}
