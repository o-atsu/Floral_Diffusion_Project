using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnoAtack2Move : MonoBehaviour
{
    public Straight_Nway straight_Nway;
    public float direction;
    public float direction_speed;
    public float radius;
    float conv_rad = Mathf.PI / 180f;
    float conv_direct = 180f / Mathf.PI;
    // Start is called before the first frame update
    void OnEnable()
    {
        straight_Nway.SetStartDirect(direction);
        SetPosition(direction, radius);
    }

    // Update is called once per frame
    void Update()
    {
        direction += direction_speed * Time.deltaTime;
        straight_Nway.SetStartDirect(direction);
        SetPosition(direction, radius);
    }

    public void SetPosition(float dir, float radi)
    {
        this.transform.localPosition = new Vector3(radi * Mathf.Cos(dir*conv_rad), radi * Mathf.Sin(dir*conv_rad));
    }



}
