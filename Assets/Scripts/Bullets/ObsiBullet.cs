using UnityEngine;

public class ObsiBullet : Bullet
{
	protected override void HitZombie(GameObject zombie)
	{
        if (hitTimes == 1)
        {
            zombie.GetComponent<Zombie>().TakeDamage(0, theBulletDamage);
			zombie.GetComponent<Zombie>().Knockback(0.1f);
        }
        if (hitTimes == 2)
        {
            zombie.GetComponent<Zombie>().TakeDamage(0, Mathf.RoundToInt(theBulletDamage/2));
            zombie.GetComponent<Zombie>().Knockback(0.05f);
        }
        if (hitTimes == 3)
        {
            zombie.GetComponent<Zombie>().TakeDamage(0, Mathf.RoundToInt(theBulletDamage / 4));
        }
        Object.Instantiate(GameAPP.particlePrefab[41], base.transform.position, Quaternion.identity, Board.Instance.transform);
		GameAPP.PlaySound(70);
	}

	protected override void OnTriggerEnter2D(Collider2D collision)
	{
		if (hitTimes >= 3 || !collision.CompareTag("Zombie") || (collision.TryGetComponent<PolevaulterZombie>(out var component) && component.polevaulterStatus == 1))
		{
			return;
		}
		Zombie component2 = collision.GetComponent<Zombie>();
		if (component2.theZombieRow != theBulletRow || component2.isMindControlled || component2.theStatus == 7)
		{
			return;
		}
		foreach (GameObject item in Z)
		{
			if (item != null && item == zombie)
			{
				return;
			}
		}
		hitTimes++;
		Z.Add(collision.gameObject);
		HitZombie(collision.gameObject);
		if (hitTimes == 3)
		{
			Die();
		}
	}
}
