using UnityEngine;
using System.Collections.Generic;

public class ObsidianTallnut : Plant
{
	public float timing;
	public List<ObsidianTallnut> tallnutList;
	public int count;
	protected override void Start()
	{
		base.Start();
        tallnutList = new List<ObsidianTallnut>();
        ReplaceSprite();
	}

	public override void Recover(int health)
	{
		base.Recover(health);
		ReplaceSprite();
	}
	public override void SpecialUpdate()
	{
		timing -= Time.deltaTime;
		if( timing <= 0)
		{
			Recover(1500);
			timing = 15;
		}
	}
	public override void TakeDamage(int damage, Zombie z = null)
	{
        base.TakeDamage(damage, z);
        ReplaceSprite();
	}
	public void TD(int d)
	{
		base.TakeDamage(d);
	}
	protected virtual void ReplaceSprite()
	{
		if (thePlantHealth > thePlantMaxHealth * 2 / 3)
		{
			base.transform.GetChild(0).gameObject.SetActive(value: true);
			base.transform.GetChild(1).gameObject.SetActive(value: false);
			base.transform.GetChild(2).gameObject.SetActive(value: false);
		}
		if (thePlantHealth > thePlantMaxHealth / 3 && thePlantHealth < thePlantMaxHealth * 2 / 3)
		{
			base.transform.GetChild(0).gameObject.SetActive(value: false);
			base.transform.GetChild(1).gameObject.SetActive(value: true);
			base.transform.GetChild(2).gameObject.SetActive(value: false);
		}
		if (thePlantHealth < thePlantMaxHealth / 3)
		{
			base.transform.GetChild(0).gameObject.SetActive(value: false);
			base.transform.GetChild(1).gameObject.SetActive(value: false);
			base.transform.GetChild(2).gameObject.SetActive(value: true);
		}
	}

	protected virtual void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Zombie"))
		{
			Zombie component = collision.GetComponent<Zombie>();
			if (component.theZombieRow == thePlantRow && component.theAttackTarget == base.gameObject && collision.gameObject.GetComponent<Zombie>().theStatus != 1)
			{
				thePlantSpeed = 0f;
			}
		}
	}

	protected override void FixedUpdate()
	{
		base.FixedUpdate();
		thePlantSpeed = theConstSpeed;
	}
}
