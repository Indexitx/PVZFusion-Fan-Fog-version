using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperStarPlant : Shooter
{
    public Transform shoot1;
    public Transform shoot2;
    public Transform shoot3;
    public Transform shoot4;
    public Transform shoot5;
    protected override void Awake()
    {
        base.Awake();
        Board.Instance.SuperStarActivate();
    }
    public override GameObject AnimShoot()
    {
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
        if (l <= 0)
        {
            g = CreateBullet.Instance.SetBullet(shoot1.position.x, shoot1.position.y, thePlantRow, 47, 0, Quaternion.Euler(0, 0, 90), true);
            g1 = CreateBullet.Instance.SetBullet(shoot2.position.x, shoot2.position.y, thePlantRow, 47, 0, Quaternion.Euler(0, 0, -90), true);
            g2 = CreateBullet.Instance.SetBullet(shoot3.position.x, shoot3.position.y, thePlantRow, 47, 7, true);
            g3 = CreateBullet.Instance.SetBullet(shoot4.position.x, shoot4.position.y, thePlantRow, 47, 0, Quaternion.Euler(0, 0, 45), true);
            g4 = CreateBullet.Instance.SetBullet(shoot5.position.x, shoot5.position.y, thePlantRow, 47, 0, Quaternion.Euler(0, 0, -45), true);
            g.GetComponent<Bullet>().theBulletDamage = Mathf.RoundToInt(60 * damageModifier);
            g1.GetComponent<Bullet>().theBulletDamage = Mathf.RoundToInt(60 * damageModifier);
            g2.GetComponent<Bullet>().theBulletDamage = Mathf.RoundToInt(60 * damageModifier);
            g3.GetComponent<Bullet>().theBulletDamage = Mathf.RoundToInt(60 * damageModifier);
            g4.GetComponent<Bullet>().theBulletDamage = Mathf.RoundToInt(60 * damageModifier);
        }
        else if (l == 1 || l == 2)
        {
            g = CreateBullet.Instance.SetBullet(shoot1.position.x, shoot1.position.y, thePlantRow, 47, 0);
            g1 = CreateBullet.Instance.SetBullet(shoot2.position.x, shoot2.position.y, thePlantRow, 47, 0);
            g2 = CreateBullet.Instance.SetBullet(shoot3.position.x, shoot3.position.y, thePlantRow, 47, 0);
            g3 = CreateBullet.Instance.SetBullet(shoot4.position.x, shoot4.position.y, thePlantRow, 47, 0);
            g4 = CreateBullet.Instance.SetBullet(shoot5.position.x, shoot5.position.y, thePlantRow, 47, 0);
            g.GetComponent<Bullet>().theBulletDamage = Mathf.RoundToInt(60 * damageModifier);
            g1.GetComponent<Bullet>().theBulletDamage = Mathf.RoundToInt(60 * damageModifier);
            g2.GetComponent<Bullet>().theBulletDamage = Mathf.RoundToInt(60 * damageModifier);
            g3.GetComponent<Bullet>().theBulletDamage = Mathf.RoundToInt(60 * damageModifier);
            g4.GetComponent<Bullet>().theBulletDamage = Mathf.RoundToInt(60 * damageModifier);
        }
        else
        {
            g = CreateBullet.Instance.SetBullet(shoot1.position.x, shoot1.position.y, thePlantRow, 47, 8, Quaternion.Euler(0, 0, 90));
            g1 = CreateBullet.Instance.SetBullet(shoot2.position.x, shoot2.position.y, thePlantRow, 47, 8, Quaternion.Euler(0, 0, -90));
            g2 = CreateBullet.Instance.SetBullet(shoot3.position.x, shoot3.position.y, thePlantRow, 47, 8, Quaternion.Euler(0, 0, 180));
            g3 = CreateBullet.Instance.SetBullet(shoot4.position.x, shoot4.position.y, thePlantRow, 47, 8, Quaternion.Euler(0, 0, 45));
            g4 = CreateBullet.Instance.SetBullet(shoot5.position.x, shoot5.position.y, thePlantRow, 47, 8, Quaternion.Euler(0, 0, -45));
            g.GetComponent<Bullet>().theBulletDamage = Mathf.RoundToInt(60 * damageModifier);
            g1.GetComponent<Bullet>().theBulletDamage = Mathf.RoundToInt(60 * damageModifier);
            g2.GetComponent<Bullet>().theBulletDamage = Mathf.RoundToInt(60 * damageModifier);
            g3.GetComponent<Bullet>().theBulletDamage = Mathf.RoundToInt(60 * damageModifier);
            g4.GetComponent<Bullet>().theBulletDamage = Mathf.RoundToInt(60 * damageModifier);
        }
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
                if (component.shadow.transform.position.x < 9.2f && SearchUniqueZombie(component) && !component.isFlying)
                {
                    return item;
                }
            }
        }
        return null;
    }
    public float GetNormalMeteorWeight()
    {
        GetComponent<LanternEffected>().Upd();
        return 2.7f;
    }
    public float GetMagnetMeteorWeight()
    {
        int l = GetComponent<LanternEffected>().CheckLightLevel();
        GetComponent<LanternEffected>().Upd();
        if (l < 4)
        {
            return 0;
        }
        return 3;
    }
}
