using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagneCactusBullet : Bullet
{
    protected override void HitZombie(GameObject zombie)
    {
        Zombie component = zombie.GetComponent<Zombie>();
        component.TakeDamage(0, theBulletDamage);
        if(component.magnetCactusProjMax != 0)
        {
            component.magnetCactusProj++;
            if(component.magnetCactusProj >= component.magnetCactusProjMax)
            {
                component.TakeDamage(0, 1000000);
            }
        }
        PlaySound(component);
        Die();
    }
}
