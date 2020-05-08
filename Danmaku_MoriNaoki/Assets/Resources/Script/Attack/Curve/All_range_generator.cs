using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class All_range_generator : Barrage_generator
{
	private float default_dir;

	void Start(){
		default_dir = direction;
	}

	protected override void Bullet_init(GameObject obj){
		obj.GetComponent<Bullet>().Set_property(position, direction, speed);
	}

	public void SetStatus(float angle_gap, int i){
		direction = default_dir + angle_gap * i;
	}
}
