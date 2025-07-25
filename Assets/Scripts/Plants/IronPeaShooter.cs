using UnityEngine;

public class IronPeaShooter : PeaShooter
{
    protected override void Awake()
    {
        base.Awake();
    }
    public override GameObject AnimShoot()
	{
		Vector3 position = base.transform.Find("Shoot").transform.position;
		float theX = position.x + 0.1f;
		float y = position.y;
		int theRow = thePlantRow;
		GameObject obj = board.GetComponent<CreateBullet>().SetBullet(theX, y, theRow, 11, 0);
		
		obj.GetComponent<Bullet>().theBulletDamage = 80;
		GameAPP.PlaySound(Random.Range(3, 5));
		return obj;
	}
}
