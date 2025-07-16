using UnityEngine;

public class NutBullet : Bullet
{
	protected override void HitZombie(GameObject zombie)
	{
		Zombie component = zombie.GetComponent<Zombie>();
		component.TakeDamage(0, theBulletDamage);
		GameObject gameObject = GameAPP.particlePrefab[7];
		GameObject obj = Object.Instantiate(gameObject, base.transform.position, Quaternion.identity);
		obj.transform.SetParent(GameAPP.board.transform);
		obj.name = gameObject.name;
		PlaySound(component);
	}

	protected override void OnTriggerEnter2D(Collider2D collision)
	{
		if (!collision.CompareTag("Zombie") || (collision.TryGetComponent<PolevaulterZombie>(out var component) && component.polevaulterStatus == 1))
		{
			return;
		}
		Zombie component2 = collision.GetComponent<Zombie>();
		if (component2.isMindControlled)
		{
			return;
		}
		int i = Random.Range(0, 100);
		if(transform.rotation == Quaternion.Euler(0, 0, 45))
		{
			i = 80;
		}
		else if(transform.rotation == Quaternion.Euler(0, 0, -45))
		{
            i = 1;
        }
		if (i <= 50)
		{
			transform.rotation = Quaternion.Euler(0, 0, 45);

		}
		else
		{
			transform.rotation = Quaternion.Euler(0, 0, -45);
		}
		hitTimes++;
		if(hitTimes >= 3)
		{
			Die();
		}
		Z.Add(collision.gameObject);
		HitZombie(collision.gameObject);
	}
}
