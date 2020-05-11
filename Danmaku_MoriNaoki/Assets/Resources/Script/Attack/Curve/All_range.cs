using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class All_range : Attack
{
	[SerializeField]
	protected All_range_generator generator;
	[SerializeField]
	protected float angle_gap = 0f;
	[SerializeField]
	protected int shot_time = 2;

	protected override IEnumerator shot(){
		while(true){
			if(shot_time % 2 == 1){
				generator.SetStatus(angle_gap, 0);
				generator.Generate();
			}
			for(int i = 1;i <= shot_time / 2;i++){
				generator.SetStatus(angle_gap, i);
				generator.Generate();
				generator.SetStatus(angle_gap, -i);
				generator.Generate();
			}
		yield return new WaitForSeconds(interval);
		}
	}

	void Start(){
		StartCoroutine("shot");
	}
}
