using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A_R_move8 : All_range_generator
{
	[SerializeField]
	private float height = 1f;
	[SerializeField]
	private float width = 1f;
	[SerializeField]
	private float move_speed = 2f;

	private float deg = 90f * Mathf.Deg2Rad;
	
	void Start(){
        position = new Vector2(0.1f * width * Mathf.Sin(deg), 0.1f * height * Mathf.Cos(2*deg));
	}
    void FixedUpdate()
    {
        position += new Vector2(0.1f * width * Mathf.Sin(deg), 0.1f * height * Mathf.Cos(2*deg));
		deg += move_speed * Mathf.Deg2Rad;

		default_dir += 5f;
    }
}
