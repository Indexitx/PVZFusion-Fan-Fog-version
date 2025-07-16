using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetCactus : Cactus
{
    public override GameObject AnimShoot()
    {
        Vector3 position = base.transform.Find("Shoot").transform.position;
        float theX = position.x + 0.1f;
        float y = position.y;
        int theRow = thePlantRow;
        CheckMagnetSystem();
        GameObject obj = board.GetComponent<CreateBullet>().SetBullet(theX, y, theRow, 43, 0);
        obj.GetComponent<Bullet>().theBulletDamage = Mathf.RoundToInt(20 * damageModifier);
        GameAPP.PlaySound(Random.Range(3, 5));
        return obj;
    }
    public override void AnimShoot2()
    {
        Vector3 position = base.transform.Find("Shoot2").transform.position;
        float theX = position.x + 0.1f;
        float y = position.y;
        int theRow = thePlantRow;
        CheckMagnetSystem();
        GameObject obj = board.GetComponent<CreateBullet>().SetBullet(theX, y, theRow, 43, 0);
        obj.GetComponent<Bullet>().theBulletDamage = Mathf.RoundToInt(20 * damageModifier);
        obj.GetComponent<Bullet>().isAttackFlying = true;
        obj.GetComponent<Bullet>().isAttackGround = false;
        GameAPP.PlaySound(Random.Range(3, 5));
    }
}
