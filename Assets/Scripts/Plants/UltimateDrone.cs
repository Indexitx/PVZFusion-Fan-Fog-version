using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltimateDrone : Drone
{
    public override void AnimShoot()
    {
        GameAPP.PlaySound(Random.Range(0, 3));
        GameAPP.PlaySound(Random.Range(12, 14));
    }
    protected override GameObject GetNearestZombie()
    {
        float num = float.MaxValue;
        GameObject gameObject = null;
        foreach (GameObject item in Board.Instance.zombieArray)
        {
            if (item != null)
            {
                Zombie component = item.GetComponent<Zombie>();
                if (!component.isMindControlled && component.theStatus != 1 && component.shadow.transform.position.x < 9.2f && component.theStatus != 7 && component.TryGetComponent<Collider2D>(out Collider2D component2) && Vector2.Distance(component2.bounds.center, base.transform.position) < num)
                {
                    if (component.theStatus == 1)
                    {
                        continue;
                    }
                    gameObject = item;
                    Vector2 v = new Vector2(component.shadow.transform.position.x, component.shadow.transform.position.y);
                    num = Vector2.Distance(v, base.transform.position);
                }
            }
        }
        if (gameObject != null)
        {
            CreateBullet.Instance.SetLayer(5, base.gameObject);
        }
        return gameObject;
    }
    protected override void ShootUpdate()
    {
        if (zombie != null)
        {
            if (zombie.GetComponent<Zombie>().theStatus != 1)
            {

                if (timeToAttack <= 0)
                {
                    Debug.Log("k");
                    anim.SetTrigger("shoot");
                    timeToAttack = 2f;
                }
            }
            else
            {
                targetPosition = new Vector3(startPosition.x, startPosition.y);
                zombie = GetNearestZombie();
            }
        }
        else
        {
            targetPosition = new Vector3(startPosition.x, startPosition.y);
            zombie = GetNearestZombie();
        }
    }
}
