using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarPumpkin : Pumpkin
{
    public Transform shoot;
    public void Shoot()
    {
        GameObject g = CreateBullet.Instance.SetBullet(shoot.position.x, shoot.position.y, thePlantRow, 40, 8);
        GameObject g1 = CreateBullet.Instance.SetBullet(shoot.position.x, shoot.position.y, thePlantRow, 40, 9);
        GameObject g2 = CreateBullet.Instance.SetBullet(shoot.position.x, shoot.position.y, thePlantRow, 40, 7);
        GameObject g3 = CreateBullet.Instance.SetBullet(shoot.position.x, shoot.position.y, thePlantRow, 40, 10);
        GameObject g4 = CreateBullet.Instance.SetBullet(shoot.position.x, shoot.position.y, thePlantRow, 40, 11);
        g.GetComponent<Bullet>().theBulletDamage = 20;
        g1.GetComponent<Bullet>().theBulletDamage = 20;
        g2.GetComponent<Bullet>().theBulletDamage = 20;
        g3.GetComponent<Bullet>().theBulletDamage = 20;
        g4.GetComponent<Bullet>().theBulletDamage = 20;
    }
    public override void TakeDamage(int damage, Zombie z = null)
    {
        base.TakeDamage(damage, z);
        Shoot();
    }
}
