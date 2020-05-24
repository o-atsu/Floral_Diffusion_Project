using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // 追加

public class Result : MonoBehaviour
{

    // リザルトを表示するText
    private Text resultText;

    // 各ゾーンのスコア
    private int scoreA;
    private int scoreB;
    private int scoreC;
    private int scoreD;
    private int scoreE;

    // 各ゾーンのタイム
    private int timeA;
    private int timeB;
    private int timeC;
    private int timeD;
    private int timeE;

    // 各ゾーンのミス数とボム数
    private int　missCounterA;
    private int　missCounterB;
    private int　missCounterC;
    private int　missCounterD;
    private int　missCounterE;
    private int　bombCounterA;
    private int　bombCounterB;
    private int　bombCounterC;
    private int　bombCounterD;
    private int　bombCounterE;

    // Start is called before the first frame update
    void Start(){

        // スコアを表示するText
        this.resultText = this.GetComponent<Text>();

        // 初期化

    }

    // Update is called once per frame
    void Update(){

    }

}
