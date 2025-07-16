using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JackboxMiner : MinerZombie
{
    public float timeToExplode = 25f;
    public GameObject particle;
    public bool pop;
    public override void TrueAwake()
    {
        miningSpeed = 20;
        theMiningPower = 12000;
    }
    public override void Function()
    {
        Explode();
    }   
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
                    timeToExplode = 0.2f;
                }
                else
                {
                    Explode();
                    pop = false;
                    theMiningPower += 1000;
                    miningSpeed += 5;
                    timeToExplode = 25f;
                }
            }
        }
        
        
    }
    public void Explode()
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
    }
    public override void Dying()
    {
        base.Dying();
        Explode();
        if (!isMindControlled)
        {
            CreateZombie.Instance.SetZombie(Mathf.RoundToInt(transform.position.x), theZombieRow, 114);
        }
        if (isMindControlled)
        {
            CreateZombie.Instance.SetZombieWithMindControl(Mathf.RoundToInt(transform.position.x), theZombieRow, 114);
        }
        DestoryZombie();
    }

}
