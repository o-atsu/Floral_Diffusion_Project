﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Straight_Bullet : Bullet
{
    private Rigidbody2D rb;
    float conv_direct = Mathf.PI / 180f;
    public int power;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        StartCoroutine("check_in_screen");
    }

    private void OnDisable()
    {
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Enemy enemy_script = collision.gameObject.GetComponent<Enemy>();
            if (Player_controll.invincible_count >= 0.1f && enemy_script.Get_phase() == 1 && SceneManager.GetActiveScene().name == "Zone_E")
            {
                return;
            }
            enemy_script.Hit(power);
            this.gameObject.SetActive(false);
        }

        if (collision.gameObject.tag == "Enemy_Bullet_Invincible")
        {
            this.gameObject.SetActive(false);
        }
    }


    public override void Set_property(Vector2 pos, float dir, float spd)
    {
        transform.localPosition = new Vector3(pos.x, pos.y, 0f);
        transform.rotation = Quaternion.AngleAxis(dir , Vector3.forward);
        rb.velocity = new Vector2(Mathf.Cos(dir*conv_direct),Mathf.Sin(dir*conv_direct)) * spd;
    }
}
