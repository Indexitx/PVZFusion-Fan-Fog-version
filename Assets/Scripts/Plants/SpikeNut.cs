using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeNut : WallNut
{
    public override void ZomboniAttacked(Zombie z)
    {
        TakeDamage(500);
        z.TakeDamage(0, 1000);
        z.Knockback(1f);
    }
}
