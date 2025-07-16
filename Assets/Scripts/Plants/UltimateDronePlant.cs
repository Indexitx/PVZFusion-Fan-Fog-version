using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltimateDronePlant : Shooter
{
    public float timeToHide;
    public bool isHere;
    public GameObject laser;
    public LanternEffected e;
    public int lightLevel;
    public override void SpecialUpdate()
    {
        if (!isHere) return;
        timeToHide -= Time.deltaTime;
        if (timeToHide <= 0)
        {
            isHere = false;
            laser.SetActive(false);
        }
    }
    public override GameObject AnimShoot()
    {
        AttackZombie();
        laser.SetActive(true);
        isHere = true;
        timeToHide = 0.4f;
        return null;
    }
    private void AttackZombie()
    {
        float x = shadow.transform.position.x;
        bool flag = false;
        foreach (GameObject item in board.zombieArray)
        {
            if (item != null)
            {
                Zombie component = item.GetComponent<Zombie>();
                if (component.theZombieRow == thePlantRow && component.shadow.transform.position.x > x && AttackUniqueZombie(component))
                {
                    if (component.isFlying)
                    {
                        zombieList.Add(component);
                        int damage = 200;
                        component.TakeDamage(0, damage);
                        flag = true;
                    }
                }
            }
        }
        lightLevel = 0;
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
