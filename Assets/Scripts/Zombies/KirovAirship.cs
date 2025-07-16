using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KirovAirship : AirshipZombie
{
    public float time2;
    public override void SpecialUpdate()
    {
        time -= Time.deltaTime;
        time2 -= Time.deltaTime;
        if(time2 <= 0 && isWalking)
        {
            if (!isMindControlled)
            {
                GameObject g = CreateZombie.Instance.SetZombie(Mathf.RoundToInt(transform.position.x), theZombieRow, 113);
                g.GetComponent<Zombie>().theOriginSpeed = 1f;
            }
            if (isMindControlled)
            {
                GameObject g = CreateZombie.Instance.SetZombieWithMindControl(Mathf.RoundToInt(transform.position.x), theZombieRow, 113);
                g.GetComponent<Zombie>().theOriginSpeed = 1f;
            }
            time2 = 10f;
        }
        if (time <= 0)
        {
            isReady = true;
        }
    }
}
