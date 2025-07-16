using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CactusBloverPumpkinDrone : Bullet
{
    protected override void HitZombie(GameObject zombie)
    {
        Zombie component = zombie.GetComponent<Zombie>();
        GameAPP.PlaySound(0);
        SetFume();
        Die();
        Blow();
    }
    public virtual void Blow()
    {
        GameObject[] gs = GameObject.FindGameObjectsWithTag("Fog");
        foreach (GameObject g in gs)
        {
            if (g == null) continue;
            if (g.TryGetComponent<Fog>(out Fog fog))
            {
                fog.Dispel(true, 15);
            }
        }
        foreach (GameObject g in Board.Instance.zombieArray)
        {
            if (g == null) continue;
            if (g.TryGetComponent<Zombie>(out Zombie z))
            {
                z.CloverAttacked();
            }
        }
    }

    private void SetFume()
    {
        Object.Instantiate(GameAPP.particlePrefab[45], base.transform.position, Quaternion.identity, GameAPP.board.transform).GetComponent<ParticleSystem>().GetComponent<Renderer>()
            .sortingLayerName = $"particle{theBulletRow}";
        AttackZombie();
    }

    private void AttackZombie()
    {
        Collider2D[] array = Physics2D.OverlapCircleAll(base.transform.position, 0.5f);
        foreach (Collider2D collider2D in array)
        {
            if (collider2D != null && collider2D.TryGetComponent<Zombie>(out var component) && (!component.gameObject.TryGetComponent<PolevaulterZombie>(out var component2) || component2.polevaulterStatus != 1) && !component.isMindControlled)
            {
                if (component.isFlying)
                {
                    component.TakeDamage(0, theBulletDamage * 2);
                }
                else
                {
                    component.TakeDamage(0, theBulletDamage);
                }
            }
        }
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
        if (gameObject == null)
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
