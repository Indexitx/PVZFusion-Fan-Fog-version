using UnityEngine;

public class AcidThrower : Shooter
{
	public override GameObject AnimShoot()
	{
		Vector3 position = base.transform.Find("body").position;
		float theX = position.x + 0.1f;
		float y = position.y + 0.5f;
		int theRow = thePlantRow;
		GameObject obj = board.GetComponent<CreateBullet>().SetBullet(theX, y, theRow, 50, 0);
		obj.GetComponent<Bullet>().theBulletDamage = 50;
		GameAPP.PlaySound(Random.Range(3, 5));
		return obj;
	}
}
