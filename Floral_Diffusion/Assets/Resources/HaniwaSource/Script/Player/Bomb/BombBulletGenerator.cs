using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBulletGenerator : Barrage_generator
{
    public float direct_speed;
    protected override void Bullet_init(GameObject obj)
    {
        BombBullet bomb = obj.GetComponent<BombBullet>();
        float dir = Random.Range(0f, 1f) * 360f;
        bomb.Set_Direction(direction);
        bomb.Init();
        //bomb.Set_Direction(dir);
        bomb.Set_Direction_Change(direct_speed);
        obj.transform.localPosition = Vector3.zero;
    }

    public void SetStatus(float set_direct, float set_dir_speed)
    {
        direction = set_direct;
        direct_speed = set_dir_speed;
        speed = 0f;
    }

}
