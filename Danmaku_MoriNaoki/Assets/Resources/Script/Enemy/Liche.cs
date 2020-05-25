using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Liche : Enemy
{
	protected override void Defeated(){
		if(phase == 0) return;

		attack_each_phase[attack_each_phase.Length - phase].SetActive(false);
		phase--;
		if(phase == 0){
			gameObject.SetActive(false);
			Debug.Log("Liche:Defeated!");
			return;
		}

		hp = MAX_HP;
		attack_each_phase[attack_each_phase.Length - phase].SetActive(true);
	}

}
