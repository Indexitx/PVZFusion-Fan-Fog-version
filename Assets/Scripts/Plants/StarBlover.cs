using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarBlover : Plant
{
    public int absorbedDamage;
    public Transform p;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Bullet>(out var component) && !component.isFromStarBlover)
        {
            component.hasHitTarget = true;
            absorbedDamage += component.theBulletDamage;
            if (component.TryGetComponent<BigDoom>(out BigDoom i))
            {
                absorbedDamage += 600;
            }
            if (component.TryGetComponent<SuperCherryBullet>(out SuperCherryBullet o))
            {
                absorbedDamage += 300;
            }
            if (component.TryGetComponent<PuffLove>(out PuffLove u))
            {
                absorbedDamage += 40;
            }
            component.Die();
        }
    }
    public override void Click()
    {
        if(absorbedDamage >= 20 && SearchZombie() != null)
        {
            StartCoroutine(Shooting());
            anim.SetTrigger("blow");
        }
        
    }

    IEnumerator Shooting()
    {
        yield return new WaitForSeconds(0.05f);
        GameObject g = CreateBullet.Instance.SetBullet(p.position.x, p.position.y, thePlantRow, 40, 6);
        g.GetComponent<Bullet>().theBulletDamage = 20;
        g.GetComponent<Bullet>().isFromStarBlover = true;
        absorbedDamage -= 20;
        if(absorbedDamage >= 20 && SearchZombie() != null)
        {
            Debug.Log("k");
            StartCoroutine(Shooting());
        }
        else
        {
            anim.SetTrigger("stopblow");
        }
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
}
