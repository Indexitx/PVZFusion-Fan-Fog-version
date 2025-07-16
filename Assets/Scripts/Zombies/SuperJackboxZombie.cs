using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperJackboxZombie : ArmorZombie
{
    public float timeToExplode = 55f;
    public GameObject particle;
    public bool pop;
    public override void SpecialUpdate()
    {
        if (haveInstrument && theStatus != 1 && GameAPP.theGameStatus == 0)
        {
            timeToExplode -= Time.deltaTime;
            if (timeToExplode <= 0)
            {
                if (!pop)
                {
                    pop = true;
                    anim.SetTrigger("pop");
                    timeToExplode = 1f;
                }
                else
                {
                    GameObject g = Instantiate(particle, transform.position, Quaternion.identity);
                    ZombieBomb b = g.GetComponent<ZombieBomb>();
                    if (isMindControlled)
                    {
                        b.isEnemy = false;
                    }
                    else
                    {
                        b.isEnemy = true;
                    }
                    b.bombRow = theZombieRow;
                    b.bombType = 1;
                    timeToExplode = 999f;
                    theStatus = 5;
                }
            }
        }
        
    }
    public override void Dying()
    {
        base.Dying();
        if (!pop)
        {
            if (!isMindControlled)
            {
                CreateZombie.Instance.SetZombie(Mathf.RoundToInt(transform.position.x), theZombieRow, 25);
            }
            if (isMindControlled)
            {
                CreateZombie.Instance.SetZombieWithMindControl(Mathf.RoundToInt(transform.position.x), theZombieRow, 25);
            }
        }
        DestoryZombie();

    }
}
