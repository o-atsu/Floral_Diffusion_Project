using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Straight_Barrage_generator : Barrage_generator
{

    protected override void Bullet_init(ref GameObject obj)
    {
        obj.GetComponent<Player_Straight_Bullet>().Set_property(position, direction, speed);
    }

    public void SetStatus(float set_x,float set_y,float set_direct,float set_speed)
    {
        position = new Vector2(set_x, set_y);
        direction = set_direct;
        speed = set_speed;
    }


}
