using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunMagnet : Producer
{
    public Transform t;
    public int itemsIDArrayLength;
    protected override void Awake()
    {
        base.Awake();
        foreach(GameObject g in GameAPP.itemPrefab)
        {
            if (g == null) continue;
            else
            {
                if(g.TryGetComponent<Bucket>(out var b))
                {
                    itemsIDArrayLength++;
                }
                
            }
        }
    }
    protected override void ProduceSun()
    {
        base.ProduceSun();
        int i = Random.Range(0, 100);
        int j = Random.Range(-1, itemsIDArrayLength);
        if(i <= 35)
        {
            CreateItem.Instance.SetItem(t.position.x, t.position.y, j);
        }
        GameAPP.PlaySound(Random.Range(3, 5), 0.3f);
    }
}
