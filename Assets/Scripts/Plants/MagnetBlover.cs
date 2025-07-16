using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetBlover : Blover
{
    public float radius;
    public GameObject target;
    public float timering;
    public float startTimering;
    public bool isStealed;
    protected override void Awake()
    {
        base.Awake();
        StartCoroutine(Steal());
        startTimering = 3f;
    }
    public override void SpecialUpdate()
    {
        base.SpecialUpdate();
        if (isStealed)
        {
            timering -= Time.deltaTime;
            if (timering <= 0)
            {
                isStealed = false;
            }
        }

    }
    IEnumerator Steal()
    {
        if (!isStealed)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);
            foreach (Collider2D c in colliders)
            {
                if (c.TryGetComponent<Zombie>(out Zombie z))
                {
                    if (z.canBeStealed)
                    {
                        if (isStealed) continue;
                        z.Steal();
                        GameObject g = CreateItem.Instance.SetItem(z.transform.position.x, z.transform.position.y, z.stealObject);
                        g.transform.position = Vector3.MoveTowards(g.transform.position, target.transform.position, 5 * Time.deltaTime);
                        isStealed = true;
                        timering = 3f;
                        Debug.Log("q");
                    }
                }
            }
        }
        Debug.Log("i");
        yield return new WaitForSeconds(1.5f);
        Debug.Log("j");
        StartCoroutine(Steal());
    }
}
