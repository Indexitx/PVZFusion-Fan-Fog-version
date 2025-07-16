using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CactusStar : Shooter
{
    public int damage;
    public int bulletType;
    public Transform shoot1;
    public Transform shoot2;
    public Transform shoot3;
    public Transform shoot4;
    public Transform shoot5;
    public override GameObject AnimShoot()
    {
        GameObject g = CreateBullet.Instance.SetBullet(shoot1.position.x, shoot1.position.y, thePlantRow, bulletType, 8, Quaternion.Euler(0, 0, 90));
        GameObject g1 = CreateBullet.Instance.SetBullet(shoot2.position.x, shoot2.position.y, thePlantRow, bulletType, 8, Quaternion.Euler(0, 0, -90));
        GameObject g2 = CreateBullet.Instance.SetBullet(shoot3.position.x, shoot3.position.y, thePlantRow, bulletType, 8, Quaternion.Euler(0, 0, 180));
        GameObject g3 = CreateBullet.Instance.SetBullet(shoot4.position.x, shoot4.position.y, thePlantRow, bulletType, 8, Quaternion.Euler(0, 0, 45));
        GameObject g4 = CreateBullet.Instance.SetBullet(shoot5.position.x, shoot5.position.y, thePlantRow, bulletType, 8, Quaternion.Euler(0, 0, -45));
        g.GetComponent<Bullet>().theBulletDamage = damage;
        g1.GetComponent<Bullet>().theBulletDamage = damage;
        g2.GetComponent<Bullet>().theBulletDamage = damage;
        g3.GetComponent<Bullet>().theBulletDamage = damage;
        g4.GetComponent<Bullet>().theBulletDamage = damage;
        g.GetComponent<Bullet>().isAttackGround = false;
        g1.GetComponent<Bullet>().isAttackGround = false;
        g2.GetComponent<Bullet>().isAttackGround = false;
        g3.GetComponent<Bullet>().isAttackGround = false;
        g4.GetComponent<Bullet>().isAttackGround = false;
        g.GetComponent<Bullet>().isAttackFlying = true;
        g1.GetComponent<Bullet>().isAttackFlying = true;
        g2.GetComponent<Bullet>().isAttackFlying = true;
        g3.GetComponent<Bullet>().isAttackFlying = true;
        g4.GetComponent<Bullet>().isAttackFlying = true;
        return g;
    }
    protected override GameObject SearchZombie()
    {
        foreach (GameObject item in GameAPP.board.GetComponent<Board>().zombieArray)
        {
            if (item != null)
            {
                Zombie component = item.GetComponent<Zombie>();
                if (component.shadow.transform.position.x < 9.2f && SearchUniqueZombie(component) && component.isFlying)
                {
                    return item;
                }
            }
        }
        return null;
    }
}
