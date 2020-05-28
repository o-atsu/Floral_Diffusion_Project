using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // 追加

public class EndZone : MonoBehaviour
{

    // 評価を表示するText
    private Text gradeText;

    // 評価文
    private string show;

    // ミス数のカウンター
    private static int missCounter;

    // ボム数のカウンター
    private static int bombCounter;

    // タイムボーナス
    private static int timeBonus;

    // Timerコンポーネント
    // private Timer t;

    // Resultコンポーネント
    private Result r;

    // プレイヤーコントロールのコンポーネント
    private Player_controll pc;

    // ゾーン背景のコンポーネント
    private SpriteRenderer zbsr;
    private AudioSource zbas;

    // ゾーン背景の色
    private Color zbsrc;
    private float defaultColorRed;
    private float defaultColorGreen;
    private float defaultColorBlue;

    // ゾーンのBGM音量
    private float defaultVolume;

    // SE
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip showHead;
    [SerializeField]
    private AudioClip showExcellent;
    [SerializeField]
    private AudioClip showOther;
    [SerializeField]
    private AudioClip pushKey;

    // ゾーンのText
    private Text zoneText;

    // Zone_controllerコンポーネント
    private Zone_controller zc;

    // next_zoneのGameObject
    private GameObject nz;

    // Start is called before the first frame update
    void Start(){

        // 評価を表示するText
        this.gradeText = this.GetComponent<Text>();

        // Timerコンポーネントを取得
        // t = GameObject.Find("Time").GetComponent<Timer>();

        // Resultコンポーネントを取得
        r = GameObject.Find("Result").GetComponent<Result>();

        // Zone_controllerコンポーネントを取得
        zc = GameObject.Find("Zone_controller").GetComponent<Zone_controller>();

        // AudioSourceコンポーネントを取得
        this.audioSource = this.GetComponent<AudioSource>();

        // next_zoneのGameObjectを取得
        nz = zc.next;

        // 初期化
        gradeText.text = "";
        show = "";
        missCounter = 0;
        bombCounter = 0;

    }

    // ミス数のカウント
    public static void CountMiss(int add){
        missCounter += add;
        // Debug.Log("miss"+missCounter.ToString());
    }

    // ボム数のカウント
    public static void CountBomb(int add){
        bombCounter += add;
        // Debug.Log("bomb"+bombCounter.ToString());
    }

    // ミス数のカウントの取得
    public static int GetMissCount(){
        return missCounter;
    }

    // ボム数のカウントの取得
    public static int GetBombCount(){
        return bombCounter;
    }

    // キー入力時のSE
    public void PlayPushKeySE(){
        audioSource.PlayOneShot(pushKey);
    }

    // 評価文を表示する
    public void WriteGrade(int add){

        // ゾーンのText
        zoneText = GameObject.Find("Zone").GetComponent<Text>();

        // ゾーンEならリザルト用にライフとボムを保存
        if(zoneText.text=="E"){
            pc = GameObject.FindWithTag("Player").GetComponent<Player_controll>();
            r.CountLifeAndBomb(pc.GetLifeCount(), pc.GetBombCount());
        }

        // ゾーン背景のコンポーネントを取得
        zbsr = GameObject.Find("ZoneBackground").GetComponent<SpriteRenderer>();
        zbas = GameObject.Find("ZoneBackground").GetComponent<AudioSource>();

        // ゾーン背景の色を取得
        zbsrc = zbsr.color;
        defaultColorRed = zbsrc.r;
        defaultColorGreen = zbsrc.g;
        defaultColorBlue = zbsrc.b;

        // ゾーンのBGM音量を取得
        defaultVolume = zbas.volume;

        // タイム計測の停止
        Timer.timeFlag = false;

        // タイムボーナスの計算
        timeBonus = (int)((float)add/Timer.GetTimeCount());
        timeBonus = System.Math.Max(timeBonus, 1);

        // ミス数とボム数の保存
        r.CountMissAndBomb(zoneText.text, missCounter, bombCounter);

        // 評価文を表示する演出の開始
        StartCoroutine("ShowGrade");

    }

    // 評価文を表示する演出
    private IEnumerator ShowGrade(){

        // 評価文の初期化
        show = "";

        // 背景の暗転とBGMのフェイドアウトとウェイト
        for(int i=1; i<=30; i++){
            zbsr.color = new Color(defaultColorRed*(60f-i)/60f, defaultColorGreen*(60f-i)/60f, defaultColorBlue*(60f-i)/60f, 1f);
            zbas.volume = defaultVolume * (30-i) / 30;
            yield return new WaitForSeconds(0.1f);
        }

        // アクションフラグ切り替え
        // pc.SetActionFlag(false);

        // ゾーンクリア
        show += "ZONE CLEAR\n\n";
        gradeText.text = show;
        audioSource.PlayOneShot(showHead);

        // ウェイト
        yield return new WaitForSeconds(0.75f);

        // タイムボーナス
        show += "TIME BONUS\n<size=45>+ " + Score.AddScore(timeBonus).ToString() + "</size>\n\n";
        gradeText.text = show;
        audioSource.PlayOneShot(showHead);

        // ウェイト
        yield return new WaitForSeconds(0.75f);

        // ミス数とボム数
        if(missCounter<=0&&bombCounter<=0){

            // ノーミスノーボム
            show += "Excellent!";
            audioSource.PlayOneShot(showExcellent);

        } else {

            // ミス数またはボム数が1以上
            show += missCounter.ToString() + "<size=45> MISS</size> " + bombCounter.ToString() + "<size=45> BOMB</size>";
            audioSource.PlayOneShot(showOther);

        }
        gradeText.text = show;

        // next_zoneのGameObjectをアクティベイト
        nz.SetActive(true);

        // カウンターの初期化
        missCounter = 0;
        bombCounter = 0;

        // 表示処理完了
        yield break;

    }

}
