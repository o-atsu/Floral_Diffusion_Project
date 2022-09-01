using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Straight_Nway : Attack
{
    public int ways_num=1;
    private float start_dir=0f;
    public GameObject start_posi_object;

    [SerializeField]
    private Barrage_generator[] generators;

    protected override IEnumerator shot()
    {
        while (true)
        {
            if (start_dir > 360f) start_dir -= 360f;
            if (start_dir < -360f) start_dir += 360f;
            float set_dir = start_dir;
            for (int i = 0; i < ways_num; i++)
            {
                if (start_posi_object == null)
                {
                    start_posi_object = this.gameObject;
                }
                Vector2 start_posi = new Vector2(start_posi_object.transform.position.x, start_posi_object.transform.position.y);
                generators[0].SetPosition(start_posi);
                generators[0].Set_Direct(set_dir);
                generators[0].Generate();
                set_dir += 360f / (float)ways_num;
            }
            yield return new WaitForSeconds(interval);
        }
    }

    void Start()
    {
        StartCoroutine("shot");
    }

    public void SetStartDirect(float dir)
    {
        start_dir = dir;
    }

}
