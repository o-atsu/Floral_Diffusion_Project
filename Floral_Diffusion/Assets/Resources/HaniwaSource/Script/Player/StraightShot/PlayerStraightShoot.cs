using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStraightShoot : Attack
{
    public Player_controll player_cont;
    bool shot_flag;
    public Player_Straight_Barrage_generator[] player_generators;
    // Start is called before the first frame update
    void Start()
    {
        //player_generators[0].SetStatus(this.transform.position.x , this.transform.position.y, 90f, 10.0f);
        shot_flag = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && shot_flag==false)
        {
            shot_flag = true;
            StartCoroutine("shot");
        }
    }

    protected override IEnumerator shot()
    {
        while (Input.GetKey(KeyCode.Z))
        {
            if(player_cont.Get_Damaging_Move_Count()>=0.98f&&player_cont.GetActionFlag()==true && EndZone.show_phase_result==false) player_generators[0].Generate();
            yield return new WaitForSeconds(interval);
        }
        shot_flag = false;
        yield break;
    }

}
