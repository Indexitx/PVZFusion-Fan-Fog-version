using System.Collections;
using UnityEngine;

public class Bucket : MonoBehaviour
{
	private readonly float verticalSpeed = 4f;

	protected float gravity = 9.8f;

	private Vector2 velocity;

	private Vector2 startPosition;

	private bool isLand;

	protected Mouse m;

	protected float existTime;

	private bool isFlash;

	private Vector3 startPos;

	protected virtual void Start()
	{
		if (GameAPP.board.GetComponent<Board>().isIZ)
		{
			Object.Destroy(base.gameObject);
		}
		base.transform.Translate(0f, 0f, -1f);
		startPosition = base.transform.position;
		velocity = new Vector2(Random.Range(-1.5f, 1.5f), verticalSpeed);
		m = GameAPP.board.GetComponent<Mouse>();
		GameAPP.board.GetComponent<Board>().theTotalNumOfCoin++;
		int num = GameAPP.board.GetComponent<Board>().theTotalNumOfCoin;
		if (num > 1000)
		{
			num %= 1000;
		}
		int num2 = 5 * num;
		if (TryGetComponent<SpriteRenderer>(out var component))
		{
			component.sortingOrder += num2;
		}
		if (base.transform.childCount != 0)
		{
			foreach (Transform item in base.transform)
			{
				if (item.TryGetComponent<SpriteRenderer>(out var component2))
				{
					component2.sortingOrder += num2;
				}
			}
		}
		Vector2 vector = Camera.main.WorldToViewportPoint(base.transform.position);
		if (vector.x < 0.05f)
		{
			vector.x = 0.05f;
		}
		else if (vector.x > 0.95f)
		{
			vector.x = 0.95f;
		}
		if (vector.y < 0.15f)
		{
			vector.y = 0.15f;
		}
		else if (vector.y > 0.9f)
		{
			vector.y = 0.9f;
		}
		base.transform.position = Camera.main.ViewportToWorldPoint(vector);
		base.transform.position = new Vector3(base.transform.position.x, base.transform.position.y, 0f);
		startPos = base.transform.position;
		startPosition = startPos;
	}

	public void PutDown()
	{
		GetComponent<Collider2D>().enabled = true;
		base.transform.position = startPos;
	}

	protected virtual void Update()
	{
		PositionUpdate();
		existTime += Time.deltaTime;
		if (existTime > 4f && !isFlash)
		{
			isFlash = true;
			StartCoroutine(Flash());
		}
		if (existTime > 8f && m.theItemOnMouse != base.gameObject)
		{
			Object.Destroy(base.gameObject);
		}
	}

	private IEnumerator Flash()
	{
		Color color = Color.white;
		bool decrease = true;
		while (true)
		{
			if (m.theItemOnMouse != base.gameObject && TryGetComponent<SpriteRenderer>(out var component))
			{
				if (component.color.r > 0.5f && decrease)
				{
					color.r -= existTime * 2f / 255f;
					color.g -= existTime * 2f / 255f;
					color.b -= existTime * 2f / 255f;
					component.color = color;
					if (component.color.r < 0.5f)
					{
						decrease = false;
					}
				}
				else
				{
					color.r += existTime * 2f / 255f;
					color.g += existTime * 2f / 255f;
					color.b += existTime * 2f / 255f;
					component.color = color;
					if (component.color.r > 0.9f)
					{
						decrease = true;
					}
				}
			}
			yield return new WaitForFixedUpdate();
		}
	}

	protected void PositionUpdate()
	{
		if (!isLand)
		{
			velocity.y -= gravity * Time.deltaTime;
			base.transform.Translate(velocity * Time.deltaTime);
			if (base.transform.position.y < startPosition.y - 1f)
			{
				isLand = true;
			}
		}
	}

	public virtual void Pick()
	{
		isLand = true;
		m.theItemOnMouse = base.gameObject;
		m.thePlantOnGlove = null;
		m.thePlantTypeOnMouse = -1;
		GetComponent<Collider2D>().enabled = false;
	}

	public virtual void Use()
	{
		Vector2 vector = new Vector2(m.theMouseColumn, m.theMouseRow);
		GameObject[] plantArray = GameAPP.board.GetComponent<Board>().plantArray;
		foreach (GameObject gameObject in plantArray)
		{
			if (!(gameObject != null))
			{
				continue;
			}
			Plant component = gameObject.GetComponent<Plant>();
			if ((float)component.thePlantColumn == vector.x && (float)component.thePlantRow == vector.y)
			{
				switch (component.thePlantType)
				{
				case 3:
					component.Die();
					GameAPP.board.GetComponent<CreatePlant>().SetPlant(component.thePlantColumn, component.thePlantRow, 1029);
					Object.Destroy(base.gameObject);
					break;
				case 0:
					component.Die();
					GameAPP.board.GetComponent<CreatePlant>().SetPlant(component.thePlantColumn, component.thePlantRow, 1020);
					Object.Destroy(base.gameObject);
					break;
				case 1093:
					component.Die();
					GameAPP.board.GetComponent<CreatePlant>().SetPlant(component.thePlantColumn, component.thePlantRow, 1103);
					Object.Destroy(base.gameObject);
					break;
				case 1098:
					component.Die();
					GameAPP.board.GetComponent<CreatePlant>().SetPlant(component.thePlantColumn, component.thePlantRow, 1106);
					Object.Destroy(base.gameObject);
					break;
				}
                if (component.isMetal)
                {
                    component.Recover(1000);
                    Object.Destroy(base.gameObject);
                }
            }
		}
		GetComponent<Collider2D>().enabled = true;
	}
}
