using UnityEngine;

public class SuperKelp : Plant
{
	private bool isReady = true;
	private float timing;
    private Collider2D[] colliders;
    protected Zombie TargetZombie;
    private Vector2 range = new Vector2(2f, 2f);
    protected override void Update()
	{
		base.Update();
		PlantShootUpdate();
	}

	private void AnimShoot()
	{
		Vector3 position = base.transform.Find("Shoot").position;
		GameObject gameObject = CreateBullet.Instance.SetBullet(position.x, position.y, thePlantRow + 1, 32, 5);
		GameObject gameObject2 = CreateBullet.Instance.SetBullet(position.x, position.y, thePlantRow, 32, 0);
		GameObject obj = CreateBullet.Instance.SetBullet(position.x, position.y, thePlantRow - 1, 32, 4);
		gameObject.GetComponent<Bullet>().theBulletDamage = 40;
		gameObject2.GetComponent<Bullet>().theBulletDamage = 40;
		obj.GetComponent<Bullet>().theBulletDamage = 40;
		GameAPP.PlaySound(Random.Range(3, 5));
	}
    private void Grab(Zombie z)
	{
		if (isReady)
		{
            z.TakeDamage(1, 1000000);
            anim.SetTrigger("grab");
            isReady = false;
			timing = 25f;
			z.Knockback(0.3f);
        }
		
	}
	public override void SpecialUpdate()
	{
		timing -= Time.deltaTime;
        colliders = Physics2D.OverlapBoxAll(shadow.transform.position, range, 0f);
        Collider2D[] array = colliders;
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i].TryGetComponent<Zombie>(out var component) && component.theStatus != 1 && !component.isMindControlled && component.theZombieRow == thePlantRow && TargetZombie == null)
            {
				Grab(component);
                GameAPP.PlaySound(71);
            }
        }

        if (timing <= 0)
		{
			isReady = true;
		}
	}
	protected override GameObject SearchZombie()
	{
		foreach (GameObject item in GameAPP.board.GetComponent<Board>().zombieArray)
		{
			if (item != null)
			{
				Zombie component = item.GetComponent<Zombie>();
				if (Mathf.Abs(component.theZombieRow - thePlantRow) <= 1 && component.shadow.transform.position.x < 9.2f && component.shadow.transform.position.x > shadow.transform.position.x && SearchUniqueZombie(component) && !component.isFlying)
				{
					return item;
				}
			}
		}
		return null;
	}
}
