﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rand_dir : Barrage_generator
{
	void Start(){
		Random.InitState(System.DateTime.Now.Millisecond);
	}
	protected override void Bullet_init(GameObject obj){
		direction = Random.Range (0f, 360f);
		obj.GetComponent<Bullet>().Set_property(position, direction, speed);

	}
}
