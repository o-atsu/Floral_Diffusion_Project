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
	private Text nextt;
	private Text quitt;

	// Scoreコンポーネント
    private Score s;

	// Timerコンポーネント
    private Timer t;

	// Resultコンポーネント
    private Result r;

	// プレイヤーコントロールのコンポーネント
    private Player_controll pc;

	// EndZoneコンポーネント
	private EndZone ez;

    // ゾーン背景のコンポーネント
    private SpriteRenderer zbsr;
    private AudioSource zbas;

    // ゾーン背景の色
    private Color zbsrc;
    private float defaultColorRed;
    private float defaultColorGreen;
    private float defaultColorBlue;

	// next_zoneを表示するText
    private Text nzt;

	// 評価を表示するText
    private Text ezt;

	// ゾーンのText
    private Text zoneText;

	// Enemy_DataのGameObject
    private GameObject edgo;

	void Awake(){
		enemy = GameObject.FindWithTag("Enemy").GetComponent<Enemy>();
		nextt = GameObject.Find("Next_Zone").GetComponent<Text>();
		quitt = GameObject.Find("Quit").GetComponent<Text>();
	}

	// Start is called before the first frame update
	void Start(){

		// Scoreコンポーネントを取得
        s = GameObject.Find("Score").GetComponent<Score>();

		// Timerコンポーネントを取得
        t = GameObject.Find("Time").GetComponent<Timer>();

		// Resultコンポーネントを取得
        r = GameObject.Find("Result").GetComponent<Result>();

		// プレイヤーコントロールのコンポーネントを取得
        pc = GameObject.FindWithTag("Player").GetComponent<Player_controll>();

		// EndZoneコンポーネントを取得
        ez = GameObject.Find("EndZone").GetComponent<EndZone>();

		// 評価を表示するText
        ezt = GameObject.Find("EndZone").GetComponent<Text>();

		// Enemy_DataのGameObjectを取得
		edgo = GameObject.Find("Enemy_Data");

	}

	public IEnumerator LoadScene(){

		if(pc.GetLifeCount()<=0){

			// 非表示にする
			edgo.SetActive(false);

			// ウェイト
			yield return new WaitForEndOfFrame();

			AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Result");

			asyncLoad.allowSceneActivation = false;

			// スコアをResultとPlusScoreに移す
			s.Archive();

			// タイムをResultとPlusTimeに移す
			t.Archive();

			// BGMの停止
			zbas.volume = 0f;

			// 背景の暗転とウェイト
	        for(int i=1; i<=30; i++){
	            zbsr.color = new Color(defaultColorRed*(30f-i)/30f, defaultColorGreen*(30f-i)/30f, defaultColorBlue*(30f-i)/30f, 1f);
	            yield return new WaitForSeconds(0.1f);
	        }

			asyncLoad.allowSceneActivation = true;

		} else {

			// next_zoneを表示するText
	        nzt = next.GetComponent<Text>();

			AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(next_scene);

			asyncLoad.allowSceneActivation = false;
			while(true){
				if(Input.GetKeyDown(KeyCode.Z)){
					ez.PlayPushKeySE(); // キー入力時のSE
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

	}

	// Update is called once per frame
	void Update(){
		if(defeated) return;

		if(pc.GetLifeCount()<=0){
			zoneText = GameObject.Find("Zone").GetComponent<Text>();
			zbsr = GameObject.Find("ZoneBackground").GetComponent<SpriteRenderer>();
	        zbas = GameObject.Find("ZoneBackground").GetComponent<AudioSource>();
			zbsrc = zbsr.color;
	        defaultColorRed = zbsrc.r;
	        defaultColorGreen = zbsrc.g;
	        defaultColorBlue = zbsrc.b;
			Timer.timeFlag = false;
			r.CountMissAndBomb(zoneText.text, EndZone.GetMissCount(), EndZone.GetBombCount());
			StartCoroutine("LoadScene");
			defeated = true;
			return;
		}

		if(enemy.Get_phase()==0&&next.activeSelf==true){
			StartCoroutine("LoadScene");
			defeated = true;
			return;
		}

		if(Input.GetKeyDown(KeyCode.Q)) StartCoroutine("to_quit");
	}

	private IEnumerator to_quit(){
		float time = 0;
		while(time < 3f){
			int text_time = (int)(4 - time);
			if(!Input.GetKey(KeyCode.Q)){
				quitt.text = "";
				break;
			}else{
				time += Time.deltaTime;
				quitt.text = "Quit to Title:" + text_time.ToString();
				
				yield return null;
			}
		}
		if(time >= 3f) SceneManager.LoadScene("Title");
	}
}
