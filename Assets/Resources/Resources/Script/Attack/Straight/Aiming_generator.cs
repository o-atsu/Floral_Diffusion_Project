using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aiming_generator : Barrage_generator
{
	private GameObject target;

	void Start(){
		target = GameObject.FindWithTag("Player");
	}

	protected override void Bullet_init(ref GameObject obj){
        if(target==null)target = GameObject.FindWithTag("Player");
        Vector2 tmp = new Vector2(target.transform.position.x - transform.position.x - position.x, target.transform.position.y - transform.position.y - position.y);
		direction = Mathf.Atan2(tmp.y, tmp.x) * Mathf.Rad2Deg;
		obj.GetComponent<Bullet>().Set_property(position, direction, speed);

	}
}
