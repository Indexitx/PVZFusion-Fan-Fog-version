using UnityEngine;

public class Caltrop : Plant
{
	protected override void Update()
	{
		base.Update();
		if (thePlantAttackCountDown > 0f)
		{
			thePlantAttackCountDown -= Time.deltaTime;
			if (thePlantAttackCountDown <= 0f)
			{
				ReadyToAttack();
				thePlantAttackCountDown = thePlantAttackInterval + Random.Range(-0.1f, 0.1f);
			}
		}
	}

	protected virtual void ReadyToAttack()
	{
		Collider2D[] array = Physics2D.OverlapBoxAll(shadow.transform.position, new Vector2(1f, 1f), 0f);
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i].TryGetComponent<Zombie>(out var component) && SearchUniqueZombie(component) && component.theZombieRow == thePlantRow)
			{
				anim.SetTrigger("attack");
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.TryGetComponent<Zombie>(out var component) && component.theStatus != 1 && component.theZombieRow == thePlantRow && !component.isMindControlled)
		{
			int theZombieType = component.theZombieType;
			if (theZombieType == 16 || theZombieType == 18 || theZombieType == 201)
			{
				anim.SetTrigger("attack");
			}
		}
	}

	protected virtual void KillCar()
	{
		Collider2D[] array = Physics2D.OverlapBoxAll(shadow.transform.position, new Vector2(1f, 1f), 0f);
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i].TryGetComponent<DriverZombie>(out var component) && component.theZombieRow == thePlantRow && !component.isMindControlled && component.theStatus != 1)
			{
				component.KillByCaltrop();
				GameAPP.PlaySound(77);
				Die();
			}
		}
	}

	protected virtual void AnimAttack()
	{
		KillCar();
		Collider2D[] array = Physics2D.OverlapBoxAll(shadow.transform.position, new Vector2(1f, 1f), 0f);
		bool flag = false;
		Collider2D[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			if (array2[i].TryGetComponent<Zombie>(out var component) && component.theZombieRow == thePlantRow && SearchUniqueZombie(component))
			{
				flag = true;
				component.TakeDamage(4, 10);
				int o = Random.Range(0, 100);
				if(o <= 5)
				{
					SummonCaltrop();
				}
			}
		}
		if (flag)
		{
			GameAPP.PlaySound(Random.Range(0, 3));
		}
	}
    protected void SummonCaltrop()
    {
        int num = 1;
        GameObject gameObject;
        do
        {
            gameObject = CreatePlant.Instance.SetPlant(thePlantColumn + num, thePlantRow, thePlantType);
            if (thePlantColumn + num > 9)
            {
                break;
            }
            num++;
        }
        while (gameObject == null);
        if (gameObject != null)
        {
            Vector2 vector = gameObject.GetComponent<Plant>().shadow.transform.position;
            Object.Instantiate(position: new Vector2(vector.x, vector.y + 0.5f), original: GameAPP.particlePrefab[11], rotation: Quaternion.identity, parent: board.transform);
        }
    }
}
