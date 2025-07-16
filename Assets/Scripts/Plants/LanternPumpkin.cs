using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanternPumpkin : Pumpkin
{
    public float timering;
    public override void SpecialUpdate()
    {
        timering -= Time.deltaTime;
        if (timering <= 0)
        {
            int l = GetComponent<LanternEffected>().CheckLightLevel();
            Recover(100 + (l * 125));
            if(l > 0)
            {
                theMaxReceivedDamage = 4000 - (l * 250);
            }
            else
            {
                theMaxReceivedDamage = 0;
            }
            GetComponent<LanternEffected>().Upd();
            timering = 25;
        }
    }
}
