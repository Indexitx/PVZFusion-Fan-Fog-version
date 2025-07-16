using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : Bullet
{
    protected override void HitZombie(GameObject zombie)
    {
        Zombie component = zombie.GetComponent<Zombie>();
        component.TakeDamage(0, theBulletDamage);
        Object.Instantiate(GameAPP.particlePrefab[44], base.transform.position, Quaternion.identity, Board.Instance.transform);
        PlaySound(component);
        Die();
    }
}
