using UnityEngine;

public class BigDoom : Bullet
{
	protected override void HitZombie(GameObject zombie)
	{
		
		GameObject gameObject = GameAPP.particlePrefab[40];
		Vector3 position = new Vector3(base.transform.position.x, base.transform.position.y, 0f);
		GameObject gameObject2 = Object.Instantiate(gameObject, position, Quaternion.identity);
		gameObject2.transform.SetParent(GameAPP.board.transform);
		gameObject2.name = gameObject.name;
		gameObject2.GetComponent<Doom>().SetDoomType(2);
		GameAPP.PlaySound(40, 0.2f);
		Die();
	}

	protected override void HitPlant(GameObject plant)
	{
		GameObject gameObject = GameAPP.particlePrefab[14];
		Vector3 position = new Vector3(base.transform.position.x - 0.5f, base.transform.position.y - 0.2f, 0f);
		GameObject obj = Object.Instantiate(gameObject, position, Quaternion.identity);
		obj.transform.SetParent(GameAPP.board.transform);
		obj.name = gameObject.name;
		obj.GetComponent<BombCherry>().bombRow = theBulletRow;
		obj.GetComponent<BombCherry>().isFromZombie = true;
		obj.GetComponent<BombCherry>().targetPlant = plant;
		GameAPP.PlaySound(40, 0.2f);
		Die();
	}
}
