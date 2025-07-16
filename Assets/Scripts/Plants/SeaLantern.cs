using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaLantern : Plantern
{
    public bool isGrowen;
    public override void SpecialUpdate()
    {
        if (isGrowen) return;
        cooldown -= Time.deltaTime;
        if (cooldown <= 0 && !isGrowen)
        {
            isGrowen = true;
            radius = radius * 1.5f;
            radius2 = radius2 * 1.5f;
            anim.SetTrigger("grow");
        }
    }
}
