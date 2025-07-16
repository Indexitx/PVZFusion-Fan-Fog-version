using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaMagnet : MagnetShroom
{
    public bool isGrowen;
    
    public override void SpecialUpdate()
    {
        base.SpecialUpdate();
        if (!isGrowen)
        {
            cooldown -= Time.deltaTime;
            if (cooldown <= 0 )
            {
                isGrowen = true;
                startTimering = 25;
                radius = radius * 2;
                anim.SetTrigger("grow");
            }
        }
    }
}
