using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // 追加

public class Zone_controller : MonoBehaviour
{
	public string next_scene;
	public GameObject next = null;
	public GameObject quit = null;

	private Enemy enemy;
	private bool defeated = false;

	// Scoreコンポーネント
    private Score s;

	// Timerコンポーネント
    private Timer t;

	// next_zoneを表示するText
    private Text nzt;

	// 評価を表示するText
    private Text ezt;

	void Awake(){
		enemy = GameObject.FindWithTag("Enemy").GetComponent<Enemy>();
	}

	// Start is called before the first frame update
	void Start(){

		// Scoreコンポーネントを取得
        s = GameObject.Find("Score").GetComponent<Score>();

		// Timerコンポーネントを取得
        t = GameObject.Find("Time").GetComponent<Timer>();

		// 評価を表示するText
        ezt = GameObject.Find("EndZone").GetComponent<Text>();

	}

	public IEnumerator LoadScene(){

		// next_zoneを表示するText
        nzt = next.GetComponent<Text>();

		AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(next_scene);

		asyncLoad.allowSceneActivation = false;
		while(true){
			if(Input.GetKeyDown(KeyCode.Z)){
				s.Archive(); // スコアをResultとPlusScoreに移す
				t.Archive(); // タイムをResultとPlusTimeに移す
				for(int i=1; i<=10; i++){
					nzt.color = new Color(1f, 0.9812571f, 0.8018868f, 0f);
					yield return new WaitForSeconds(0.05f);
					nzt.color = new Color(1f, 0.9812571f, 0.8018868f, 1f);
					yield return new WaitForSeconds(0.05f);
				}
				ezt.text = ""; // 評価を表示するテキストを消去する
				next.SetActive(false);
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

		if(Input.GetKeyDown(KeyCode.Q)) StartCoroutine("to_quit");
	}

	private IEnumerator to_quit(){
		quit.SetActive(true);
		float time = 0;
		while(time < 3f){
			int text_time = (int)(4 - time);
			if(!Input.GetKey(KeyCode.Q)){
				quit.SetActive(false);
				break;
			}else{
				time += Time.deltaTime;
				quit.GetComponent<Text>().text = "Quit to Title:" + text_time.ToString();

				yield return null;
			}
		}
		if(time >= 3f) SceneManager.LoadScene("Title");
	}
}
