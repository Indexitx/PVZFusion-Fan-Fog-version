using UnityEngine;

public class CherrySplitPea : Shooter
{
    protected override void Awake()
    {
        base.Awake();
    }
    public override GameObject AnimShoot()
	{
		Vector3 position = base.transform.Find("Shoot").transform.position;
        Vector3 position2 = base.transform.Find("PeaShooter_Head").GetChild(0).transform.position;
        float theX = position.x + 0.1f;
		float y = position.y;
        float theX2 = position2.x + 0.1f;
        float y2 = position2.y;
        int theRow = thePlantRow;
		GameObject obj = board.GetComponent<CreateBullet>().SetBullet(theX, y, theRow, 1, 0);
        GameObject obj2 = board.GetComponent<CreateBullet>().SetBullet(theX2, y2, theRow, 1, 10);
        obj.GetComponent<Bullet>().theBulletDamage = 60;
        obj2.GetComponent<Bullet>().theBulletDamage = 60;
        GameAPP.PlaySound(Random.Range(3, 5));
		return obj;
	}
    public GameObject AnimShoot2()
    {
        Vector3 position = base.transform.Find("PeaShooter_Head").GetChild(0).transform.position;
        float theX = position.x + 0.1f;
        float y = position.y;
        int theRow = thePlantRow;
        GameObject obj = board.GetComponent<CreateBullet>().SetBullet(theX, y, theRow, 1, 10);
        obj.GetComponent<Bullet>().theBulletDamage = 20;
        GameAPP.PlaySound(Random.Range(3, 5));
        return obj;
    }
}
