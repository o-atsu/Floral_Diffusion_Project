using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Enemy : MonoBehaviour
{
	[SerializeField]
	protected int MAX_HP;
	[SerializeField]
	protected int scorePerPhase;
	[SerializeField]
	protected int phase;
	[SerializeField]
	protected GameObject on_defeated = null;
	[SerializeField]
	protected GameObject Cut_in = null;// 同シーン上に非アクティブ状態で置いて

	public GameObject[] attack_each_phase;

	private const float START_LAG = 2f;
	private const float ENTRY_SPEED = 0.5f;

	protected int hp;
	private static bool onquit = false;
	private Collider2D col;

	// EndPhaseコンポーネント
	private EndPhase endPhase;

	// Start is called before the first frame update
    void Start(){

        // EndPhaseコンポーネントを取得
		endPhase = GameObject.Find("EndPhase").GetComponent<EndPhase>();

		StartCoroutine("entry");
    }

	/**** 倒されるときの処理 ****/
	protected virtual void Defeated(){
		phase--;
		if(phase == 0){
			gameObject.SetActive(false);
			return;
		}
		hp = MAX_HP;
	}

	/**** 動きのコルーチン ****/
	protected virtual IEnumerator move(){yield return null;}

	void OnEnable(){
		hp = MAX_HP;
	}
	void OnDisable(){
		if(on_defeated != null && !onquit){
			Instantiate(on_defeated, transform.position, Quaternion.identity);
		}
	}
	void OnApplicationQuit(){
		onquit = true;
	}
	void OnDestroy(){
		onquit = true;
	}

	public int Get_MAX_HP(){
		return MAX_HP;
	}

	public virtual void Hit(int damage){
		Score.AddScore(damage); // 与えたダメージ分スコアを増加させる
		hp -= damage;
		if(hp<=0&&phase>=1){
			Score.AddScore(hp); // オーバーキルしたときの過剰なスコア増加分を削る
			endPhase.WriteGrade(scorePerPhase); // フェイズ終了時の評価とスコアの増加
			Defeated();
		}
	}

	public float Get_percent(){
		return 100f * hp / MAX_HP;
	}
	public int Get_phase(){
		return phase;
	}

	public IEnumerator cut_in(){// カットイン
		if(Cut_in != null){
			Cut_in.SetActive(true);
			yield return new WaitForSeconds(2.6f);
			Cut_in.SetActive(false);
		}
	}

	protected virtual IEnumerator entry(){
		col = GetComponent<Collider2D>();
		col.enabled = false;
		Vector3 default_pos = transform.position;
		transform.Translate(0f, 5f, 0f);
		yield return new WaitForSeconds(START_LAG);
		StartCoroutine("cut_in");
		float time = 0f;
		while(time < 1f){
			transform.position = Vector3.Slerp(transform.position, default_pos, time);
			time += Time.deltaTime * ENTRY_SPEED;
			yield return null;
		}
		attack_each_phase[0].SetActive(true);
		col.enabled = true;
		StartCoroutine("move");
	}
}
