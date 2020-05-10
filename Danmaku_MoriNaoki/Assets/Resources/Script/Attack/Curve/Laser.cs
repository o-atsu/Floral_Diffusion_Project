using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : All_range
{
	[SerializeField]
	private float length = 1f;


	protected override IEnumerator shot(){
		while(true){
			float time = 0;

			while(time < length){
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
				time += Time.deltaTime;
				yield return null;
			}

			yield return new WaitForSeconds(interval);
		}
	}
}
