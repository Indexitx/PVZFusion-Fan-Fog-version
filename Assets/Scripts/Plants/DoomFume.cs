using UnityEngine;

public class DoomFume : Plant
{
	public bool isBuffed1;
	protected override void Awake()
	{
		base.Awake();
	}
	protected override void Update()
	{
		base.Update();
		if (thePlantAttackCountDown > 0f)
		{
			thePlantAttackCountDown -= Time.deltaTime;
			if (thePlantAttackCountDown <= 0f)
			{
				thePlantAttackCountDown = 0f;
				anim.SetTrigger("backToIdle");
			}
		}
	}

	public override void Click()
	{
		if (thePlantAttackCountDown == 0f)
		{
			anim.SetTrigger("shoot");
			thePlantAttackCountDown = 60f;
		}
	}

	private void AnimShoot()
	{
		Vector3 position = base.transform.Find("Shoot").transform.position;
		Object.Instantiate(GameAPP.particlePrefab[31], position, Quaternion.Euler(0f, 90f, 0f), board.transform).GetComponent<ParticleSystem>().GetComponent<Renderer>()
			.sortingLayerName = $"particle{thePlantRow}";
		GameAPP.PlaySound(58);
		AttackZombie();
	}

	private void AttackZombie()
	{
		bool flag = false;
		foreach (GameObject item in board.zombieArray)
		{
			if (item != null)
			{
				Zombie component = item.GetComponent<Zombie>();
				if (!(component.shadow.transform.position.x < shadow.transform.position.x) && SearchUniqueZombie(component) && component.theZombieRow == thePlantRow && !component.isFlying)
				{
					zombieList.Add(component);
					flag = true;
				}
			}
		}
		for (int num = zombieList.Count - 1; num >= 0; num--)
		{
			if (zombieList[num] != null)
			{
				zombieList[num].TakeDamage(1, 1800);

            }
		}
		zombieList.Clear();
		if (flag)
		{
			GameAPP.PlaySound(Random.Range(0, 3));
		}
	}
}
