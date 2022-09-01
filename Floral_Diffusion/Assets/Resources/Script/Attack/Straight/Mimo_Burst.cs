using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mimo_Burst : Attack
{
	[SerializeField]
	private int burst_num = 16;
	[SerializeField]
	private Barrage_generator[] generators;
    protected override IEnumerator shot(){
		yield return new WaitForSeconds(4.5f);
		while(true){
			for(int i = 0;i < burst_num;i++){
				foreach(Barrage_generator k in generators){
					k.Generate();
				}
			}
			yield return new WaitForSeconds(3.6f);
			for(int i = 0;i < burst_num;i++){
				foreach(Barrage_generator k in generators){
					k.Generate();
				}
			}
			yield return new WaitForSeconds(3.25f);
			for(int i = 0;i < burst_num;i++){
				foreach(Barrage_generator k in generators){
					k.Generate();
				}
			}
			yield return new WaitForSeconds(3.35f);
		}
	}

	void Start(){
		StartCoroutine("shot");
	}
}
