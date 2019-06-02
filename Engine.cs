using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Engine : MonoBehaviour {

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

    float time;
    int losowanie;                      // zmienna decyzyjna, uzywana w kazdym losowaniu rand()
    int damage_C;                       // obliczana ilosc obrazen championa dla monstera
    int damage_M;                       // obliczana ilosc obrazen monstera dla championa

    float wait;                         // czas blokady klawiatury
    bool block;                         // blokada klawiatury

    void Start () {
        monster = FindObjectOfType<Monster>();
        control = FindObjectOfType<Controller>();

    //    time = Time.time + 3;
        timeToShowDmg = Time.time - 1;
        time = Time.time + 4;

        Heath_Champion = 100;
        Heath_Monster = 100;

        block = false;
        wait = Time.time - 1;
	}

    private void FixedUpdate()
    {
        HP_M.text = "MONSTER " + Heath_Monster + "/100";
        HP_C.text = Heath_Champion + "/100 CHAMPION";

        if(timeToShowDmg - Time.time > 0)
        {
            switch(showDMG)
            {
                case 1:
                    {
                        if (damage_C == 0)  {   damage_text.text = "missed"; damage_text_M.text = "";    }
                        else{   damage_text.text = damage_C + " dmg"; damage_text_M.text = "";  }
                    }
                    break;
                case 2:
                    {
                        if (damage_M == 0) { damage_text_M.text = "missed"; damage_text.text = ""; }
                        else { damage_text_M.text = damage_M + " dmg"; damage_text.text = ""; }
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

        if(wait - Time.time < 0)
        {
            monster.change = false;
            block = false;
            control.block_click = false;
        }

        // atak Monster'a po czekaniu
        if((time - Time.time < 0) && !block)
        {
            monster.change = true;
            block = true;
            control.block_click = true;
            wait = Time.time + 2.5f;
            time = Time.time + 4.55f;

            losowanie = Random.Range(1, 11);

            if(losowanie > 0 && losowanie <= 4) // atak silny
            {
                monster.animacjaMonster = 2;
                losowanie = Random.Range(1, 11);

                if(losowanie > 0 && losowanie <= 8)
                {
                    // M. P.
                    DMGforCHAMP(2);
                    timeToShowDmg = Time.time + 2;
                }
                else
                {
                    // NIC
                    nothing();
                }
            }
            else                                // atak szybki
            {
                //M.P.
                monster.animacjaMonster = 1;
                DMGforCHAMP(1);
                timeToShowDmg = Time.time + 2;
            }
        }

        if((Input.GetKeyDown(KeyCode.Q)) && !block)
        {
            afterClick();

            losowanie = Random.Range(1, 11);
            if(losowanie > 0 && losowanie <= 4)     // atak szybki
            {
                monster.animacjaMonster = 1;

                losowanie = Random.Range(1, 11);

                if(losowanie > 0 && losowanie <=4)  // Ch.P.
                {
                    timeToShowDmg = Time.time + 2;
                    DMGforMONSTER(1);
                }

                if(losowanie > 4 && losowanie <=7)  // M. P.
                {
                    timeToShowDmg = Time.time + 2;
                    DMGforCHAMP(1);
                }

                if(losowanie > 7 && losowanie <=10) // NIC
                {
                    nothing();
                }
            }

            if (losowanie > 4 && losowanie <= 8)    // obrona
            {
                monster.animacjaMonster = 3;

                losowanie = Random.Range(1, 11);
                if(losowanie > 0 && losowanie <= 8) // NIC 
                {
                    nothing();
                }
                else                               // Ch.P.
                {
                    timeToShowDmg = Time.time + 2;
                    DMGforMONSTER(1);
                }
            }

            if (losowanie > 8 && losowanie <= 10)   // nic
            {
                monster.animacjaMonster = 9;
                timeToShowDmg = Time.time + 2;
                //Ch.P.
                DMGforMONSTER(1);
            }

        }

        
        if((Input.GetKeyDown(KeyCode.W)) && !block)             // sredni atak
        {
            afterClick();

            losowanie = Random.Range(1, 11);

            if (losowanie > 0 && losowanie <= 3)    // atak szybki
            {
                monster.animacjaMonster = 1;

                losowanie = Random.Range(1, 11);

                if (losowanie > 0 && losowanie <=4) // Ch. P
                {
                    DMGforMONSTER(2);
                    timeToShowDmg = Time.time + 2;
                }
                if(losowanie > 4 && losowanie <= 7) // M. P.
                {
                    DMGforCHAMP(1);
                    timeToShowDmg = Time.time + 2;
                }
                if(losowanie > 7 && losowanie <=11) // NIC
                {
                    nothing();
                }
            }
            if(losowanie > 3 && losowanie <=6)      // atak silny
            {
                monster.animacjaMonster = 2;

                losowanie = Random.Range(1, 11);

                if(losowanie > 0 && losowanie <=6)  // Ch.P.
                {
                    DMGforMONSTER(2);
                    timeToShowDmg = Time.time + 2;
                }
                if(losowanie > 6 && losowanie <= 8) // M.P.
                {
                    DMGforCHAMP(2);
                    timeToShowDmg = Time.time + 2;
                }
                if(losowanie > 8 && losowanie <=10) // NIC
                {
                    nothing();
                }
            }
            if(losowanie > 6 && losowanie <=10)     // obrona
            {
                monster.animacjaMonster = 3;

                losowanie = Random.Range(1, 11);

                if(losowanie > 0 && losowanie <= 8) // NIC
                {
                    nothing();
                }
                else                                // Ch.P.
                {
                    DMGforMONSTER(2);
                    timeToShowDmg = Time.time + 2;
                }
            }
        }

        if((Input.GetKeyDown(KeyCode.E)) && !block)
        {
            afterClick();

            losowanie = Random.Range(1, 11);

            if(losowanie > 0 && losowanie <= 2)     // szybki atak
            {
                monster.animacjaMonster = 1;

                losowanie = Random.Range(1, 11);

                if(losowanie > 0 && losowanie <= 4)
                {
                    // Ch.P.
                    DMGforMONSTER(3);
                    timeToShowDmg = Time.time + 2;
                }
                if(losowanie > 4 && losowanie <= 7)
                {
                    // M.P.
                    DMGforCHAMP(1);
                    timeToShowDmg = Time.time + 2;
                }
                if(losowanie > 7 && losowanie <= 10)
                {
                    // NIC
                    nothing();
                }
            }

            if(losowanie > 2 && losowanie <= 4)     // atak silny
            {
                monster.animacjaMonster = 2;

                losowanie = Random.Range(1, 11);

                if(losowanie > 0 && losowanie <= 6)
                {
                    // Ch. P.
                    DMGforMONSTER(2);
                    timeToShowDmg = Time.time + 2;
                }
                if(losowanie > 6 && losowanie <= 8)
                {
                    // M.P.
                    DMGforCHAMP(2);
                    timeToShowDmg = Time.time + 2;
                }
                if (losowanie > 8 && losowanie <= 10)
                {
                    // NIC
                    nothing();
                }
            }

            if(losowanie > 4 && losowanie <= 10)    // obrona
            {
                monster.animacjaMonster = 3;

                losowanie = Random.Range(1, 11);

                if(losowanie > 0 && losowanie <= 8)
                {
                    // NIC
                    nothing();
                }
                else
                {
                    //Ch. P.
                    DMGforMONSTER(2);
                    timeToShowDmg = Time.time + 2;
                }
            }
        }

        if((Input.GetKeyDown(KeyCode.Space)) && !block)
        {
            afterClick();

            losowanie = Random.Range(1, 11);

            if(losowanie > 0 && losowanie <= 5)     // atak szybki
            {
                monster.animacjaMonster = 1;

                losowanie = Random.Range(1, 11);

                if(losowanie > 0 && losowanie <= 8)
                {
                    // M. P.
                    DMGforCHAMP(1);
                    timeToShowDmg = Time.time + 2;
                }
                else
                {
                    // NIC
                    nothing();
                }
            }

            if (losowanie > 5 && losowanie <= 6)     // atak silny
            {
                monster.animacjaMonster = 2;

                losowanie = Random.Range(1, 11);

                if(losowanie > 0 && losowanie <= 6)
                {
                    // M. P.
                    DMGforCHAMP(2);
                    timeToShowDmg = Time.time + 2;
                }
                else
                {
                    // NIC
                    nothing();
                }
            }

            if(losowanie > 6 && losowanie <= 10)    // NIC
            {
                // NIC
                nothing();
            }
        }





	}


    void DMGforMONSTER(int jakiatak)
    {
        damage_C = 0;
        damage_C = Random.Range(5, 10);
        damage_C = damage_C * jakiatak;
        Heath_Monster = Heath_Monster - damage_C;

        damage_M = 0;
        showDMG = 1;

    }

    void DMGforCHAMP(int jakiatak)
    {
        damage_M = 0;
        damage_M = Random.Range(10, 20);
        damage_M = damage_M * jakiatak;
        Heath_Champion = Heath_Champion - damage_M;

        damage_C = 0;
        showDMG = 2;

    }

    void afterClick()
    {
        monster.change = true;
        block = true;
        control.block_click = true;
        wait = Time.time + 2.5f;
        time = Time.time + 3.5f;
    }

    void nothing()
    {
        damage_C = 0;
        damage_M = 0;
        showDMG = 3;
        timeToShowDmg = Time.time + 2;
    }


}

