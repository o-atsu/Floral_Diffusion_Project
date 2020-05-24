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

	protected int hp;
	private static bool onquit = false;

	// EndPhaseコンポーネント
	private EndPhase endPhase;

	// Start is called before the first frame update
    void Start(){

        // EndPhaseコンポーネントを取得
		endPhase = GameObject.Find("EndPhase").GetComponent<EndPhase>();

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
		StartCoroutine("move");
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

	public int Get_MAX_HP(){
		return MAX_HP;
	}

	public void Hit(int damage){
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
}
