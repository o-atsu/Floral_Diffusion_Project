using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroy : MonoBehaviour
{
	private static string[] destroys = new string[]{"Title", "Result"};

	void Awake(){
		Conf_destroy();
		DontDestroyOnLoad(gameObject);
	}
	void Start(){
		SceneManager.sceneLoaded += SceneLoaded;
	}

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= SceneLoaded;
    }

    void Conf_destroy(){
		string sname = SceneManager.GetActiveScene().name;
		Debug.Log(sname);
		foreach(string i in destroys){
			if(sname.Equals(i)){
				Destroy(this.gameObject);
			}
		}
	}

	void SceneLoaded(Scene nextScene, LoadSceneMode mode){
		Conf_destroy();
	}
}
