using UnityEngine;

public class UltimateDoom : DoomShroom
{
	protected override void Awake()
	{
		base.Awake();
    }
    protected override void Start()
    {
        base.Start();
        anim.Play("explode");
        GameAPP.PlaySound(39);
    }
    public override void AnimExplode()
	{
		Vector2 vector = new Vector2(shadow.transform.position.x - 0.3f, shadow.transform.position.y + 0.3f);
		Object.Instantiate(GameAPP.particlePrefab[25], vector, Quaternion.identity, board.transform).GetComponent<Doom>().theDoomType = 5;
		ScreenShake.TriggerShake();
		GameAPP.PlaySound(41);
		if (board.boxType[thePlantColumn, thePlantRow] != 1)
		{
			if (board.isNight)
			{
				GridItem.CreateGridItem(thePlantColumn, thePlantRow, 1);
			}
			else
			{
				GridItem.CreateGridItem(thePlantColumn, thePlantRow, 0);
			}
		}
		Die();
	}
}
