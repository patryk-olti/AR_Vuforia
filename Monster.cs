using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour {

    private Animator anim1;
    private float waiting;

    public int animacjaMonster;
    public bool change;


    // Use this for initialization
    void Start () {
        waiting = Time.time + 0.2f;
        anim1 = GetComponent<Animator>();
        change = false;
    }

    private void Update()
    {
        if (change)
        {
            waiting = Time.time + 2.48f;
            change = false;
            switch (animacjaMonster){
            case 1:     anim1.SetInteger("monsterAction", 1); break;

            case 2:     anim1.SetInteger("monsterAction", 2); break;

            case 3:     anim1.SetInteger("monsterAction", 3); break;
            }
        }

        if ((waiting - Time.time < 0) && !change)
        {
            anim1.SetInteger("monsterAction", 9);
            waiting = Time.time + 2.48f;
        }
    }


    void wait()
    {
        anim1.SetInteger("monsterAction", 9);
    }


}
