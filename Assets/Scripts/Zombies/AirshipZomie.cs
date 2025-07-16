using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirshipZombie : Zombie
{
    public GameObject g;
    public GameObject explode;
    public GameObject stady1;
    public GameObject stady2;
    public GameObject stady3;
    public float time;
    public bool isSmall;
    public bool isWalking;
    public bool isReady;
    protected override void OnTriggerStay2D(Collider2D collision)
    {
        if(collision != null)
        {
            if(collision.TryGetComponent<Plant>(out Plant p))
            {
                if(p.thePlantRow == theZombieRow)
                {
                    if (p != null && !isMindControlled)
                    {
                        LaunchExplode(p.transform);
                        return;
                    }
                }
                
            }
            if (collision.TryGetComponent<Zombie>(out Zombie z))
            {
                if(z.theZombieRow == theZombieRow)
                {
                    if (z.isMindControlled && !isMindControlled)
                    {
                        LaunchExplode(z.transform);
                        return;
                    }
                    if (!z.isMindControlled && isMindControlled)
                    {
                        LaunchExplode(z.transform);
                        return;
                    }
                }
                
            }
        }
    }
    protected override void MoveUpdate()
    {
        base.MoveUpdate();
        if(isWalking)
        {
            Knockback(-Time.deltaTime * theSpeed / 10);
        }
        
    }
    public void MU()
    {
        base.MoveUpdate();
    }
    public void LaunchExplode(Transform t)
    {
        if (isReady)
        {
            isReady = false;
            time = 1f;
            GameObject particle = Instantiate(g, t.position, Quaternion.identity);
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
            b.bombType = 2;
        }
    }
    public override void SpecialUpdate()
    {
        time -= Time.deltaTime;
        if(time <= 0)
        {
            isReady = true;
        }
    }
    protected override void BodyTakeDamage(int theDamage)
    {
        
        theHealth -= theDamage;
        if (theHealth > theMaxHealth / 3 * 2)
        {
            stady1.SetActive(true);
            stady2.SetActive(false);
            stady3.SetActive(false);
        }
        if (theHealth <= theMaxHealth / 3 * 2 && theHealth > theMaxHealth / 3)
        {
            stady1.SetActive(false);
            stady2.SetActive(true);
            stady3.SetActive(false);
        }
        if (theHealth <= theMaxHealth / 3)
        {
            stady1.SetActive(false);
            stady2.SetActive(false);
            stady3.SetActive(true);
        }
        if (theHealth <= 0f)
        {
            Die(2);
        }
    }
    public override void CloverAttacked()
    {
        Knockback(2f);
    }
    public void TD(int theDamageType, int theDamage)
    {
        base.TakeDamage(theDamageType, theDamage);
    }
    protected override void DieEvent()
    {
        GameObject particle = Instantiate(explode, transform.position, Quaternion.identity);
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
        if (!isSmall)
        {
            b.bombType = 1;
        }
        else
        {
            b.bombType = 2;
        }
        base.DestoryZombie();
    }
}
