using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dan : MonoBehaviour
{
	private ParticleSystem ps;
	public Transform me; 

	void Awake(){
		ps = GetComponent<ParticleSystem>();
	}

	void Update(){
		ps.transform.Rotate(me.position);
	}
}
