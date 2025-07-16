using UnityEngine;

public class DoubleDoom : Plant
{
	public int stady;
	public float timeToExplode;
    public float timering;
    protected override void Start()
	{
		base.Start();
        timering = 12f;
		timeToExplode = 3f;
	}
	public override void Click()
	{
		if(timeToExplode <= 0)
		{
			if(stady == 0)
			{
				SmallExplode();
            }
			else if(stady == 1)
			{
				MediumExplode();
			}
			else if(stady == 2)
			{
				Explode();
			}
		}
	}
	public void SmallExplode()
	{
        Object.Instantiate(GameAPP.particlePrefab[27], shadow.transform.position, Quaternion.identity, GameAPP.board.transform);
        Collider2D[] array = Physics2D.OverlapCircleAll(shadow.transform.position, 2);
        foreach (Collider2D col in array)
		{
			if (col == null) continue;
			if(col.TryGetComponent<Zombie>(out var z) && Mathf.Abs(z.theZombieRow - thePlantRow) <= 1)
			{
				z.TakeDamage(10, 225);
			}
			
		}
        GameAPP.PlaySound(41);
		Reset();
    }
    public void MediumExplode()
    {
        Object.Instantiate(GameAPP.particlePrefab[50], shadow.transform.position, Quaternion.identity, GameAPP.board.transform);
        Collider2D[] array = Physics2D.OverlapCircleAll(shadow.transform.position, 3.5f);
        foreach (Collider2D col in array)
        {
            if (col == null) continue;
            if (col.TryGetComponent<Zombie>(out var z) && Mathf.Abs(z.theZombieRow - thePlantRow) <= 1)
            {
                z.TakeDamage(10, 1200);
            }
        }
        GameAPP.PlaySound(41);
		Reset();
    }
	public void Reset()
	{
        anim.SetTrigger("recover");
		stady = 0;
		timering = 12f;
		timeToExplode = 3f;
    }
    public virtual void Explode()
	{
		Vector2 position = new Vector2(shadow.transform.position.x - 0.3f, shadow.transform.position.y + 0.3f);
		board.SetDoom(thePlantColumn, thePlantRow, setPit: false, position);
        Reset();
    }

	protected override void Update()
	{
		base.Update();
		timeToExplode -= Time.deltaTime;
		if(stady != 2)
		{
            timering -= Time.deltaTime;
            if (timering <= 0)
            {
                stady++;
				if(stady == 1)
				{
                    timering = 30;
                }
                
                anim.SetTrigger("grow");
            }
        }
		
	}
	public override void Die(int dieReason)
	{
		Explode();
		base.Die(dieReason);
	}
}
