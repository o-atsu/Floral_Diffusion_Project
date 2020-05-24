using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectBarrageGenerator : Barrage_generator
{
    public int reflect_time;

    private void Awake()
    {
        
    }

    private void OnEnable()
    {
        pool = GameObject.Find("ReflectObPool").GetComponent<Object_pool>();
    }

    protected override void Bullet_init(GameObject obj)
    {
        position = new Vector2(this.gameObject.transform.position.x, this.gameObject.transform.position.y);
        Reflect_Bullet reflect_bullet = obj.GetComponent<Reflect_Bullet>();
        reflect_bullet.Init_Property(position, direction, speed, reflect_time);
    }

}
