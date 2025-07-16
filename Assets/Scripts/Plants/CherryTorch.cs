using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CherryTorch : Plant
{
    public int fireTimes;
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Bullet>(out var component) && !(component.torchWood == base.gameObject) && !component.isZombieBullet && (component.theMovingWay == 2 || component.theBulletRow == thePlantRow) && component.theBulletType == 0 && Board.Instance.YellowFirePea(component, this))
        {
            fireTimes++;
            if (fireTimes > 29)
            {
                fireTimes = 0;
                SummonCherry();
            }
        }
        if (collision.TryGetComponent<SuperCherryBullet>(out var b))
        {
            SummonCherry(b.GetComponent<Bullet>().theBulletDamage);
            b.GetComponent<Bullet>().Die();
        }
    }

    protected virtual void SummonCherry(int dmg = 300)
    {
        GameObject g = CreateBullet.Instance.SetBullet(transform.position.x + 1f, transform.position.y + 1f, thePlantRow, 3, 6);
        g.GetComponent<Bullet>().theBulletDamage = dmg;
    }
}
