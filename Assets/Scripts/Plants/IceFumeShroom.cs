using UnityEngine;

public class IceFumeShroom : FumeShroom
{
	protected override void AttackZombie()
	{
		bool flag = false;
		foreach (GameObject item in board.zombieArray)
		{
			if (item != null)
			{
				Zombie component = item.GetComponent<Zombie>();
				if (!(component.shadow.transform.position.x > shadow.transform.position.x + 7f) && !(component.shadow.transform.position.x < shadow.transform.position.x) && SearchUniqueZombie(component) && component.theZombieRow == thePlantRow)
				{
					zombieList.Add(component);
					flag = true;
				}
			}
		}
        bool b = true;
        for (int num = zombieList.Count - 1; num >= 0; num--)
		{
			if (zombieList[num] != null)
			{
                int i = Random.Range(0, 100);
				
                if (i <= 35 && isBuffed1 && b)
                {
                    zombieList[num].Knockback(0.2f);
					b = false;
                }
                zombieList[num].TakeDamage(3, 20);
			}
		}
		zombieList.Clear();
		if (flag)
		{
			GameAPP.PlaySound(Random.Range(0, 3));
		}
	}

	public override GameObject AnimShoot()
	{
		Vector3 position = base.transform.Find("Shoot").transform.position;
		Object.Instantiate(GameAPP.particlePrefab[30], position, Quaternion.Euler(0f, 90f, 0f), board.transform).GetComponent<ParticleSystem>().GetComponent<Renderer>()
			.sortingLayerName = $"particle{thePlantRow}";
		GameAPP.PlaySound(58);
		AttackZombie();
		return null;
	}
}
