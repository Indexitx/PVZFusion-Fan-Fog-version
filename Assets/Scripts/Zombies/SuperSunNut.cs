using UnityEngine;

public class SuperSunNut : WallNut
{
	public float timing;
	public float timing2;
    public float timing3;
	public bool isReady;
	public bool isReady2;
    public bool isReady3;
	public override void TakeDamage(int damage, Zombie z = null)
	{
		
		if (isReady)
		{
            damage = 0;
            CreateCoin.Instance.SetCoin(thePlantColumn, thePlantRow, 0, 0);
            isReady = false;
            timing = 2.5f;
        }
        base.TakeDamage(damage, z);
    }

	public override void Click()
	{
		if (isReady2)
		{
            if (board.theSun > 500)
            {
                board.theSun -= 500;
                Recover(1500);
                GameObject gameObject = CreatePlant.Instance.SetPlant(thePlantColumn + 1, thePlantRow, 251);
                if (gameObject != null)
                {
                    Vector3 position = gameObject.GetComponent<Plant>().shadow.transform.position;
                    Object.Instantiate(GameAPP.particlePrefab[11], position + new Vector3(0f, 0.5f, 0f), Quaternion.identity, board.transform);
                }
				isReady2 = false;
				timing2 = 10f;
            }
        }
		
	}
	public override void SpecialUpdate()
	{
		timing -= Time.deltaTime;
        timing2 -= Time.deltaTime;
        timing3 -= Time.deltaTime;
        if (isReady3)
        {
            Recover(100);
            isReady3 = false;
            timing3 = 15f;
        }
        if (timing <= 0)
		{
			isReady = true;
		}
        if (timing2 <= 0)
        {
            isReady2 = true;
        }
        if(timing3 <= 0)
        {
            isReady3 = true;
        }
    }
}
