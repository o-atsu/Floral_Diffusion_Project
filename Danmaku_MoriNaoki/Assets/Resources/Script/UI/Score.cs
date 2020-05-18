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

    // スコア増加演出のフレーム数
    private static int showFrame = 14;

    // 1F毎に増加させる表示スコア
    private static int addPerFrame;

    // Start is called before the first frame update
    void Start(){

        // スコアを表示するText
        this.scoreText = this.GetComponent<Text>();

        // スコアを0に戻す
        score = 0;

        // 表示スコアを0に戻す
        showScore = 0;

    }

    // Update is called once per frame
    void Update(){

        // 表示スコアを増加させる
        showScore += System.Math.Min(score-showScore, addPerFrame);

        // スコアを表示する
        scoreText.text = showScore.ToString();

    }

    // ZONE終了時に取得スコアをPlusScoreに移す
    public void Archive(){
        PlusScore.plusScore += score;
        PlusScore.PlusScoreRewrite();
        score = 0;
    }

    // スコアの増加
    public static void AddScore(int add){
        score += add;
        addPerFrame = System.Math.Max((score-showScore)/showFrame+1, 1);
    }

}
