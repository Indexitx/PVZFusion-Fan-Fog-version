using UnityEngine;

public class JackboxStar : Bullet
{
	protected override void HitZombie(GameObject zombie)
	{
		Zombie component = zombie.GetComponent<Zombie>();
		GameAPP.PlaySound(40);
		SetFume();
		Die();
	}

	private void SetFume()
	{
		Object.Instantiate(GameAPP.particlePrefab[43], base.transform.position, Quaternion.identity, GameAPP.board.transform).GetComponent<ParticleSystem>().GetComponent<Renderer>()
			.sortingLayerName = $"particle{theBulletRow}";
		AttackZombie();
	}

	private void AttackZombie()
	{
		Collider2D[] array = Physics2D.OverlapCircleAll(base.transform.position, 1.5f);
		foreach (Collider2D collider2D in array)
		{
			if (collider2D != null && collider2D.TryGetComponent<Zombie>(out var component) && (!component.gameObject.TryGetComponent<PolevaulterZombie>(out var component2) || component2.polevaulterStatus != 1) && !component.isMindControlled)
			{
                component.TakeDamage(0, theBulletDamage);
            }
		}
	}
}
