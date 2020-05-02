using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Straight_generator : Barrage_generator
{
	protected override void Bullet_init(ref GameObject obj){
		obj.GetComponent<Bullet>().Set_property(position, direction, speed);
	}
}
