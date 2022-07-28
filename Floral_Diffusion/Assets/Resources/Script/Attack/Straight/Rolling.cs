using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rolling : Straight
{
	[SerializeField]
	private float rot_speed = 0.5f;
	void Update(){
		transform.Rotate(0f, 0f, rot_speed);
	}
}
