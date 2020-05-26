using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // 追加

public class Zone_controller : MonoBehaviour
{
	public string next_scene;
	public GameObject next = null;

	private Enemy enemy;
	private bool defeated = false;

	// Scoreコンポーネント
    private Score s;

	// Timerコンポーネント
    private Timer t;

	// next_zoneを表示するText
    private Text nzt;

    // next_zoneの色
    private Color nztc;

	void Awake(){
		enemy = GameObject.FindWithTag("Enemy").GetComponent<Enemy>();
	}

	// Start is called before the first frame update
	void Start(){

		// Scoreコンポーネントを取得
        s = GameObject.Find("Score").GetComponent<Score>();

		// Timerコンポーネントを取得
        t = GameObject.Find("Time").GetComponent<Timer>();

	}

	public IEnumerator LoadScene(){

		// next_zoneを表示するText
        nzt = next.GetComponent<Text>();

        // next_zoneの色を取得
        nztc = nzt.color;

		AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(next_scene);

		asyncLoad.allowSceneActivation = false;
		while(true){
			if(Input.GetKeyDown(KeyCode.Z)){
				s.Archive(); // スコアをResultとPlusScoreに移す
				t.Archive(); // タイムをResultとPlusTimeに移す
				for(int i=1; i<=15; i++){
					nztc.a = 0f;
					yield return new WaitForEndOfFrame();
					yield return new WaitForEndOfFrame();
					nztc.a = 1f;
					yield return new WaitForEndOfFrame();
					yield return new WaitForEndOfFrame();
				}
				asyncLoad.allowSceneActivation = true;
				break;
			}
			yield return null;
		}
	}

	// Update is called once per frame
	void Update(){
		if(defeated) return;

		if(enemy.Get_phase()==0&&next.activeSelf==true){
			StartCoroutine("LoadScene");
			defeated = true;
			return;
		}

	}
}
