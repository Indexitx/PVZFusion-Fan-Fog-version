using UnityEngine;

public class SunSeaShroom : Producer
{
	private int growLevel;

	protected override void Start()
	{
		base.Start();
		attributeCountdown = 25f;
    }

	protected override void Update()
	{
		base.Update();
		if (growLevel != 2 && GameAPP.theGameStatus == 0 && attributeCountdown <= 0f)
		{
			Grow();
		}
	}

	protected override void ProduceSun()
	{
		if (growLevel == 1)
		{
			base.ProduceSun();
			return;
		}
		if(growLevel == 2)
		{
            GameAPP.PlaySound(Random.Range(3, 5), 0.3f);
            board.GetComponent<CreateCoin>().SetCoin(thePlantColumn, thePlantRow, 1, 0);
            return;
        }
		GameAPP.PlaySound(Random.Range(3, 5), 0.3f);
		board.GetComponent<CreateCoin>().SetCoin(thePlantColumn, thePlantRow, 2, 0);
	}

	public void Grow()
	{
		growLevel++;
		attributeCountdown = 90f;
        anim.SetTrigger("grow");
		GameAPP.PlaySound(56);
	}
}
