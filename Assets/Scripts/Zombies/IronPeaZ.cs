using Unity.VisualScripting;
using UnityEngine;

public class IronPeaZ : PeaShooterZ
{
	public override GameObject AnimShoot()
	{
		if (theStatus == 1)
		{
			return null;
		}
		Vector3 position = base.transform.Find("Zombie_head").GetChild(0).transform.position;
		float theX = (isMindControlled ? (position.x + 0.4f) : (position.x - 0.4f));
		float theY = position.y - 0.1f;
		int theRow = theZombieRow;
		GameObject gameObject = board.GetComponent<CreateBullet>().SetBullet(theX, theY, theRow, 11, 0);
		Vector3 position2 = gameObject.transform.GetChild(0).transform.position;
		gameObject.transform.GetChild(0).transform.position = new Vector3(position2.x, position2.y - 0.5f, position2.z);
		if (!isMindControlled)
		{
			gameObject.GetComponent<Bullet>().isZombieBullet = true;
		}
		gameObject.GetComponent<Bullet>().theBulletDamage = 80;
		GameAPP.PlaySound(Random.Range(3, 5));
		return gameObject;
	}

	protected override void BodyTakeDamage(int theDamage)
	{
		theHealth -= theDamage;
		if (!isLoseHand && theHealth < (float)(theMaxHealth * 2 / 3))
		{
			isLoseHand = true;
			GameAPP.PlaySound(7);
			for (int i = 0; i < base.transform.childCount; i++)
			{
				Transform child = base.transform.GetChild(i);
				if (child.CompareTag("ZombieHand"))
				{
					Object.Destroy(child.gameObject);
				}
				if (child.CompareTag("ZombieArmUpper"))
				{
					child.GetComponent<SpriteRenderer>().sprite = GameAPP.spritePrefab[0];
					child.transform.localScale = new Vector3(4f, 4f, 4f);
				}
				if (child.name == "LoseArm")
				{
					child.gameObject.SetActive(value: true);
					child.gameObject.GetComponent<ParticleSystemRenderer>().sortingLayerName = $"zombie{theZombieRow}";
					child.gameObject.GetComponent<ParticleSystemRenderer>().sortingOrder += baseLayer + 29;
					child.gameObject.GetComponent<ParticleSystem>().collision.AddPlane(board.transform.GetChild(2 + theZombieRow));
					child.AddComponent<ZombieHead>();
				}
			}
		}
		if (!(theHealth < (float)(theMaxHealth / 3)) || theStatus == 1)
		{
			return;
		}
		theStatus = 1;
		for (int j = 0; j < base.transform.childCount; j++)
		{
			Transform child2 = base.transform.GetChild(j);
			if (child2.CompareTag("ZombieHead"))
			{
                CreateItem.Instance.SetItem(child2.position.x, child2.position.y, 0);
                Object.Destroy(child2.gameObject);
			}
		}
	}
}
