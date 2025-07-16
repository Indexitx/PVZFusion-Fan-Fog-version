using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CherryDrone : Plant
{
    protected Collider2D[] colliders;
    public float timering;
    protected void Update()
    {
        base.Update();
        timering -= Time.deltaTime;
        if (timering <= 0 && SearchZombie() != null)
        {
            Attack();
        }
    }
    protected override GameObject SearchZombie()
    {
        Vector3 center = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z);
        colliders = Physics2D.OverlapCircleAll(center, 2, zombieLayer);
        Collider2D[] array = colliders;
        foreach (Collider2D collider2D in array)
        {
            if (collider2D.TryGetComponent<Zombie>(out var component) && Mathf.Abs(component.theZombieRow - thePlantRow) <= 1 && SearchUniqueZombie(component))
            {
                return collider2D.gameObject;
            }
        }
        return null;
    }
    public void Attack()
    {
        timering = 15;
        GameObject g = CreateBullet.Instance.SetBullet(transform.position.x, transform.position.y + 1f, thePlantRow, 3, 6);
        g.GetComponent<Bullet>().theBulletDamage = 300;
    }
}
