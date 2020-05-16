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
		string[] destroys = new string[]{"Title", "Result"};
		string sname = SceneManager.GetActiveScene().name;
		foreach(string i in destroys){
			if(sname.Equals(i)){
				DestroyImmediate(gameObject);
			}
		}
		enemy = GameObject.FindWithTag("Enemy").GetComponent<Enemy>();
		int phase = enemy.Get_phase();
		objects = new GameObject[phase];
		for(int i = 0;i < phase;i++){
			Vector2 pos = new Vector2(210f, 690f - i * SPACE);
			objects[i] = Instantiate(img_object, pos, Quaternion.identity, transform);
		}
	}

	void Awake(){
		Init();
		DontDestroyOnLoad(gameObject);
	}

	void SceneLoaded(){
		Init();
	}

	void FixedUpdate(){
		int phase = enemy.Get_phase();
		if(phase == objects.Length) return;
		if(objects[phase].activeSelf) objects[phase].SetActive(false);
	}
}
