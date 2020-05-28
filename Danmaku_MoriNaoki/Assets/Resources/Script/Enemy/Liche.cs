using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Liche : Enemy
{
	protected override IEnumerator Defeated(){
		if(phase == 0) yield break;

		attack_each_phase[attack_each_phase.Length - phase].SetActive(false);
		phase--;
		if(phase == 0){
			on_defeated.transform.parent = null;
			on_defeated.SetActive(true);
			yield return new WaitForSeconds(0.2f);
			gameObject.SetActive(false);
			Debug.Log("Liche:Defeated!");
			yield break;
		}

		hp = MAX_HP;
		attack_each_phase[attack_each_phase.Length - phase].SetActive(true);
	}

}
