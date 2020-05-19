using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // 追加

public class PlusScore : MonoBehaviour
{

    // プラススコアを表示するText
    public Text plusScoreText;

    // プラススコア
    public int plusScore;

    // Start is called before the first frame update
    void Start(){

        // プラススコアを表示するText
        this.plusScoreText = this.GetComponent<Text>();

        // プラススコアを0に戻す
        plusScore = 0;

    }

    // 表示更新
    public void PlusScoreRewrite(){

        // プラススコアを表示する
        plusScoreText.text = "+ " + plusScore.ToString();

    }

}
