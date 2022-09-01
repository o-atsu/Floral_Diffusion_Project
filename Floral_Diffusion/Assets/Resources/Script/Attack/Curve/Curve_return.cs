using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curve_return : Curve
{
	private float default_curve;

    void Start(){
		default_curve = curve_level;
		StartCoroutine("curve_ret");
	}
	
	IEnumerator curve_ret(){
		while(true){
			curve_level *= 0.9f;
			yield return new WaitForSeconds(0.3f);
		}
	}

	void OnDisable(){
		curve_level = default_curve;
	}
}
