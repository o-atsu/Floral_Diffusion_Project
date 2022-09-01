using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A_R_liche : All_range
{
	private float time = 0;

	void Update(){
		interval = 1.5f * Mathf.Sin(time) + 1.7f;
		time += 0.02f;
	}
}
