using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroy : MonoBehaviour
{
	void Awake(){
		Conf_destroy();
		DontDestroyOnLoad(gameObject);
		SceneManager.sceneLoaded += SceneLoaded;
	}

	void Conf_destroy(){
		string[] destroys = new string[]{"Title", "Result"};

		string sname = SceneManager.GetActiveScene().name;
		Debug.Log(sname);
		foreach(string i in destroys){
			if(sname.Equals(i)){
				DestroyImmediate(gameObject);
			}
		}
	}

	void SceneLoaded(Scene nextScene, LoadSceneMode mode){
		Conf_destroy();
	}
}
