using UnityEngine;

public class SuperChomper : Chomper
{
	private int shootType = 6;

	public float timing;

	public float maxTiming;

	public bool canEat;

    public bool isBuffed1;

    private GameObject face1;

	private GameObject face2;

	private GameObject face3;

	private GameObject body1;

	private GameObject body2;

	private GameObject body3;

	protected override void Start()
	{
		base.Start();
		face1 = base.transform.Find("Chomper_face_upper").gameObject;
		face2 = base.transform.Find("Chomper_face_upper1").gameObject;
		face3 = base.transform.Find("Chomper_face_upper2").gameObject;
		body1 = base.transform.Find("Chomper_body").gameObject;
		body2 = base.transform.Find("Chomper_body1").gameObject;
		body3 = base.transform.Find("Chomper_body2").gameObject;
		maxTiming = 35f;
		ReplaceSprite();
	}

	protected override void SetAttackRange()
	{
		pos = new Vector2(shadow.transform.position.x + 0.5f, shadow.transform.position.y + 0.5f);
		range = new Vector2(3f, 1.5f);
	}

	public override void BiteEvent()
	{
		if (zombie != null)
		{
			Zombie component = zombie.GetComponent<Zombie>();
			if (component.theAttackTarget == base.gameObject)
			{
				Bite(zombie);
				return;
			}
			Collider2D[] array = colliders;
			foreach (Collider2D collider2D in array)
			{
				if (!(collider2D == null) && collider2D.gameObject == zombie && !component.isMindControlled && !component.isFlying)
				{
					Bite(collider2D.gameObject);
					return;
				}
			}
			zombie = null;
		}
		GameAPP.PlaySound(49);
	}

	public virtual void AnimShoot()
	{
		Vector3 position = base.transform.Find("Shoot").transform.position;
		float x = position.x;
		float y = position.y;
		int theRow = thePlantRow;
		board.GetComponent<CreateBullet>().SetBullet(x, y, theRow, shootType, 1).GetComponent<Bullet>()
			.theBulletDamage = 70;
		GameAPP.PlaySound(Random.Range(3, 5));
	}

	public override void Recover(int health)
	{
		base.Recover(health);
		ReplaceSprite();
	}

	public override void TakeDamage(int damage, Zombie z = null)
	{
		base.TakeDamage(damage, z);
		ReplaceSprite();
	}

    public override void SpecialUpdate()
    {
		timing -= Time.deltaTime;
		if(timing <= 0)
		{
			canEat = true;
		}
    }

    protected void ReplaceSprite()
	{
		if (thePlantHealth > thePlantMaxHealth * 2 / 3)
		{
			body1.SetActive(value: true);
			face1.SetActive(value: true);
			body2.SetActive(value: false);
			face2.SetActive(value: false);
			body3.SetActive(value: false);
			face3.SetActive(value: false);
		}
		if (thePlantHealth > thePlantMaxHealth / 3 && thePlantHealth < thePlantMaxHealth * 2 / 3)
		{
			body1.SetActive(value: false);
			face1.SetActive(value: false);
			body2.SetActive(value: true);
			face2.SetActive(value: true);
			body3.SetActive(value: false);
			face3.SetActive(value: false);
		}
		if (thePlantHealth < thePlantMaxHealth / 3)
		{
			body1.SetActive(value: false);
			face1.SetActive(value: false);
			body2.SetActive(value: false);
			face2.SetActive(value: false);
			body3.SetActive(value: true);
			face3.SetActive(value: true);
		}
	}

	protected virtual void Bite(GameObject _zombie)
	{
		Zombie component = _zombie.GetComponent<Zombie>();
		if (canEat && component.isEatablest && isBuffed1)
		{
			component.Die(2);
			Recover(4000);
			canEat = false;
			timing = maxTiming;
			shootType = 6;
		}
		else
		{
			component.TakeDamage(1, 100);
			Recover(100);
			shootType = Random.Range(4, 6);
		}
		GameAPP.PlaySound(49);
		zombie = null;
	}
}
