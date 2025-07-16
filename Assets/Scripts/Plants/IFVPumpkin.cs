using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IFVPumpkin : Pumpkin
{
    protected override void Awake()
    {
        base.Awake();
        StartCoroutine(CheckHybridization());
    }
    IEnumerator CheckHybridization()
    {
        yield return new WaitForSeconds(10f);
        foreach (GameObject g in Board.Instance.plantArray)
        {
            if(g == null) continue;
            Plant p = g.GetComponent<Plant>();
            if (!p.isDrone) continue;
            if(p.thePlantRow == thePlantRow && p.thePlantColumn == thePlantColumn)
            {
                if(p.thePlantType == 1085)
                {
                    CreatePlant.Instance.Hybridizate(this, p, 912);
                }
            }
        }
        StartCoroutine(CheckHybridization());
    }
}
