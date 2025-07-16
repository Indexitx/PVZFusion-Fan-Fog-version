using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaCactus : Cactus
{
    public bool isGrowen;
    public float cooldown;
    protected override void Awake()
    {
        base.Awake();
        
        cooldown = 120f;
    }
    public override void SpecialUpdate()
    {
        if (isGrowen) return;
        cooldown -= Time.deltaTime;
        if (cooldown <= 0 && !isGrowen)
        {
            isGrowen = true;
            anim.SetTrigger("grow");
        }
    }
    protected override GameObject SearchZombie()
    {
        foreach (GameObject item in GameAPP.board.GetComponent<Board>().zombieArray)
        {
            if (item != null)
            {
                Zombie component = item.GetComponent<Zombie>();
                if (component.theZombieRow == thePlantRow && component.shadow.transform.position.x < 9.2f && component.shadow.transform.position.x > shadow.transform.position.x && SearchUniqueZombie(component))
                {
                    return item;
                }
            }
        }
        return null;
    }
}
