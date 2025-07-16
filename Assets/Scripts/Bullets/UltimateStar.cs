using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltimateStar : Bullet
{
    protected override void HitZombie(GameObject zombie)
    {
        Zombie component = zombie.GetComponent<Zombie>();
        if (component.theFirstArmorHealth > 0 || component.theSecondArmorHealth > 0)
        {
            component.TakeDamage(0, theBulletDamage * 2);
        }
        if (component.magnetCactusProjMax != 0)
        {
            component.magnetCactusProj+=0.5f;
            if (component.magnetCactusProj >= component.magnetCactusProjMax)
            {
                component.TakeDamage(0, 1000000);
            }
        }
        component.TakeDamage(0, theBulletDamage);
        Object.Instantiate(GameAPP.particlePrefab[42], base.transform.position, Quaternion.identity, Board.Instance.transform);
        PlaySound(component);
        Die();
    }
    protected virtual GameObject GetNearestZombie(float minDistance = 0f, bool onOtherLanes = true)
    {
        float num = float.MaxValue;
        GameObject gameObject = null;
        foreach (GameObject item in Board.Instance.zombieArray)
        {
            if (item != null)
            {
                Zombie component = item.GetComponent<Zombie>();
                if (!component.isMindControlled && component.theStatus != 1 && component.shadow.transform.position.x < 9.2f && component.theStatus != 7 && component.TryGetComponent<Collider2D>(out Collider2D component2) && Vector2.Distance(component2.bounds.center, base.transform.position) < num && Vector2.Distance(component2.bounds.center, base.transform.position) > minDistance)
                {
                    if (component.isFlying)
                    {
                        gameObject = item;
                        num = Vector2.Distance(component.GetComponent<Collider2D>().bounds.center, base.transform.position);
                    }   
                }
            }
        }
        if(gameObject == null)
        {
            foreach (GameObject item in Board.Instance.zombieArray)
            {
                if (item != null)
                {
                    Zombie component = item.GetComponent<Zombie>();
                    if (!component.isMindControlled && component.theStatus != 1 && component.shadow.transform.position.x < 9.2f && component.theStatus != 7 && component.TryGetComponent<Collider2D>(out Collider2D component2) && Vector2.Distance(component2.bounds.center, base.transform.position) < num && Vector2.Distance(component2.bounds.center, base.transform.position) > minDistance)
                    {
                        gameObject = item;
                        num = Vector2.Distance(component.GetComponent<Collider2D>().bounds.center, base.transform.position);
                    }
                }
            }
        }
        if (gameObject != null)
        {
            CreateBullet.Instance.SetLayer(5, base.gameObject);
        }
        return gameObject;
    }
}
