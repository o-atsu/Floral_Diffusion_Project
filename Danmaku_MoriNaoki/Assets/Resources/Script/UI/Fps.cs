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
    public static int frame = 0;
    public static int minusFrame = 0;
    public static int pastFrame = 0;

    // 経過処理時間
    public static float processingTime = 0f;
    public static float minusProcessingTime = 0f;

    // 次回表示更新する経過処理時間
    public static float nextShow = 1f;

    // Start is called before the first frame update
    void Start(){

        // FPSを表示するText
        this.fpsText = this.GetComponent<Text>();

        // 初期化
        // frame = 0;
        // minusFrame = 0;
        // pastFrame = 0;
        // processingTime = 0f;
        // minusProcessingTime = 0f;
        // nextShow = 1f;

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
        // Debug.Log(frame.ToString()+" "+minusFrame.ToString()+" "+processingTime.ToString()+" "+minusProcessingTime.ToString());
        return 100-(int)((float)((frame-minusFrame)*5)/((processingTime-minusProcessingTime)*3f));
    }

    // リセット
    public static void ResetFps(){
        frame = 0;
        minusFrame = 0;
        pastFrame = 0;
        processingTime = 0f;
        minusProcessingTime = 0f;
        nextShow = 1f;
    }

}
