using UnityEngine;

public class ZombieBomb : MonoBehaviour
{

	public int bombType;

	public int bombRow;

	private Plant thePlant;

	private Board board;

	public bool isEnemy;


	private void Start()
	{
		board = GameAPP.board.GetComponent<Board>();
        
        Vector3 position = base.transform.position;
		base.transform.position = new Vector3(position.x, position.y, 1f);
		if (bombType == 1)
		{
			Explode(position, 2f);
		}
		else if (bombType == 2)
		{
			Explode(position, 0.2f);
		}
	}

	private void Explode(Vector2 explosionPosition, float explosionRadius)
	{
        GameAPP.PlaySound(40);
        Collider2D[] array = Physics2D.OverlapCircleAll(explosionPosition, explosionRadius);
		foreach (Collider2D collider2D in array)
		{
			if (collider2D.CompareTag("Plant"))
			{
				if (isEnemy)
				{
                    Plant component = collider2D.GetComponent<Plant>();
                    if (component.isDrone) continue;
                    if (component.thePlantType == 1105) continue;
                    if (Mathf.Abs(component.thePlantRow - bombRow) <= 1)
                    {
                        if(component.explodeImmunityLevel == 0)
						{
							if(bombType == 1)
							{
								component.TakeDamage(1000000);
							}
							else
							{
                                component.TakeDamage(1000);
                            }
						}
                        if (component.explodeImmunityLevel == 1)
                        {
                            if (bombType == 1)
                            {
                                component.TakeDamage(1000000);
                            }
                            else
                            {
                                component.TakeDamage(500);
                            }
                        }
                        if (component.explodeImmunityLevel == 2)
                        {
                            if (bombType == 1)
                            {
                                component.TakeDamage(1000);
                            }
                            else
                            {
                                component.TakeDamage(0);
                            }
                        }
                        if (component.explodeImmunityLevel == 3)
                        {
                            if (bombType == 1)
                            {
                                component.TakeDamage(0);
                            }
                            else
                            {
                                component.TakeDamage(0);
                            }
                        }
                    }
                }
				
			}
			else
			{
				if (!isEnemy)
				{
                    if (!collider2D.CompareTag("Zombie"))
                    {
                        continue;
                    }
                    Zombie component2 = collider2D.GetComponent<Zombie>();
                    if (component2.isMindControlled)
                    {
                        continue;
                    }
                    else
                    {
                        if (Mathf.Abs(component2.theZombieRow - bombRow) > 1)
                        {
                            continue;
                        }
                        if (bombType == 2)
                        {
                            if (component2.theStatus != 7)
                            {
                                component2.TakeDamage(10, 1800);
                            }
                            continue;
                        }
                        if (component2.theHealth > 1800f)
                        {
                            component2.TakeDamage(10, 1800);
                            continue;
                        }
                        if (bombType == 2)
                        {
                            GameAPP.board.GetComponent<CreateCoin>().SetCoin(0, 0, 0, 0, base.transform.position);
                        }
                        component2.Charred();
                    }
                }
				
			}
		}
	}

	private void PlantTakeDamage(Plant plant)
	{
		int num = 1000;
		if (board.isEveStarted)
		{
			num = 80;
		}
		if (TypeMgr.IsCaltrop(plant.thePlantType))
		{
			return;
		}
		if (GameAPP.difficulty < 5)
		{
			if (plant.cherryImmunityLevel == 3)
			{
				plant.Recover(200);
			}
			else if(plant.cherryImmunityLevel == 2)
			{
				plant.TakeDamage(0);
			}
			else if (plant.cherryImmunityLevel == 1)
			{
				plant.TakeDamage(num / 2);
			}
			else
			{
				plant.TakeDamage(num);
			}
			plant.FlashOnce();
		}
		else
		{
            if (plant.cherryImmunityLevel == 3)
            {
                plant.Recover(200);
            }
            else if (plant.cherryImmunityLevel == 2)
            {
                plant.TakeDamage(0);
            }
            else if (plant.cherryImmunityLevel == 1)
            {
                plant.TakeDamage(num / 2);
            }
            else
            {
                plant.TakeDamage(num);
            }
            plant.FlashOnce();
		}
	}
}
