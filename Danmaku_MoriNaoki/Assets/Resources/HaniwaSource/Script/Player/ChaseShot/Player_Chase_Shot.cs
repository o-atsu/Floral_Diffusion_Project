using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Chase_Shot : Attack
{
    public Player_controll player_cont;
    bool shot_flag;
    public Player_ChaseBullet_Generator[] player_generators;
    // Start is called before the first frame update
    void Start()
    {
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
            if (player_cont.Get_Damaging_Move_Count()>=0.98f&&player_cont.GetActionFlag()== true && EndZone.show_phase_result == false)
            {
                player_generators[0].Generate();

            }
            yield return new WaitForSeconds(interval);
        }
        shot_flag = false;
        yield break;
    }
}
