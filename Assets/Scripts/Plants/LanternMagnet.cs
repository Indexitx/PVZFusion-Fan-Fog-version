using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanternMagnet : Plant
{
    public float radius;
    public GameObject target;
    public float timering;
    public float startTimering;
    public bool isStealed;
    public float cooldown;
    protected override void Awake()
    {
        base.Awake();
        StartCoroutine(Steal());
        startTimering = 45f;
        cooldown = 12f;
    }
    public override void SpecialUpdate()
    {
        if (isStealed)
        {
            timering -= Time.deltaTime;
        }
    }
    IEnumerator Steal()
    {
        if (!isStealed)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);
            foreach (Collider2D c in colliders)
            {
                if (isStealed) continue;
                if (c.TryGetComponent<Zombie>(out Zombie z))
                {
                    if (z.canBeStealed)
                    {
                        
                        z.Steal();
                        GameObject g = CreateItem.Instance.SetItem(z.transform.position.x, z.transform.position.y, z.stealObject);
                        g.transform.position = Vector3.MoveTowards(g.transform.position, target.transform.position, 5 * Time.deltaTime);
                        anim.SetTrigger("attrack");
                        isStealed = true;
                        int l = GetComponent<LanternEffected>().CheckLightLevel();
                        startTimering = 45 -( l * 5f);
                        radius = 2f + (l * 0.5f);
                        timering = startTimering;
                        GetComponent<LanternEffected>().Upd();
                    }
                }
            }
        }
        else
        {

            if (timering <= 0)
            {
                isStealed = false;
                anim.SetTrigger("recover");
            }
        }
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(Steal());
    }
}
