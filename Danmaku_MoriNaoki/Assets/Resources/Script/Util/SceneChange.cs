using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public string next_scene;

	public IEnumerator Load_scene(){
		AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(next_scene);

		asyncLoad.allowSceneActivation = true;//trueで読み込み完了時に強制遷移

		while(!asyncLoad.isDone){
			yield return null;
		}
	}
}
