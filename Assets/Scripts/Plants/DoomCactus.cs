using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoomCactus : Shooter
{
    public int chance;
    public override GameObject AnimShoot()
    {
        Transform position = base.transform.Find("Shoot").transform;
        GameObject obj = null;
        GameObject obj1 = null;
        GameObject obj2 = null;
        float theX = position.position.x + 0.1f;
        float y = position.position.y;
        int theRow = thePlantRow;
        int i = Random.Range(0, 100);
        if(i < chance)
        {
            chance = 10;
            obj = board.GetComponent<CreateBullet>().SetBullet(theX, y, theRow, 41, 0);
            obj1 = board.GetComponent<CreateBullet>().SetBullet(theX, y, theRow, 41, 10);
            obj2 = board.GetComponent<CreateBullet>().SetBullet(theX, y, theRow, 41, 11);
            obj.GetComponent<Bullet>().theBulletDamage = 120;
            obj1.GetComponent<Bullet>().theBulletDamage = 120;
            obj2.GetComponent<Bullet>().theBulletDamage = 120;
            obj1.GetComponent<Bullet>().isAttackFlying = true;
            obj2.GetComponent<Bullet>().isAttackFlying = true;
        }
        else
        {
            chance += 6;
            obj = board.GetComponent<CreateBullet>().SetBullet(theX, y, theRow, 41, 0);
            obj.GetComponent<Bullet>().theBulletDamage = 40;
        }
        
        obj.GetComponent<Bullet>().isAttackFlying = true;
        GameAPP.PlaySound(Random.Range(3, 5));
        return obj;
    }
    protected override GameObject SearchZombie()
    {
        foreach (GameObject item in GameAPP.board.GetComponent<Board>().zombieArray)
        {
            if (item != null)
            {
                Zombie component = item.GetComponent<Zombie>();
                if (component.theZombieRow == thePlantRow && component.shadow.transform.position.x < 9.2f && component.shadow.transform.position.x > shadow.transform.position.x && SearchUniqueZombie(component))
                {
                    return item;
                }
            }
        }
        return null;
    }
}
