using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // 追加

public class Fps : MonoBehaviour
{

    // FPSを表示するText
    private Text fpsText;

    // FPS
    private static int fps;

    // 経過フレーム数
    private static int frame;
    private static int minusFrame;
    private int pastFrame;

    // 経過処理時間
    private static float processingTime;
    private static float minusProcessingTime;

    // 次回表示更新する経過処理時間
    private float nextShow;

    // Start is called before the first frame update
    void Start(){

        // FPSを表示するText
        this.fpsText = this.GetComponent<Text>();

        // 初期化
        frame = 0;
        minusFrame = 0;
        pastFrame = 0;
        processingTime = 0f;
        minusProcessingTime = 0f;
        nextShow = 1f;

        // 目標FPSを定める
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update(){

        frame += 1;
        processingTime += Time.deltaTime;

        if(Timer.timeFlag==false){
            minusFrame += 1;
            minusProcessingTime += Time.deltaTime;
        }

        // FPSを算出して表示する
        if(processingTime>=nextShow){
            fps = frame - pastFrame;
            fpsText.text = fps.ToString();
            pastFrame = frame;
            nextShow += 1f;
        }

    }

    // 最後にプレイ全体の処理落ち率を返す関数
    public static int GetProcessingReport(){
        return 100-(int)((float)((frame-minusFrame)*5)/((processingTime-minusProcessingTime)*3f));
    }

}
