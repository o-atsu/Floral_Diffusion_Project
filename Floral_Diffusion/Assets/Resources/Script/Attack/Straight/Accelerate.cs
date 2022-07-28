﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accelerate : Straight
{
	[SerializeField]
	private float accel = 1f;

	void Start(){
		LEFT = -10f;
		RIGHT = 7f;
		TOP = 9f;
		BOTTOM = -9f;
	}

    void Update(){
		rb.velocity = new Vector2(rb.velocity.x + transform.up.x*accel, rb.velocity.y + transform.up.y*accel);
	}
}
