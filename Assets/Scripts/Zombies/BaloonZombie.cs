using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaloonZombie : ArmorZombie
{
    protected override void Start()
    {
        base.Start();
        if (GameAPP.theGameStatus == 0 || GameAPP.theGameStatus == 1)
        {
        }
    }
    protected override void FirstArmorFall()
    {
        if(Board.Instance.roadType[theZombieRow] != 1)
        {
            anim.SetTrigger("fall");
            isFlying = false;
        }
        else
        {
            DestoryZombie();
        }
    }
    public override void CloverAttacked()
    {
        TakeDamage(0, 1000000);
    }
}
