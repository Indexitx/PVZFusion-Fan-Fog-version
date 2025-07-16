using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cactus : Shooter
{
    public bool isRised;
    public override GameObject AnimShoot()
    {
        Vector3 position = base.transform.Find("Shoot").transform.position;
        float theX = position.x + 0.1f;
        float y = position.y;
        int theRow = thePlantRow;
        GameObject obj = board.GetComponent<CreateBullet>().SetBullet(theX, y, theRow, 39, 0);
        obj.GetComponent<Bullet>().theBulletDamage = 20;
        GameAPP.PlaySound(Random.Range(3, 5));
        return obj;
    }
    public virtual void AnimShoot2()
    {
        Vector3 position = base.transform.Find("Shoot2").transform.position;
        float theX = position.x + 0.1f;
        float y = position.y;
        int theRow = thePlantRow;
        GameObject obj = board.GetComponent<CreateBullet>().SetBullet(theX, y, theRow, 39, 0);
        obj.GetComponent<Bullet>().theBulletDamage = 20;
        obj.GetComponent<Bullet>().isAttackFlying = true;
        obj.GetComponent<Bullet>().isAttackGround = false;
        GameAPP.PlaySound(Random.Range(3, 5));
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
                    if (!component.isFlying)
                    {
                        return item;
                    }
                }
            }
        }
        return null;
    }
    protected override GameObject SearchFlyingZombie()
    {
        foreach (GameObject item in GameAPP.board.GetComponent<Board>().zombieArray)
        {
            if (item != null)
            {
                Zombie component = item.GetComponent<Zombie>();
                if (component.theZombieRow == thePlantRow && component.shadow.transform.position.x < 9.2f && component.shadow.transform.position.x > shadow.transform.position.x && SearchUniqueZombie(component))
                {
                    if (component.isFlying)
                    {
                        return item;
                    }
                }
            }
        }
        return null;
    }
    protected override void PlantShootUpdate()
    {
        thePlantAttackCountDown -= Time.deltaTime;
        if (thePlantAttackCountDown < 0f)
        {
            thePlantAttackCountDown = thePlantAttackInterval;
            thePlantAttackCountDown += Random.Range(-0.1f, 0.1f);
            if (SearchFlyingZombie() != null)
            {
                anim.SetTrigger("shoot2");
            }
            else if (SearchZombie() != null)
            {
                anim.SetTrigger("shoot");
            }
            else if (Board.Instance.isScaredyDream && thePlantType == 9)
            {
                anim.SetTrigger("shoot");
            }
        }
        if(SearchFlyingZombie() != null && !isRised)
        {
            anim.SetBool("rise", true);
            isRised = true;
        }
        else if(SearchFlyingZombie() == null && isRised)
        {
            anim.SetBool("rise", false);
            isRised = false;
        }
    }
}
