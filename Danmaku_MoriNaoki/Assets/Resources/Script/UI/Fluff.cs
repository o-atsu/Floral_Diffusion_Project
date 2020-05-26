using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fluff : MonoBehaviour
{

    private const float StandardDirect = 90f;
    private float direct=0f;
    private float speed_x = 0f;
    private float speed_y = 0f;


    float conv_rad = Mathf.PI / 180f;
    float conv_dierect = 180f / Mathf.PI;

    private const float TOP = 360f+40f;
    private const float BOTTOM = -360f-40f;
    private const float RIGHT = 640f+40f;
    private const float LEFT = -640f-40f;

    private RectTransform rect;

    // Start is called before the first frame update
    void Start()
    {
        rect = this.gameObject.GetComponent<RectTransform>();
        Init();
        rect.localPosition += new Vector3(0f, Random.Range(0f, 420f), 0f);
    }

    // Update is called once per frame
    void Update()
    {
        rect.localPosition += new Vector3(speed_x * Time.deltaTime, speed_y * Time.deltaTime, 0);

        if (CheckInScreen() == false) Init();

    }

    private void Init()
    {
        direct = StandardDirect + (Random.Range(0f, 60f) - 20f);
        float new_x = Random.Range(0f, 1280f) - 640f;
        float new_y = Random.Range(0f, 20f) - 100f;
        rect.localPosition = new Vector3(new_x, new_y, 0);
        float speed = Random.Range(100f, 150f);
        speed_x = speed * Mathf.Cos(direct * conv_rad);
        speed_y = speed * Mathf.Sin(direct * conv_rad);

    }

    private bool CheckInScreen()
    {
        if(rect.localPosition.x<LEFT)return false;
        if(rect.localPosition.x>RIGHT)return false;
        if(rect.localPosition.y<BOTTOM)return false;
        if(rect.localPosition.y>TOP)return false;

        return true;
    }

}
