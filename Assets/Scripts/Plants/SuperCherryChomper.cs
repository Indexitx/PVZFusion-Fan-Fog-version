using UnityEngine;

public class SuperCherryChomper : SuperChomper
{
    public bool antiDeath = true;
    
    public void Start()
    {
        base.Start();
        maxTiming = 25f;
    }
	public override void Die(int reason = 0)
	{
        if (antiDeath && reason != 8888)
        {
            Recover(1000000);
            antiDeath = false;
            return;
        }
        if (status == 1)
        {
            Board.Instance.curSuperCount--;
        }
        if (status == 2)
        {
            Board.Instance.curKingCount--;
        }
        if (Mouse.Instance.thePlantOnGlove == base.gameObject)
        {
            Object.Destroy(Mouse.Instance.theItemOnMouse);
            Mouse.Instance.theItemOnMouse = null;
            Mouse.Instance.thePlantTypeOnMouse = -1;
            Mouse.Instance.thePlantOnGlove = null;
        }
        if (Board.Instance.isIZ && !Board.Instance.isEveStarted && reason == 0)
        {
            GiveSunInIZ();
        }
        
        TryRemoveFromList();
        Object.Destroy(base.gameObject);
    }
	public override void AnimShoot()
	{
		Vector3 position = base.transform.Find("Shoot").transform.position;
		float x = position.x;
		float y = position.y;
		int theRow = thePlantRow;
		board.GetComponent<CreateBullet>().SetBullet(x, y, theRow, 3, 0).GetComponent<Bullet>()
			.theBulletDamage = 300;
		GameAPP.PlaySound(Random.Range(3, 5));
	}

	protected override void Bite(GameObject _zombie)
	{
		Zombie component = _zombie.GetComponent<Zombie>();
        if (canEat && isBuffed1 && component.isEatablest)
        {

            component.Die(2);
            Recover(8000);
            canEat = false;
            timing = maxTiming;
            antiDeath = true;
        }
        else
		{
			component.TakeDamage(1, 1000);
			Recover(1000);
		}
		GameAPP.PlaySound(49);
		zombie = null;
	}
}
