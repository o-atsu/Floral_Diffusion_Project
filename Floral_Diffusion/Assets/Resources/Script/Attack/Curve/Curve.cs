﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curve : Bullet
{
	[SerializeField]
	protected float curve_level = 0.1f;

	private Rigidbody2D rb;
	void Awake(){
		rb = GetComponent<Rigidbody2D>();
	}

	public override void Set_property(Vector2 pos, float dir, float spd){
		transform.localPosition = new Vector3(pos.x, pos.y, 0f);
		transform.rotation = Quaternion.AngleAxis(dir - 90f, Vector3.forward);
		rb.velocity = transform.up * spd;
	}

	void Update(){
		transform.Rotate(0, 0, curve_level);
		rb.velocity = transform.up * rb.velocity.magnitude;
	}
}
