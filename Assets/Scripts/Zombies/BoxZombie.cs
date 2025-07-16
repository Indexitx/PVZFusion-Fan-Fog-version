using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxZombie : Zombie
{
    public float timeToExplode = 40f;
    public GameObject particle;
    public bool pop;
    // Start is called before the first frame update
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
                    pop = false;
                    timeToExplode = 999f;
                    Charred();
                }
            }
        }
        
        
    }
}
