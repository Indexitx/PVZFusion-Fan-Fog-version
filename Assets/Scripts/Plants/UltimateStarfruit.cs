using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltimateStarfruit : Shooter
{
    public Transform shoot1;
    public Transform shoot2;
    public Transform shoot3;
    public Transform shoot4;
    public Transform shoot5;
    protected override void Awake()
    {
        base.Awake();
        Board.Instance.UltimateStarActivate();
    }
    public override GameObject AnimShoot()
    {
        CheckMagnetSystem();
        int l = GetComponent<LanternEffected>().CheckLightLevel();
        GameObject g = null;
        GameObject g1 = null;
        GameObject g2 = null;
        GameObject g3 = null;
        GameObject g4 = null;
        if (l >= 2)
        {
            Board.Instance.SuperMagnetStarActivate();
        }
        g = CreateBullet.Instance.SetBullet(shoot1.position.x, shoot1.position.y, thePlantRow, 48, 8, Quaternion.Euler(0, 0, 90));
        g1 = CreateBullet.Instance.SetBullet(shoot2.position.x, shoot2.position.y, thePlantRow, 48, 8, Quaternion.Euler(0, 0, -90));
        g2 = CreateBullet.Instance.SetBullet(shoot3.position.x, shoot3.position.y, thePlantRow, 48, 8, Quaternion.Euler(0, 0, 180));
        g3 = CreateBullet.Instance.SetBullet(shoot4.position.x, shoot4.position.y, thePlantRow, 48, 8, Quaternion.Euler(0, 0, 45));
        g4 = CreateBullet.Instance.SetBullet(shoot5.position.x, shoot5.position.y, thePlantRow, 48, 8, Quaternion.Euler(0, 0, -45));
        g.GetComponent<Bullet>().theBulletDamage = Mathf.RoundToInt((30 + l * 15) * damageModifier);
        g1.GetComponent<Bullet>().theBulletDamage = Mathf.RoundToInt((30 + l * 15) * damageModifier);
        g2.GetComponent<Bullet>().theBulletDamage = Mathf.RoundToInt((30 + l * 15) * damageModifier);
        g3.GetComponent<Bullet>().theBulletDamage = Mathf.RoundToInt((30 + l * 15) * damageModifier);
        g4.GetComponent<Bullet>().theBulletDamage = Mathf.RoundToInt((30 + l * 15) * damageModifier);
        g.GetComponent<Bullet>().isAttackFlying = true;
        g1.GetComponent<Bullet>().isAttackFlying = true;
        g2.GetComponent<Bullet>().isAttackFlying = true;
        g3.GetComponent<Bullet>().isAttackFlying = true;
        g4.GetComponent<Bullet>().isAttackFlying = true;
        GetComponent<LanternEffected>().Upd();
        return g;
    }
    protected override GameObject SearchZombie()
    {
        foreach (GameObject item in GameAPP.board.GetComponent<Board>().zombieArray)
        {
            if (item != null)
            {
                Zombie component = item.GetComponent<Zombie>();
                if (component.shadow.transform.position.x < 9.2f && SearchUniqueZombie(component))
                {
                    return item;
                }
            }
        }
        return null;
    }
    public int GetUltimateMeteorWeight()
    {
        int l = GetComponent<LanternEffected>().CheckLightLevel();
        int num = l + 2;
        GetComponent<LanternEffected>().Upd();
        return num;
    }
}
