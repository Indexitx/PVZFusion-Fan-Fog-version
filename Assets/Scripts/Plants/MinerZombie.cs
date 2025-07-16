using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinerZombie : ArmorZombie
{
    public int miningSpeed;
    public int theMiningPower;
    public int miningProgress;
    public Plant attackingPlant;
    protected override void Awake()
    {
        base.Awake();
        miningSpeed = Random.Range(5, 20);
        theMiningPower = Random.Range(1000, 12000);
        TrueAwake();
    }
    public virtual void TrueAwake()
    {

    }
    public override void AttackPlant(Plant plant)
    {
        base.AttackPlant(plant);
        attackingPlant = plant;
        if (haveInstrument)
        {
            if (plant.minerImmunityLevel == 0)
            {
                if (plant.thePlantHealth > theMiningPower)
                {
                    miningProgress += miningSpeed;
                }
                else
                {
                    miningProgress += miningSpeed * 3;
                }
                if (miningProgress >= 100)
                {
                    Function();
                    miningProgress = 0;
                    attackingPlant.Die();
                }
            }
        }
        else if (plant.minerImmunityLevel == 1)
        {
            if (plant.minerImmunityLevel == 0)
            {
                if (plant.thePlantHealth > theMiningPower)
                {
                    miningProgress += miningSpeed / 2;
                }
                if (miningProgress >= 100)
                {
                    Function();
                    miningProgress = 0;
                    attackingPlant.Die();
                }
            }
        }
        
    }
    public virtual void Function()
    {

    }
}
