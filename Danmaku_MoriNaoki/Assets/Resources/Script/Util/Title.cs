using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : SceneChange
{
	void Start(){
		StartCoroutine("Load_scene");
	}
}
