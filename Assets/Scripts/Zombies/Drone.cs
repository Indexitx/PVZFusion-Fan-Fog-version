using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{
    public Vector3 startPosition;
    public GameObject zombie;
    protected Animator anim;
    protected Vector3 targetPosition;
    protected Vector3 currentDireciton;
    protected float timeToAttack;
    protected bool haveTarget;
    private void Start()
    {
        startPosition = base.transform.position;
        anim = GetComponent<Animator>();
    }
    public void Update()
    {
        timeToAttack -= Time.deltaTime;
        base.transform.position = Vector2.MoveTowards(base.transform.position, targetPosition, 4f * Time.deltaTime);
        if (GetNearestZombie() != null)
        {
            haveTarget = true;
            zombie = GetNearestZombie();
            targetPosition = new Vector3(zombie.GetComponent<Zombie>().shadow.transform.position.x - 1f, zombie.GetComponent<Zombie>().shadow.transform.position.y + 1.5f, transform.position.z);
        }
        else
        {
            haveTarget = false;
            targetPosition = startPosition;
            return;
        }
        if(Vector3.Distance(transform.position, targetPosition) <= 0.5f)
        {
            ShootUpdate();
            Debug.Log("o");
        }
    }
    protected virtual void ShootUpdate()
    {
        if(zombie != null)
        {
            if(zombie.GetComponent<Zombie>().theStatus != 1 && zombie.GetComponent<Zombie>().isFlying)
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
    public virtual void AnimShoot()
    {
        Vector3 position = base.transform.Find("Shoot").transform.position;
        float theX = position.x + 0.1f;
        float y = position.y;
        GameObject obj = Board.Instance.GetComponent<CreateBullet>().SetBullet(theX, y, zombie.GetComponent<Zombie>().theZombieRow, 39, 6);
        obj.GetComponent<Bullet>().theBulletDamage = 20;
        obj.GetComponent<Bullet>().isAttackFlying = true;
        obj.GetComponent<Bullet>().isAttackGround = false;
        GameAPP.PlaySound(Random.Range(3, 5));
    }
    protected virtual GameObject GetNearestZombie()
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
                    if ((component.isFlying))
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
        }
        if (gameObject != null)
        {
            CreateBullet.Instance.SetLayer(5, base.gameObject);
        }
        return gameObject;
    }
}
