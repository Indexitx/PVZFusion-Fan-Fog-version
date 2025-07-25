using UnityEngine;

public class IceDoomFume : Shooter
{
	public override GameObject AnimShoot()
	{
		Vector3 position = base.transform.Find("Shoot").transform.position;
		float theX = position.x + 0.1f;
		float y = position.y;
		int theRow = thePlantRow;
		GameObject obj = board.GetComponent<CreateBullet>().SetBullet(theX, y, theRow, 24, 0);
		obj.GetComponent<Bullet>().theBulletDamage = 20;
		GameAPP.PlaySound(Random.Range(3, 5));
		return obj;
	}

	protected override GameObject SearchZombie()
	{
		foreach (GameObject item in GameAPP.board.GetComponent<Board>().zombieArray)
		{
			if (item != null)
			{
				Zombie component = item.GetComponent<Zombie>();
				if (component.theZombieRow == thePlantRow && SearchUniqueZombie(component) && !component.isFlying)
				{
					return item;
				}
			}
		}
		return null;
	}
}
