using System.Collections.Generic;
using UnityEngine;

public class Doom : MonoBehaviour
{
	private Vector2 pos = new Vector2(0f, 0f);

	private const float range = 5f;

	public int theDoomType;

	private Board board;

    private void Start()
	{
        board = GameAPP.board.GetComponent<Board>();
		pos = base.transform.position;
		Explode();
        
    }
	public void SetDoomType(int d)
	{
		theDoomType = d;
	}
	private void Explode()
	{
		if (theDoomType == 0)
		{
			NormalDoom();
		}
        
        if (theDoomType == 1)
		{
			IceDoom();
		}
        if (theDoomType == 2)
        {
			SmallDoom();
        }
        if (theDoomType == 3)
        {
            StarBomb();
        }
        if (theDoomType == 5)
        {
            UltimateDoom();
        }
    }

	private void IceDoom()
	{
		List<Zombie> list = new List<Zombie>();
		foreach (GameObject item in board.zombieArray)
		{
			if (item != null)
			{
				Zombie component = item.GetComponent<Zombie>();
				if (!component.isMindControlled)
				{
					list.Add(component);
				}
			}
		}
		foreach (Zombie item2 in list)
		{
            item2.SetCold(10f);
            if (item2.theHealth > 1800f)
			{
				item2.TakeDamage(3, 1800);
				continue;
			}
			
			item2.Charred();
		}
	}

	private void NormalDoom()
	{
		Collider2D[] array = Physics2D.OverlapCircleAll(pos, 5f);
		foreach (Collider2D collider2D in array)
		{
			if (collider2D != null && collider2D.TryGetComponent<Zombie>(out var component) && !component.isMindControlled)
			{
				if (component.theHealth > 1800f)
				{
					component.TakeDamage(10, 1800);
                }
				else
				{
                    Debug.Log("i");
					component.Charred();
				}
			}
		}
	}
    private void UltimateDoom()
    {
        Collider2D[] array = Physics2D.OverlapCircleAll(pos, 5f);
        foreach (Collider2D collider2D in array)
        {
            if (collider2D != null && collider2D.TryGetComponent<Zombie>(out var component) && !component.isMindControlled)
            {
				component.isDoom = true;
				component.SetMindControl(true);
            }
        }
    }
    private void StarBomb()
    {
        Collider2D[] array = Physics2D.OverlapCircleAll(pos, 5f);
        foreach (Collider2D collider2D in array)
        {
            if (collider2D != null && collider2D.TryGetComponent<Zombie>(out var component) && !component.isMindControlled)
            {
				component.TakeDamage(0, 1800);
            }
        }
    }
    private void SmallDoom()
    {
        Collider2D[] array = Physics2D.OverlapCircleAll(pos, 5f);
        foreach (Collider2D collider2D in array)
        {
            if (collider2D != null && collider2D.TryGetComponent<Zombie>(out var component) && !component.isMindControlled)
            {
                component.TakeDamage(0, 1800);
            }
        }
    }

    private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Collider2D[] array = Physics2D.OverlapCircleAll(pos, 5f, 0);
		foreach (Collider2D obj in array)
		{
			Gizmos.color = Color.green;
			Gizmos.DrawSphere(obj.bounds.center, 0.1f);
		}
	}

	private void Die()
	{
		Object.Destroy(base.gameObject);
	}
}
