using UnityEngine;

public class UltimateThreepeater : Shooter
{
	public override void Die(int dieReason)
	{
		Board.Instance.CreateFireLine(0);
        Board.Instance.CreateFireLine(1);
        Board.Instance.CreateFireLine(2);
        Board.Instance.CreateFireLine(3);
        Board.Instance.CreateFireLine(4);
		if(Board.Instance.roadNum > 5)
		{
            Board.Instance.CreateFireLine(5);
        }
        
		base.Die(dieReason);
    }
	public override GameObject AnimShoot()
	{
		Vector3 position = base.transform.Find("headPos2").Find("Shoot").transform.position;
		float num = position.x + 0.1f;
		float y = position.y;
		int num2 = thePlantRow;
		GameObject obj = CreateBullet.Instance.SetBullet(num, y, num2, 53, 0);
		obj.GetComponent<Bullet>().theBulletDamage = 80;
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
		CreateBullet.Instance.SetBullet(X, Y, row, 53, 4).GetComponent<Bullet>().theBulletDamage = 160;
	}

	private void ShootLower(float X, float Y, int row)
	{
		CreateBullet.Instance.SetBullet(X, Y, row, 53, 5).GetComponent<Bullet>().theBulletDamage = 160;
	}

	private void ExtraBullet()
	{
		Vector3 position = base.transform.Find("headPos2").Find("Shoot").transform.position;
		float theX = position.x + 0.1f;
		float y = position.y;
		int theRow = thePlantRow;
		board.GetComponent<CreateBullet>().SetBullet(theX, y, theRow, 53, 0).GetComponent<Bullet>()
			.theBulletDamage = 160;
	}

	protected override GameObject SearchZombie()
	{
		foreach (GameObject item in GameAPP.board.GetComponent<Board>().zombieArray)
		{
			if (item != null)
			{
				Zombie component = item.GetComponent<Zombie>();
				if (Mathf.Abs(component.theZombieRow - thePlantRow) <= 1 && component.shadow.transform.position.x < 9.2f && component.shadow.transform.position.x > shadow.transform.position.x && SearchUniqueZombie(component) && !component.isFlying)
				{
					return item;
				}
			}
		}
		return null;
	}
}
