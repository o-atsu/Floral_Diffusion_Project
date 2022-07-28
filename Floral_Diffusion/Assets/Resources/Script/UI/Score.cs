using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // 追加

public class Score : MonoBehaviour
{

    // スコアを表示するText
    private Text scoreText;

    // スコア
    private static int score;

    // 表示スコア
    private static int showScore;

    // 100で割る前の素点
    private static int rawScore;

    // 素点にかかっているパーセント
    private static int scoreRate = 100;

    // 実際に増加したスコア
    private static int realAdd;

    // スコア増加演出のフレーム数
    private static int showFrame = 14;

    // 1F毎に増加させる表示スコア
    private static int addPerFrame;

    // PlusScoreコンポーネント
    private PlusScore ps;

    // Resultコンポーネント
    private Result r;

    // ゾーンのText
    private Text zoneText;

    // アーカイブ中フラグ
    private bool archiveNow;

    // Start is called before the first frame update
    void Start(){

        // スコアを表示するText
        this.scoreText = this.GetComponent<Text>();

        // PlusScoreコンポーネントを取得
        ps = GameObject.Find("PlusScore").GetComponent<PlusScore>();

        // Resultコンポーネントを取得
        r = GameObject.Find("Result").GetComponent<Result>();

        // 初期化
        score = 0;
        showScore = 0;
        rawScore = 0;
        archiveNow = false;

    }

    // Update is called once per frame
    void Update(){

        if(archiveNow==true){

            // 表示スコアを減少させる
            showScore += System.Math.Max(showScore*-1, addPerFrame);

            // アーカイブが完了したらフラグをfalseにする
            if(showScore<=0){
                addPerFrame = 1;
                archiveNow = false;
            }

        } else {

            // 表示スコアを増加させる
            showScore += System.Math.Min(score-showScore, addPerFrame);

        }

        // スコアを表示する
        scoreText.text = showScore.ToString();

    }

    // ZONE終了時に取得スコアをResultとPlusScoreに移す
    public void Archive(){
        archiveNow = true;
        zoneText = GameObject.Find("Zone").GetComponent<Text>();
        r.CountScore(zoneText.text, score);
        ps.PlusScoreRewrite(score);
        score = 0;
        rawScore = 0;
        addPerFrame = System.Math.Min(showScore*-1/showFrame+1, -1);
    }

    // スコアの増加
    public static int AddScore(int add){
        rawScore += add * scoreRate;
        realAdd = rawScore / 100 - score;
        score = rawScore / 100;
        addPerFrame = System.Math.Max((score-showScore)/showFrame+1, 1);
        return realAdd;
    }

    public static int GetScoreCount(){
        return score;
    }

}
