using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Straight : Bullet
{

	private Rigidbody2D rb;
	void Awake(){
		rb = GetComponent<Rigidbody2D>();
	}

	public override void Set_property(Vector2 pos, float dir, float spd){
		Quaternion rot = Quaternion.AngleAxis(dir - 90f, Vector3.forward);

		transform.localPosition = new Vector3(pos.x, pos.y, 0f);
		transform.rotation = rot;
		rb.velocity = transform.eulerAngles * spd;
	}

}
