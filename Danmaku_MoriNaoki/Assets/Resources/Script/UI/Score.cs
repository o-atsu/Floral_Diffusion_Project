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

    // Start is called before the first frame update
    void Start(){

        // スコアを表示するText
        this.scoreText = this.GetComponent<Text>();

        // スコアを0に戻す
        score = 0;

    }

    // Update is called once per frame
    void Update(){

        // スコアを表示する
        scoreText.text = score.ToString();

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
    }

}
