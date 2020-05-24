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
    private Timer t;

    // Resultコンポーネント
    private Result r;

    // ゾーンのText
    private Text zoneText;

    // Start is called before the first frame update
    void Start(){

        // 評価を表示するText
        this.gradeText = this.GetComponent<Text>();

        // Timerコンポーネントを取得
        t = GameObject.Find("Time").GetComponent<Timer>();

        // Resultコンポーネントを取得
        r = GameObject.Find("Result").GetComponent<Result>();

        // 初期化
        gradeText.text = "";
        show = "";
        missCounter = 0;
        bombCounter = 0;

    }

    // ミス数のカウント
    public static void CountMiss(int add){
        missCounter += add;
    }

    // ボム数のカウント
    public static void CountBomb(int add){
        bombCounter += add;
    }

    // 評価文を表示する
    public void WriteGrade(int add){

        // ゾーンのText
        zoneText = GameObject.Find("Zone").GetComponent<Text>().text;

        // タイムボーナスの計算
        timeBonus = add / t.time;
        timeBonus = System.Math.Max(timeBonus, 1);

        // ミス数とボム数の保存
        if(zoneText=="A"){
            r.missCounterA = missCounter;
            r.bombCounterA = bombCounter;
        } else if(zoneText=="B"){
            r.missCounterB = missCounter;
            r.bombCounterB = bombCounter;
        } else if(zoneText=="C"){
            r.missCounterC = missCounter;
            r.bombCounterC = bombCounter;
        } else if(zoneText=="D"){
            r.missCounterD = missCounter;
            r.bombCounterD = bombCounter;
        } else if(zoneText=="E"){
            r.missCounterE = missCounter;
            r.bombCounterE = bombCounter;
        }

        // 評価文を表示する演出の開始
        StartCoroutine("ShowGrade");

    }

    // 評価文を表示する演出
    private IEnumerator ShowGrade(){

        // 評価文の初期化
        show = "";

        // ゾーンクリア
        show += "ZONE CLEAR\n\n";
        gradeText.text = show;

        // ウェイト
        for(int i=1; i<=45; i++){
            yield return new WaitForEndOfFrame();
        }

        // タイムボーナス
        show += "TIME BONUS\n<size=45>+ " + Score.AddScore(timeBonus).ToString() + "</size>\n\n";
        gradeText.text = show;

        // ウェイト
        for(int i=1; i<=45; i++){
            yield return new WaitForEndOfFrame();
        }

        // ミス数とボム数
        if(missCounter<=0&&bombCounter<=0){

            // ノーミスノーボム
            show += "Excellent!";

        } else {

            // ミス数またはボム数が1以上
            show += missCounter.ToString() + "<size=45> MISS</size> " + bombCounter.ToString() + "<size=45> BOMB";

        }
        gradeText.text = show;

        // カウンターの初期化
        missCounter = 0;
        bombCounter = 0;

        // 表示処理完了
        yield break;

    }

}
