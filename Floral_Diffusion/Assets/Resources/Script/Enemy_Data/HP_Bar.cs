﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HP_Bar : MonoBehaviour
{
	private Slider hp_bar;
	private Enemy enemy;

	void Start(){
		hp_bar = GetComponent<Slider>();
		enemy = GameObject.FindWithTag("Enemy").GetComponent<Enemy>();
		SceneManager.activeSceneChanged += SceneChanged;
	}

	void SceneChanged(Scene thisScene, Scene nextScene){
		string sname = SceneManager.GetActiveScene().name;
		if(sname!="Title"&&sname!="Result"){
			enemy = GameObject.FindWithTag("Enemy").GetComponent<Enemy>();
		}
	}

	void FixedUpdate(){
		if(enemy != null){
			if(hp_bar.value<enemy.Get_percent()){
				hp_bar.value += 1;
			} else {
				hp_bar.value = enemy.Get_percent();
			}
		}else{
			hp_bar.value = 0;
		}
	}

	/**** FPS固定、ゲーム開始時どっかから呼び出して ****/
	/*
	void Start(){
		Application.targetFrameRate = 60;
	}
	*/
}
