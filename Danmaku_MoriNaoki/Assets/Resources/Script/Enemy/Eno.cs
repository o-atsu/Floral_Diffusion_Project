using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eno : Enemy
{
	private Rigidbody2D rb;
	private Animator anim;

    void Awake(){
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
        this.transform.position = new Vector3(-2f, 2.6f, 0f);
    }

    private void OnEnable()
    {
        SetAtack();
        hp = MAX_HP;
    }

    protected override void Defeated(){
        if (phase == 0) return;

        attack_each_phase[attack_each_phase.Length - phase].SetActive(false);
        StopMove();
        phase--;
        if (phase == 0){
			gameObject.SetActive(false);
			Debug.Log("Eno:Defeated!");
			return;
		}
		hp = MAX_HP;
        StartCoroutine("phase_change");
    }

	protected override IEnumerator move(){
        this.transform.position = new Vector3(-2f, 2.6f, 0f);
        while (phase == 4){
            yield return null;
        }
        
        yield break;
	}

    protected IEnumerator move33()
    {
        float tmp = 0;
        float goal_x;
        float goal_y;
        while (phase == 3)
        {
            tmp = 0;
            goal_x = -3.0f - Random.Range(0f, 1f);
            goal_y = 2.1f + Random.Range(0f, 1f);
            while (tmp < 1)
            {
                if (phase != 3)
                { 
                    yield break;
                }
                
                transform.position = Vector3.Lerp(transform.position, new Vector3(goal_x, goal_y, 0f), tmp);
                tmp += 0.4f * Time.deltaTime;
                yield return null;
            }
            tmp = 0;
            goal_x = -1.0f + Random.Range(0f, 1f);
            goal_y = 2.1f + Random.Range(0f, 1f);
            while (tmp < 1)
            {
                if (phase != 3)
                {
                    yield break;
                }
                
                transform.position = Vector3.Lerp(transform.position, new Vector3(goal_x, goal_y, 0f), tmp);
                tmp += 0.4f * Time.deltaTime;
                yield return null;
                
            }
        }
        
        yield break;
    }
    protected IEnumerator move3()
    {
        
        while (phase == 3)
        {

            yield return null;
        }

        yield break;
    }

    protected IEnumerator move2()
    {
        int stay_time = 5;
        int move_time = 3;

        rb.velocity = new Vector2(-2f, 0f);
        for (int i = 0; i < 1; i++)
        {
            yield return new WaitForSeconds(1.5f);
            if (phase != 2) break;
        }
        while (phase == 2)
        {
            rb.velocity = new Vector2(0f, 0f);

            for (int i = 0; i < stay_time; i++)
            {
                yield return new WaitForSeconds(1.0f);
                if (phase != 2) break;
            }

            rb.velocity = new Vector2(2f, 0f);

            for (int i = 0; i < move_time; i++)
            {
                yield return new WaitForSeconds(1.0f);
                if (phase != 2) break;
            }

            rb.velocity = new Vector2(0f, 0f);

            for (int i = 0; i < stay_time; i++)
            {
                yield return new WaitForSeconds(1.0f);
                if (phase != 2) break;
            }

            rb.velocity = new Vector2(-2f, 0f);

            for (int i = 0; i < move_time; i++)
            {
                yield return new WaitForSeconds(1.0f);
                if (phase != 2) break;
            }

        }

        yield break;
    }

    protected IEnumerator move1()
    {
        while (phase==1)
        {

            yield return null;
        }

        yield break;
    }

    private IEnumerator phase_change()
    {
        rb.velocity = new Vector3(0f, 0f, 0f);
        float tmp = 0;
        while (tmp < 1)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(-2.0f, 2.6f, 0f), tmp);
            tmp += 0.3f * Time.deltaTime;
            yield return null;
        }
        yield return 1.0;
        SetMove();
        SetAtack();

		if(phase == 1) StartCoroutine("cut_in");
    }

    private void StopMove()
    {
        rb.velocity = Vector2.zero;
        string S = "move";
        S += phase.ToString();
        StopCoroutine(S);
    }

    private void SetMove()
    {
        string S = "move";
        S += phase.ToString();
        StartCoroutine(S);
    }

    private void SetAtack()
    {
        attack_each_phase[attack_each_phase.Length - phase].SetActive(true);
    }

    void Update(){
		anim.SetFloat("velocity_x", rb.velocity.x);
	}
}
