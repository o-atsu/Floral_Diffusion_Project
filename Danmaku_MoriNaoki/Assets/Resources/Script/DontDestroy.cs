using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
		SceneManager.activeSceneChanged += SceneChanged;
    }

	void SceneChanged(Scene thisScene, Scene nextScene){
		string[] destroys = new string[]{"Title", "Result"};

		string sname = nextScene.name;
		foreach(string i in destroys){
			if(sname.Equals(i)){
				DestroyImmediate(gameObject);
			}
		}
	}
}
