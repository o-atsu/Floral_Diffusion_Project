using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Show_phase : MonoBehaviour
{
	[SerializeField]
	private GameObject img_object;

	private const float SPACE = 40f;

	private Enemy enemy;
	private GameObject[] objects;

	private void Init(){
		enemy = GameObject.FindWithTag("Enemy").GetComponent<Enemy>();
		int phase = enemy.Get_phase();
		objects = new GameObject[phase];
		for(int i = 0;i < phase;i++){
			Vector2 pos = new Vector2(210f, 690f - i * SPACE);
			objects[i] = Instantiate(img_object, pos, Quaternion.identity, transform);
		}
	}

	void Start(){
		string sname = SceneManager.GetActiveScene().name;
		if(sname!="Title"&&sname!="Result"){
			Init();
		}
		SceneManager.sceneLoaded += SceneLoaded;
	}

	void SceneLoaded(Scene nextScene, LoadSceneMode mode){
		string sname = SceneManager.GetActiveScene().name;
		if(sname!="Title"&&sname!="Result"){
			Init();
		}
	}

	void FixedUpdate(){
		int phase = enemy.Get_phase();
		if(phase == objects.Length) return;
		if(objects[phase].activeSelf) objects[phase].SetActive(false);
	}

	void OnDestroy(){
		objects = new GameObject[0];
	}
}
