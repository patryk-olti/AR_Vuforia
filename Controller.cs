using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

    public Animator anim1;
    public float waiting;

    public bool block_click;
    public bool change;
    public int animacjaChampion;

    // Use this for initialization
    void Start () {
        waiting = Time.time;
        anim1 = GetComponent<Animator>();

        block_click = false;
        change = false;
	}
	
	void Update () {

        Klawiatura();

        if(change)
        {
            waiting = Time.time + 2.48f;
            change = false;

            switch(animacjaChampion)
            {
                case 1: anim1.SetInteger("behaviour", 1); break;
                case 2: anim1.SetInteger("behaviour", 2); break;
                case 3: anim1.SetInteger("behaviour", 3); break;
                case 4: anim1.SetInteger("behaviour", 4); break;
            }
        }

        if (Time.time > waiting)
        {
            anim1.SetInteger("behaviour", 0);
            block_click = false;
        }

    }

    void Klawiatura()
    {
        if (Input.GetKeyDown(KeyCode.Q) && (block_click == false)) // szybki
        {
            anim1.SetInteger("behaviour", 1);
            waiting = Time.time + 2.5f;

            block_click = true;
        }

        if (Input.GetKeyDown(KeyCode.E) && (block_click == false))    // ciezki
        {
            anim1.SetInteger("behaviour", 3);
            waiting = Time.time + 2.5f;

            block_click = true;
        }

        if (Input.GetKeyDown(KeyCode.W) && (block_click == false))    // sredni
        {
            anim1.SetInteger("behaviour", 2);
            waiting = Time.time + 2.5f;

            block_click = true;
        }

        if (Input.GetKeyDown(KeyCode.Space) && (block_click == false))    // def
        {
            anim1.SetInteger("behaviour", 4);
            waiting = Time.time + 2.5f;

            block_click = true;
        }
    }

}
