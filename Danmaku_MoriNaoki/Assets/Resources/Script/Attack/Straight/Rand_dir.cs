using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rand_dir : Barrage_generator
{
	void Awake(){
		Create_pool();
		Random.InitState(System.DateTime.Now.Millisecond);
	}
	protected override void Bullet_init(ref GameObject obj){
		direction = Random.Range (0f, 360f);
		obj.GetComponent<Bullet>().Set_property(position, direction, speed);

	}
}
