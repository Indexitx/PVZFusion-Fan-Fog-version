using UnityEngine;

public class DoomGatlingPea : Shooter
{
    public int doomTimes;
    protected override void Awake()
    {
        base.Awake();
    }
    public override GameObject AnimShoot()
	{

		Vector3 position = base.transform.Find("GatlingPea_head").GetChild(0).position;
		float theX = position.x + 0.1f;
		float y = position.y;

        GameObject obj = null;


        int theRow = thePlantRow;
		if(doomTimes != 15)
		{
            obj = board.GetComponent<CreateBullet>().SetBullet(theX, y, theRow, 23, 0);
            obj.GetComponent<Bullet>().theBulletDamage = 300;
            GameAPP.PlaySound(Random.Range(3, 5));
            doomTimes++;
        }
		else
		{
            obj = board.GetComponent<CreateBullet>().SetBullet(theX, y, theRow, 37, 0);
            obj.GetComponent<Bullet>().theBulletDamage = 0;
			doomTimes = 0;
        }
        
        return obj;
	}
}
