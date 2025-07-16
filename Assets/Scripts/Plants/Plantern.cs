using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plantern : Plant
{
    public float radius;
    public float radius2 = 1f;
    public float cooldown;
    protected override void Awake()
    {
        base.Awake();
        cooldown = 120f;
        StartCoroutine(Check());
    }
    IEnumerator Check()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);
        foreach (Collider2D c in colliders)
        {
            if (c.TryGetComponent<Fog>(out Fog fog))
            {
                fog.Dispel(true, 1f);
            }
        }
        foreach(GameObject g in Board.Instance.plantArray)
        {
            if (g == null) continue;
            if (TryGetComponent<LanternEffected>(out LanternEffected e))
            {
                e.RemovePlantern(this);
            }
        }
        Collider2D[] colliders2 = Physics2D.OverlapCircleAll(transform.position, radius2);
        foreach (Collider2D c in colliders2)
        {
            if (c.TryGetComponent<LanternEffected>(out LanternEffected l))
            {
                if (l.planterns.Contains(this)) continue;
                l.AddPlantern(this);
            }
        }
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(Check());
    }
}
