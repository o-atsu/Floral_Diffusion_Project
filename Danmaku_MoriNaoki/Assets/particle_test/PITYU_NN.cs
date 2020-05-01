using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PITYU_NN : MonoBehaviour
{
	void OnParticleCollision(GameObject obj){
		Debug.Log("PITYU_NN" + obj.name);
	}
}
