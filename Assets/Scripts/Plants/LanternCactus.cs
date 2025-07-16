using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanternCactus : Cactus
{
    public float timeToHide;
    public bool isHere;
    public GameObject laser;
    public LanternEffected e;
    public int lightLevel;
    public override void SpecialUpdate()
    {
        if (!isHere) return;
        timeToHide -= Time.deltaTime;
        if(timeToHide <= 0)
        {
            isHere = false;
            laser.SetActive(false);
        }
    }
    public override GameObject AnimShoot()
    {
        Vector3 position = base.transform.Find("Shoot").transform.position;
        float theX = position.x + 0.1f;
        float y = position.y;
        int theRow = thePlantRow;
        GameObject obj = null;
        lightLevel = e.CheckLightLevel();
        Debug.Log(lightLevel);
        e.Upd();
        if (lightLevel <= 0)
        {
            obj = board.GetComponent<CreateBullet>().SetBullet(theX, y, theRow, 39, 0);
        }
        else if(lightLevel == 1)
        {
            obj = board.GetComponent<CreateBullet>().SetBullet(theX, y, theRow, 42, 0);
        }
        else if(lightLevel >= 2)
        {
            AttackZombie();
            laser.SetActive(true);
            isHere = true;
            timeToHide = 0.4f;
            return null;
        }
        if (obj != null)
        {
            obj.GetComponent<Bullet>().theBulletDamage = 20;
        }
        GameAPP.PlaySound(Random.Range(3, 5));
        return obj;
    }
    public override void AnimShoot2()
    {
        Vector3 position = base.transform.Find("Shoot2").transform.position;
        float theX = position.x + 0.1f;
        float y = position.y;
        int theRow = thePlantRow;
        GameObject obj = null;
        lightLevel = e.CheckLightLevel();
        Debug.Log(lightLevel);
        e.Upd();
        if (lightLevel <= 0)
        {
            obj = board.GetComponent<CreateBullet>().SetBullet(theX, y, theRow, 39, 0);
        }
        else if (lightLevel == 1)
        {
            obj = board.GetComponent<CreateBullet>().SetBullet(theX, y, theRow, 42, 0);
        }
        else if (lightLevel == 2)
        {
            AttackZombie2();
            laser.SetActive(true);
            isHere = true;
            timeToHide = 0.4f;
        }
        else if(lightLevel >= 3)
        {
            AttackZombie2();
            AnimShoot3();
            laser.SetActive(true);
            isHere = true;
            timeToHide = 0.4f;
        }
        if(obj != null)
        {
            obj.GetComponent<Bullet>().theBulletDamage = 20;
            obj.GetComponent<Bullet>().isAttackFlying = true;
            obj.GetComponent<Bullet>().isAttackGround = false;
        }
        
        GameAPP.PlaySound(Random.Range(3, 5));
    }
    public void AnimShoot3()
    {
        Vector3 position = base.transform.Find("Shoot2").transform.position;
        float theX = position.x + 0.1f;
        float y = position.y;
        int theRow = thePlantRow;
        GameObject obj = null;
        lightLevel = e.CheckLightLevel();
        Debug.Log(lightLevel);
        e.Upd();
        if (lightLevel <= 0)
        {
            obj = board.GetComponent<CreateBullet>().SetBullet(theX, y, theRow, 39, 0);
        }
        else if (lightLevel == 1)
        {
            obj = board.GetComponent<CreateBullet>().SetBullet(theX, y, theRow, 42, 0);
        }
        else if (lightLevel == 2)
        {
            AttackZombie2();
            laser.SetActive(true);
            isHere = true;
            timeToHide = 0.4f;
        }
        else if (lightLevel >= 3)
        {
            AttackZombie2();
            AnimShoot3();
            laser.SetActive(true);
            isHere = true;
            timeToHide = 0.4f;
        }
        if (obj != null)
        {
            obj.GetComponent<Bullet>().theBulletDamage = 20;
            obj.GetComponent<Bullet>().isAttackFlying = true;
            obj.GetComponent<Bullet>().isAttackGround = false;
        }
        GameAPP.PlaySound(Random.Range(3, 5));
    }
    private void AttackZombie()
    {
        float x = shadow.transform.position.x;
        bool flag = false;
        foreach (GameObject item in board.zombieArray)
        {
            if (item != null)
            {
                Zombie component = item.GetComponent<Zombie>();
                if (component.theZombieRow == thePlantRow && component.shadow.transform.position.x > x && AttackUniqueZombie(component))
                {
                    if (!component.isFlying)
                    {
                        zombieList.Add(component);
                        int i = (lightLevel - 2) * 10;
                        if(i < 0)
                        {
                            i = 0;
                        }
                        int damage = 20 + i;
                        component.TakeDamage(0, damage);
                        flag = true;
                    }
                }
            }
        }
        lightLevel = 0;
    }
    private void AttackZombie2()
    {
        float x = shadow.transform.position.x;
        bool flag = false;
        foreach (GameObject item in board.zombieArray)
        {
            if (item != null)
            {
                Zombie component = item.GetComponent<Zombie>();
                if (component.theZombieRow == thePlantRow && component.shadow.transform.position.x > x && AttackUniqueZombie(component))
                {
                    if (component.isFlying)
                    {
                        zombieList.Add(component);
                        int i = (lightLevel - 2) * 10;
                        if (i < 0)
                        {
                            i = 0;
                        }
                        int damage = 20 + i;
                        component.TakeDamage(0, damage);
                        flag = true;
                    }
                }
            }
        }
        lightLevel = 0;
    }
}
