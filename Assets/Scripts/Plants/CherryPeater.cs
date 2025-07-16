using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CherryPeater : ThreePeater
{
    public bool isBuffed2;
    protected override void Awake()
    {
        base.Awake();
    }
    public override GameObject AnimShoot()
    {
        Vector3 position = base.transform.Find("headPos2").Find("Shoot").transform.position;
        float num = position.x + 0.1f;
        float y = position.y;
        int num2 = thePlantRow;
        GameObject obj = CreateBullet.Instance.SetBullet(num, y, num2, 1, 0);
        obj.GetComponent<Bullet>().theBulletDamage = 60;
        if (isBuffed2)
        {
            obj.GetComponent<Bullet>().theBulletDamage = 90;
        }
        GameAPP.PlaySound(Random.Range(3, 5));
        if (thePlantRow == 0)
        {
            ShootLower(num, y, num2 + 1);
            Invoke("ExtraBullet", 0.2f);
            return obj;
        }
        if (thePlantRow == board.roadNum - 1)
        {
            ShootUpper(num, y, num2 - 1);
            Invoke("ExtraBullet", 0.2f);
            return obj;
        }
        ShootLower(num, y, num2 + 1);
        ShootUpper(num, y, num2 - 1);
        return obj;
    }

    private void ShootUpper(float X, float Y, int row)
    {
        GameObject obj = CreateBullet.Instance.SetBullet(X, Y, row, 1, 4);
        obj.GetComponent<Bullet>().theBulletDamage = 60;
        if (isBuffed2)
        {
            obj.GetComponent<Bullet>().theBulletDamage = 90;
        }
    }

    private void ShootLower(float X, float Y, int row)
    {
        GameObject obj = CreateBullet.Instance.SetBullet(X, Y, row, 1, 5);
        obj.GetComponent<Bullet>().theBulletDamage = 60;
        if (isBuffed2)
        {
            obj.GetComponent<Bullet>().theBulletDamage = 90;
        }
    }

    private void ExtraBullet()
    {
        Vector3 position = base.transform.Find("headPos2").Find("Shoot").transform.position;
        float theX = position.x + 0.1f;
        float y = position.y;
        int theRow = thePlantRow;
        GameObject obj = CreateBullet.Instance.SetBullet(theX, y, theRow, 1, 0);
        obj.GetComponent<Bullet>().theBulletDamage = 60;
        if (isBuffed2)
        {
            obj.GetComponent<Bullet>().theBulletDamage = 90;
        }
    }
}
