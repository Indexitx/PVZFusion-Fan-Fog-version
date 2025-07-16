using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightCactusBullet : Bullet
{
    protected override void HitZombie(GameObject zombie)
    {
        zombie.GetComponent<Zombie>().TakeDamage(0, theBulletDamage);
        GameAPP.PlaySound(70);
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (hitTimes >= 3 || !collision.CompareTag("Zombie") || (collision.TryGetComponent<PolevaulterZombie>(out var component) && component.polevaulterStatus == 1))
        {
            return;
        }
        Zombie component2 = collision.GetComponent<Zombie>();
        if (component2.theZombieRow != theBulletRow || component2.isMindControlled || component2.theStatus == 7)
        {
            return;
        }
        foreach (GameObject item in Z)
        {
            if (item != null && item == zombie)
            {
                return;
            }
        }
        hitTimes++;
        Z.Add(collision.gameObject);
        HitZombie(collision.gameObject);
        if (hitTimes == 3)
        {
            Die();
        }
    }
}
