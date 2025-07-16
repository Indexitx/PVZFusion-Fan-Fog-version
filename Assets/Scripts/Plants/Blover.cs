using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blover : Plant
{
    public float timeToKill = 5f;
    public int timeToAntidispelFog = 20;
    protected override void Awake()
    {
        base.Awake();
        Blow();
    }
    public virtual void Blow()
    {
        GameObject[] gs = GameObject.FindGameObjectsWithTag("Fog");
        foreach (GameObject g in gs)
        {
            if (g == null) continue;
            if (g.TryGetComponent<Fog>(out Fog fog))
            {
                fog.Dispel(true, timeToAntidispelFog);
            }
        }
        foreach (GameObject g in Board.Instance.zombieArray)
        {
            if (g == null) continue;
            if (g.TryGetComponent<Zombie>(out Zombie z))
            {
                z.CloverAttacked();
            }
        }
        BlowEffect();
    }
    public override void SpecialUpdate()
    {
        timeToKill -= Time.deltaTime;
        if(timeToKill <= 0)
        {
            Die();
        }
    }
    public virtual void BlowEffect()
    {

    }
}
