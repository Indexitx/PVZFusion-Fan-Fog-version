using System.Collections.Generic;
using UnityEngine;

public class BigWallNut : Plant
{

	protected override void Start()
	{
		GameAPP.PlaySound(53);
		anim.Play("Round");
	}

	protected override void FixedUpdate()
	{
	}

	protected override void Update()
	{
		if (Camera.main.WorldToViewportPoint(base.transform.position).x > 1f)
		{
			Object.Destroy(base.gameObject);
		}
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		if (!collision.TryGetComponent<Zombie>(out var component) || component.theZombieRow != thePlantRow || component.isMindControlled)
		{
			return;
		}
		component.TakeDamage(10, 100);
		GameAPP.PlaySound(Random.Range(54, 56));
		ScreenShake.TriggerShake(0.02f);
	}

	public override void Crashed()
	{
	}
}
