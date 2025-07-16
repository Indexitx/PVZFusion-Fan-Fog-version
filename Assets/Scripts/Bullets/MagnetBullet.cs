using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetBullet : Bullet
{
    protected override void HitZombie(GameObject zombie)
    {
        Zombie component = zombie.GetComponent<Zombie>();
        if(component.theFirstArmorHealth > 0 || component.theSecondArmorHealth > 0)
        {
            component.TakeDamage(0, theBulletDamage * 2);
        }
        component.TakeDamage(0, theBulletDamage);
        Object.Instantiate(GameAPP.particlePrefab[42], base.transform.position, Quaternion.identity, Board.Instance.transform);
        PlaySound(component);
        Die();
    }
}
