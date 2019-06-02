using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewEngine : MonoBehaviour {

    int Heath_Monster;                  // aktualna ilosc zycia wroga
    int Heath_Champion;                 // aktualna ilosc zycia bohatera

    public TextMesh HP_M;               // aktualnie wyswietlanie zycie w pasku potwora
    public TextMesh HP_C;               // aktualnie wyswietlanie zycie w pasku championa
    public TextMesh damage_text;        // aktualnie wyswietlanie zycie ponad championem - zadany dmg
    public TextMesh damage_text_M;      // aktualnie wyswietlanie zycie ponad potworem   - zadany dmg

    Monster monster;                    // instancja potwora
    Controller control;                 // instancja kontrolera bohatera

    int showDMG;                        // co pokazac f. fixed update
    float timeToShowDmg;                // jaki czas pokazywac 
    float timeWaitForAttack;            // jaki czas czeka monster

    float wait;                         // czas blokady klawiatury
    bool block;                         // blokada klawiatury

    int losowanie;                      // zmienna decyzyjna, uzywana w kazdym losowaniu rand()
    int damage_C;                       // obliczana ilosc obrazen championa dla monstera
    int damage_M;                       // obliczana ilosc obrazen monstera dla championa

    bool miniblock;
                                        // Use this for initialization
    void Start () {

        Heath_Monster = 100;
        Heath_Champion = 100;

        monster = FindObjectOfType<Monster>();
        control = FindObjectOfType<Controller>();

        showDMG = 0;
        timeToShowDmg = Time.time - 1f;
        timeWaitForAttack = Time.time + 4;
        wait = Time.time - 1;
        block = false;

        losowanie = 0;
        damage_C = 0;
        damage_M = 0;

        miniblock = false;
}

    private void FixedUpdate()
    {
        HP_M.text = "MONSTER: " + Heath_Monster + "/100";
        HP_C.text = Heath_Champion + "/100 :CHAMPION";

        if (timeToShowDmg - Time.time > 0)
        {
            switch (showDMG)
            {
                case 1:
                    {
                        damage_text.text = damage_C + " dmg";
                        damage_text_M.text = "";
                    }
                    break;
                case 2:
                    {
                        damage_text_M.text = damage_M + " dmg";
                        damage_text.text = "";
                    }
                    break;
                case 3:
                    {
                        damage_text.text = "missed";
                        damage_text_M.text = "missed";
                    }
                    break;
            }
        }
        else
        {
            damage_text.text = "";
            damage_text_M.text = "";
        }


    }

    // Update is called once per frame
    void Update () {

        if (wait - Time.time < 0)
        {
            monster.change = false;
            block = false;
            control.block_click = false;
            control.change = false;
            monster.animacjaMonster = 9;
        }

        Q_Click();
        W_Click();
        E_Click();
        Space_Click();

        MonsterAttack();

        if(Heath_Champion < 0)
        {
            SceneManager.LoadScene("monsterWin");
        }

        if(Heath_Monster < 0)
        {
            SceneManager.LoadScene("champWin");
        }

    }


    void Q_Click()
    {

        if ((Input.GetKeyDown(KeyCode.Q)) && (block == false))
        {
            timeWaitForAttack = 0;
            timeWaitForAttack = Time.time + 3.5f;

            afterClick();

            losowanie = Random.Range(1, 11);

            if (losowanie > 0 && losowanie <= 4)     // atak szybki
            {
                monster.animacjaMonster = 1;
                losowanie = Random.Range(1, 11);

                if (losowanie > 0 && losowanie <= 4 && !miniblock)  // Ch.P.
                {
                    DMGforMONSTER(1);
                }

                if (losowanie > 4 && losowanie <= 7 && !miniblock)  // M. P.
                {
                    DMGforCHAMP(1);
                }

                if (losowanie > 7 && losowanie <= 10 && !miniblock) // NIC
                {
                    nothing();
                }
            }

            if (losowanie > 4 && losowanie <= 10)    // obrona
            {
                monster.animacjaMonster = 3;
                losowanie = Random.Range(1, 11);

                if (losowanie > 0 && losowanie <= 8 && !miniblock) // NIC 
                {
                    nothing();
                }

                if(losowanie > 8 && !miniblock) // Ch.P.
                {
                    DMGforMONSTER(2);
                }
            }
        }

        miniblock = false;
    }

    public void Q_Click_2()
    {
            if (block == false)
            {
                timeWaitForAttack = 0;
                timeWaitForAttack = Time.time + 3.5f;

                afterClick();
                control.change = true;
                control.animacjaChampion = 1;

                losowanie = Random.Range(1, 11);

                if (losowanie > 0 && losowanie <= 4)     // atak szybki
                {
                    monster.animacjaMonster = 1;
                    losowanie = Random.Range(1, 11);

                    if (losowanie > 0 && losowanie <= 4 && !miniblock)  // Ch.P.
                    {
                        DMGforMONSTER(1);
                    }

                    if (losowanie > 4 && losowanie <= 7 && !miniblock)  // M. P.
                    {
                        DMGforCHAMP(1);
                    }

                    if (losowanie > 7 && losowanie <= 10 && !miniblock) // NIC
                    {
                        nothing();
                    }
                }

                if (losowanie > 4 && losowanie <= 10)    // obrona
                {
                    monster.animacjaMonster = 3;
                    losowanie = Random.Range(1, 11);

                    if (losowanie > 0 && losowanie <= 8 && !miniblock) // NIC 
                    {
                        nothing();
                    }

                    if (losowanie > 8 && !miniblock) // Ch.P.
                    {
                        DMGforMONSTER(2);
                    }
                }
            }
            miniblock = false;
    }

    void W_Click()
    {
        if ((Input.GetKeyDown(KeyCode.W)) && (block == false))             // sredni atak
        {
            timeWaitForAttack = 0;
            timeWaitForAttack = Time.time + 3.5f;

            afterClick();

            losowanie = Random.Range(1, 11);

            if (losowanie > 0 && losowanie <= 3)    // atak szybki
            {
                monster.animacjaMonster = 1;
                losowanie = Random.Range(1, 11);

                if (losowanie > 0 && losowanie <= 4 && !miniblock) // Ch. P
                {
                    DMGforMONSTER(2);
                }
                if (losowanie > 4 && losowanie <= 7 && !miniblock) // M. P.
                {
                    DMGforCHAMP(1);
                }
                if (losowanie > 7 && losowanie <= 11 && !miniblock) // NIC
                {
                    nothing();
                }
            }

            if (losowanie > 3 && losowanie <= 6)      // atak silny
            {
                monster.animacjaMonster = 2;

                losowanie = Random.Range(1, 11);

                if (losowanie > 0 && losowanie <= 6 && !miniblock)  // Ch.P.
                {
                    DMGforMONSTER(2);
                }
                if (losowanie > 6 && losowanie <= 8 && !miniblock) // M.P.
                {
                    DMGforCHAMP(2);
                }
                if (losowanie > 8 && losowanie <= 10 && !miniblock) // NIC
                {
                    nothing();
                }
            }
            if (losowanie > 6 && losowanie <= 10)     // obrona
            {
                monster.animacjaMonster = 3;

                losowanie = Random.Range(1, 11);

                if (losowanie > 0 && losowanie <= 8 && !miniblock) // NIC
                {
                    nothing();
                }
                if (losowanie > 8 && !miniblock)
                {
                    DMGforMONSTER(2);
                }
            }
        }

        miniblock = false;
    }

    public void W_Click_2()
    {
        if (block == false)             // sredni atak
        {
            timeWaitForAttack = 0;
            timeWaitForAttack = Time.time + 3.5f;

            afterClick();
            control.change = true;
            control.animacjaChampion = 2;

            losowanie = Random.Range(1, 11);

            if (losowanie > 0 && losowanie <= 3)    // atak szybki
            {
                monster.animacjaMonster = 1;
                losowanie = Random.Range(1, 11);

                if (losowanie > 0 && losowanie <= 4 && !miniblock) // Ch. P
                {
                    DMGforMONSTER(2);
                }
                if (losowanie > 4 && losowanie <= 7 && !miniblock) // M. P.
                {
                    DMGforCHAMP(1);
                }
                if (losowanie > 7 && losowanie <= 11 && !miniblock) // NIC
                {
                    nothing();
                }
            }

            if (losowanie > 3 && losowanie <= 6)      // atak silny
            {
                monster.animacjaMonster = 2;

                losowanie = Random.Range(1, 11);

                if (losowanie > 0 && losowanie <= 6 && !miniblock)  // Ch.P.
                {
                    DMGforMONSTER(2);
                }
                if (losowanie > 6 && losowanie <= 8 && !miniblock) // M.P.
                {
                    DMGforCHAMP(2);
                }
                if (losowanie > 8 && losowanie <= 10 && !miniblock) // NIC
                {
                    nothing();
                }
            }
            if (losowanie > 6 && losowanie <= 10)     // obrona
            {
                monster.animacjaMonster = 3;

                losowanie = Random.Range(1, 11);

                if (losowanie > 0 && losowanie <= 8 && !miniblock) // NIC
                {
                    nothing();
                }
                if (losowanie > 8 && !miniblock)
                {
                    DMGforMONSTER(2);
                }
            }
        }

        miniblock = false;
    }

    void E_Click()
    {
        if ((Input.GetKeyDown(KeyCode.E)) && !block)
        {
            timeWaitForAttack = 0;
            timeWaitForAttack = Time.time + 3.5f;

            afterClick();

            losowanie = Random.Range(1, 11);

            if (losowanie > 0 && losowanie <= 2)     // szybki atak
            {
                monster.animacjaMonster = 1;
                losowanie = Random.Range(1, 11);

                if (losowanie > 0 && losowanie <= 4 && !miniblock)
                {
                    // Ch.P.
                    DMGforMONSTER(3);
                }
                if (losowanie > 4 && losowanie <= 7 && !miniblock)
                {
                    // M.P.
                    DMGforCHAMP(1);
                }
                if (losowanie > 7 && losowanie <= 10 && !miniblock)
                {
                    // NIC
                    nothing();
                }
            }

            if (losowanie > 2 && losowanie <= 4)     // atak silny
            {
                monster.animacjaMonster = 2;

                losowanie = Random.Range(1, 11);

                if (losowanie > 0 && losowanie <= 6 && !miniblock)
                {
                    // Ch. P.
                    DMGforMONSTER(2);
                }
                if (losowanie > 6 && losowanie <= 8 && !miniblock)
                {
                    // M.P.
                    DMGforCHAMP(2);
                }
                if (losowanie > 8 && losowanie <= 10 && !miniblock)
                {
                    // NIC
                    nothing();
                }
            }

            if (losowanie > 4 && losowanie <= 10 && !miniblock)    // obrona
            {
                monster.animacjaMonster = 3;

                losowanie = Random.Range(1, 11);

                if (losowanie > 0 && losowanie <= 8)
                {
                    // NIC
                    nothing();
                }
                if(losowanie > 8 && !miniblock)
                {
                    //Ch. P.
                    DMGforMONSTER(2);
                }
            }
        }
        miniblock = false;
    }

    public void E_Click_2()
    {
        if (block == false)
        {
            timeWaitForAttack = 0;
            timeWaitForAttack = Time.time + 3.5f;

            afterClick();
            control.change = true;
            control.animacjaChampion = 3;

            losowanie = Random.Range(1, 11);

            if (losowanie > 0 && losowanie <= 2)     // szybki atak
            {
                monster.animacjaMonster = 1;
                losowanie = Random.Range(1, 11);

                if (losowanie > 0 && losowanie <= 4 && !miniblock)
                {
                    // Ch.P.
                    DMGforMONSTER(3);
                }
                if (losowanie > 4 && losowanie <= 7 && !miniblock)
                {
                    // M.P.
                    DMGforCHAMP(1);
                }
                if (losowanie > 7 && losowanie <= 10 && !miniblock)
                {
                    // NIC
                    nothing();
                }
            }

            if (losowanie > 2 && losowanie <= 4)     // atak silny
            {
                monster.animacjaMonster = 2;

                losowanie = Random.Range(1, 11);

                if (losowanie > 0 && losowanie <= 6 && !miniblock)
                {
                    // Ch. P.
                    DMGforMONSTER(2);
                }
                if (losowanie > 6 && losowanie <= 8 && !miniblock)
                {
                    // M.P.
                    DMGforCHAMP(2);
                }
                if (losowanie > 8 && losowanie <= 10 && !miniblock)
                {
                    // NIC
                    nothing();
                }
            }

            if (losowanie > 4 && losowanie <= 10 && !miniblock)    // obrona
            {
                monster.animacjaMonster = 3;

                losowanie = Random.Range(1, 11);

                if (losowanie > 0 && losowanie <= 8)
                {
                    // NIC
                    nothing();
                }
                if (losowanie > 8 && !miniblock)
                {
                    //Ch. P.
                    DMGforMONSTER(2);
                }
            }
        }
        miniblock = false;
    }

    void Space_Click()
    {
        if ((Input.GetKeyDown(KeyCode.Space)) && !block)
        {
            timeWaitForAttack = 0;
            timeWaitForAttack = Time.time + 3.5f;

            afterClick();

            losowanie = Random.Range(1, 11);

            if (losowanie > 0 && losowanie <= 5)     // atak szybki
            {
                monster.animacjaMonster = 1;
                losowanie = Random.Range(1, 11);

                if (losowanie > 0 && losowanie <= 8 && !miniblock)
                {
                    // M. P.
                    DMGforCHAMP(1);
                }
                if(losowanie > 8 && !miniblock)
                {
                    // NIC
                    nothing();
                }
            }

            if (losowanie > 5 && losowanie <= 6)     // atak silny
            {
                monster.animacjaMonster = 2;

                losowanie = Random.Range(1, 11);

                if (losowanie > 0 && losowanie <= 6 && !miniblock)
                {
                    // M. P.
                    DMGforCHAMP(2);
                }
                if(losowanie > 6 && !miniblock)
                {
                    // NIC
                    nothing();
                }
            }

            if (losowanie > 6 && losowanie <= 10)    // NIC
            {
                // NIC
                nothing();
            }
        }
        miniblock = false;
    }

    public void Space_Click_2()
    {
        if (block == false)
        {
            timeWaitForAttack = 0;
            timeWaitForAttack = Time.time + 3.5f;

            afterClick();
            control.change = true;
            control.animacjaChampion = 4;

            losowanie = Random.Range(1, 11);

            if (losowanie > 0 && losowanie <= 5)     // atak szybki
            {
                monster.animacjaMonster = 1;
                losowanie = Random.Range(1, 11);

                if (losowanie > 0 && losowanie <= 8 && !miniblock)
                {
                    // M. P.
                    DMGforCHAMP(1);
                }
                if (losowanie > 8 && !miniblock)
                {
                    // NIC
                    nothing();
                }
            }

            if (losowanie > 5 && losowanie <= 6)     // atak silny
            {
                monster.animacjaMonster = 2;

                losowanie = Random.Range(1, 11);

                if (losowanie > 0 && losowanie <= 6 && !miniblock)
                {
                    // M. P.
                    DMGforCHAMP(2);
                }
                if (losowanie > 6 && !miniblock)
                {
                    // NIC
                    nothing();
                }
            }

            if (losowanie > 6 && losowanie <= 10)    // NIC
            {
                // NIC
                nothing();
            }
        }
        miniblock = false;
    }

    void MonsterAttack()
    {
        if ((timeWaitForAttack - Time.time < 0) && (block == false))
        {
            afterClick();
            timeWaitForAttack = 0;
            timeWaitForAttack = Time.time + 4.55f;

            losowanie = Random.Range(1, 11);

            if (losowanie > 0 && losowanie <= 5) // atak silny
            {
                monster.animacjaMonster = 2;
                losowanie = Random.Range(1, 11);

                if (losowanie > 0 && losowanie <= 8 && !miniblock)
                {
                    // M. P.
                    DMGforCHAMP(2);
                    control.anim1.SetInteger("behaviour", 1);
                }
                if(losowanie > 8 && !miniblock)
                {
                    // NIC
                    nothing();
                    control.anim1.SetInteger("behaviour", 4);
                }
            }

            if(losowanie > 5)                               // atak szybki
            {
                //M.P.
                monster.animacjaMonster = 1;
                DMGforCHAMP(1);
                control.anim1.SetInteger("behaviour", 4);
            }
        }

        miniblock = false;
    }


    void DMGforMONSTER(int jakiatak)
    {
        damage_M = 0;
        damage_C = 0;
        damage_C = Random.Range(5, 10);
        damage_C = damage_C * 1;
        Heath_Monster = Heath_Monster - damage_C;

        showDMG = 1;
        miniblock = true;
    }

    void DMGforCHAMP(int jakiatak)
    {
        damage_M = 0;
        damage_M = Random.Range(10, 20);
        damage_M = damage_M * 1;
        Heath_Champion = Heath_Champion - damage_M;

        damage_C = 0;
        showDMG = 2;
        miniblock = true;
    }

    void afterClick()
    {
        wait = Time.time + 2.5f;
        monster.change = true;
        block = true;
        control.block_click = true;
        control.waiting = Time.time + 2.5f;
        timeToShowDmg = Time.time + 2;
    }

    void nothing()
    {
        damage_C = 0;
        damage_M = 0;
        showDMG = 3;

        miniblock = true;
    }

}
