using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Plant : MonoBehaviour
{
	public Board board;

	public int thePlantColumn;

	public int thePlantRow;

	public int thePlantType;

	public bool isWaterPlant;

	public int thePlantMaxHealth = 300;

	public int returningDamagePercent;

	public int returningDamage;

	public int thePlantHealth = 300;

	public float thePlantSpeed = 1f;

	protected float theConstSpeed;

	public float thePlantAttackInterval;

	public float thePlantAttackCountDown;

	public float thePlantProduceInterval;

	public bool isDrone;

	public bool isMetal;

	public int theMaxReceivedDamage;

	public bool canBeCrashed = true;

    public float thePlantProduceCountDown;

	public float attributeCountdown;

	public float flashCountDown;

	public Vector3 startPos;

	public bool isPot;

	public bool isLily;

	public bool isPumpkin;

	public bool isMagnet;

	public int status;

	public bool isFly;

	public bool isAshy;

	public bool isNut;

	public Plant defence;

	public List<Plant> defenced;

	public int cherryImmunityLevel;

	public int doomImmunityLevel;

	public int explodeImmunityLevel;

	public int minerImmunityLevel;

    public int realMaxHealth;

	public int lostMaxHealth;

	public float time;

	public float damageModifier;

	public Animator anim;

	private bool alwaysLightUp;

	public GameObject shadow;

	public int baseLayer;

	private float brightness = 1f;

	private bool isFlashing;

	public int place;

	public bool isShort;

	public bool isFromWheat;

	public float wheatTime;

	public bool adjustPosByLily;

	private bool isCrashed;

	protected List<Zombie> zombieList = new List<Zombie>();

	protected Zombie targetZombie;

	public int zombieLayer;

	protected virtual void Awake()
	{
		damageModifier = 1;
		defenced = new List<Plant>();
		StartCoroutine("e");
        if (base.transform.Find("Shadow") != null)
		{
			shadow = base.transform.Find("Shadow").gameObject;
		}
		else
		{
			Debug.LogError("Failed to find shadow." + base.gameObject);
		}
		anim = GetComponent<Animator>();
		if (isDrone)
		{
			foreach(GameObject g in Board.Instance.plantArray)
			{
				if(g == null) continue;
				Plant p = g.GetComponent<Plant>();
				if (p.isDrone)
				{
					if(p.thePlantRow == thePlantRow && p.thePlantColumn == thePlantColumn)
					{
                        if (InGameText.Instance != null)
                        {
                            InGameText.Instance.EnableText("Here is a drone already!", 3f);
                        }
                        Die();
					}
				}
			}
		}
        theConstSpeed = Random.Range(0.9f, 1.1f);
		zombieLayer = LayerMask.GetMask("Zombie");    
    }
	IEnumerator e()
	{
		yield return new WaitForSeconds(0.2f);
        if (isPumpkin)
        {
            foreach (GameObject g in Board.Instance.plantArray)
            {
                if (g == null) continue;
                Plant p = g.GetComponent<Plant>();
                if (p == this) continue;
				if(p.thePlantRow == thePlantRow && p.thePlantColumn == thePlantColumn)
				{
                    p.defence = this;
					if (!defenced.Contains(p))
					{
						defenced.Add(p);
					}
					continue;
                }
				else
				{
                    if (defenced.Contains(p))
                    {
                        if(p.defence == this)
						{
							defenced.Remove(p);
							p.defence = null;
						}
                    }
                }
            }
        }
    }
	protected virtual void Start()
	{
        
    }

	protected virtual void Update()
	{
		PlantUpdate();
		
	}

	protected virtual void FixedUpdate()
	{
		MouseFixedUpdate();
		FlashUpdate();
	}
	public void CheckMagnetSystem()
	{
		damageModifier = 1;
		foreach(GameObject g in Board.Instance.plantArray)
		{
            if (g == null) continue;
            if (g == this.gameObject) continue;
            
			if(g.TryGetComponent<Plant>(out Plant p))
			{
				if (p.isMagnet)
				{
					damageModifier += 0.05f;
				}
			}
        }
	}
	private void FlashUpdate()
	{
		if (flashCountDown > 0f)
		{
			if (flashCountDown > 0.2f)
			{
				SetBrightness(base.gameObject, -30f * flashCountDown + 10f);
			}
			else if (flashCountDown > 0f)
			{
				SetBrightness(base.gameObject, 15f * flashCountDown + 1f);
			}
			flashCountDown -= 0.02f;
			if (flashCountDown == 0f)
			{
				SetBrightness(base.gameObject, 1f);
			}
		}
	}

	private void MouseFixedUpdate()
	{
		if (!alwaysLightUp && !isFlashing && brightness != 1f)
		{
			SetBrightness(base.gameObject, 1f);
		}
		if (Mouse.Instance.theItemOnMouse == null)
		{
			alwaysLightUp = false;
			return;
		}
		RaycastHit2D[] array = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
		for (int i = 0; i < array.Length; i++)
		{
			RaycastHit2D raycastHit2D = array[i];
			if (!raycastHit2D.collider.CompareTag("Plant") || !(raycastHit2D.collider.gameObject == base.gameObject))
			{
				continue;
			}
			if (Mouse.Instance.theItemOnMouse.name == "Shovel")
			{
				if (brightness != 2.5f)
				{
					SetBrightness(base.gameObject, 2.5f);
				}
				alwaysLightUp = true;
				return;
			}
			if (Mouse.Instance.theItemOnMouse.name == "Glove")
			{
				if (brightness != 2.5f)
				{
					SetBrightness(base.gameObject, 2.5f);
				}
				alwaysLightUp = true;
				return;
			}
		}
		if (Mouse.Instance.theItemOnMouse.CompareTag("Preview") && !Board.Instance.isIZ && MixData.data[thePlantType, Mouse.Instance.thePlantTypeOnMouse] != 0)
		{
			if (brightness != 2.5f)
			{
				SetBrightness(base.gameObject, 2.5f);
			}
			alwaysLightUp = true;
		}
		else
		{
			alwaysLightUp = false;
		}
	}

	protected virtual void PlantUpdate()
	{
		if (thePlantHealth <= 0)
		{
            Die();
			
		}
		time -= Time.deltaTime;
		if (time <= 0)
		{
			time = 1f;
			if(thePlantHealth > thePlantMaxHealth)
			{
				thePlantHealth -= lostMaxHealth;
				if(thePlantHealth < thePlantMaxHealth)
				{
					thePlantHealth = thePlantMaxHealth;
                }
			}
		}
		anim.SetFloat("Speed", thePlantSpeed);
		if (isFromWheat && Board.Instance.theCurrentNumOfZombieUncontroled > 0)
		{
			WheatUpdate();
		}
		if (GameAPP.theGameStatus == 0 && attributeCountdown > 0f)
		{
			attributeCountdown -= Time.deltaTime;
		}
		SpecialUpdate();
	}
	public virtual void SpecialUpdate()
	{

	}
	public virtual void TakeDamage(int damage, Zombie z = null)
	{
		if(defence != null)
		{
			defence.TakeDamage(damage, z);
			return;
		}
		if(theMaxReceivedDamage != 0)
		{
			if(theMaxReceivedDamage < damage)
			{
				theMaxReceivedDamage = damage;
			}
		}
		if(z != null)
		{
			if(returningDamage != 0)
			{
				z.TakeDamage(0, returningDamage);
			}
			else if(returningDamagePercent != 0)
			{
				z.TakeDamage(0, Mathf.RoundToInt(returningDamagePercent * damage));
			}
		}
		thePlantHealth -= damage;
	}
	public virtual void Click()
	{

	}
	private void WheatUpdate()
	{
		wheatTime += Time.deltaTime;
		if (wheatTime > 31f)
		{
			Die(1);
			int num = Random.Range(0, 20);
			if (Board.Instance.isEveStarted && num == 10)
			{
				GameObject obj = Board.Instance.createPlant.SetPlant(thePlantColumn, thePlantRow, Random.Range(901, 905));
				obj.GetComponent<Plant>().wheatTime = 20f;
				obj.GetComponent<Plant>().isFromWheat = true;
				Vector2 vector = shadow.transform.position;
				Object.Instantiate(position: new Vector2(vector.x, vector.y + 0.5f), original: GameAPP.particlePrefab[11], rotation: Quaternion.identity, parent: board.transform);
			}
			else
			{
                Board.Instance.SetEvePlants(thePlantColumn, thePlantRow, fromWheat: true).GetComponent<Plant>().isFromWheat = true;
			}
		}
	}

	public void FlashOnce()
	{
		if (!alwaysLightUp)
		{
			flashCountDown = 0.3f;
		}
	}

	public virtual void Die(int reason = 0)
	{
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

	protected bool TryRemoveFromList()
	{
		for (int i = 0; i < Board.Instance.plantArray.Length; i++)
		{
			if (Board.Instance.plantArray[i] == base.gameObject)
			{
                Board.Instance.plantArray[i] = null;
				return true;
			}
		}
		return false;
	}

	protected virtual GameObject SearchZombie()
	{
		foreach (GameObject item in GameAPP.board.GetComponent<Board>().zombieArray)
		{
			if (item != null)
			{
				Zombie component = item.GetComponent<Zombie>();
				if (component.theZombieRow == thePlantRow && component.shadow.transform.position.x < 9.2f && component.shadow.transform.position.x > shadow.transform.position.x && SearchUniqueZombie(component) && !component.isFlying)
				{
					return item;
				}
			}
		}
		return null;
	}
    protected virtual GameObject SearchFlyingZombie()
    {
        return null;
    }

    protected virtual void PlantShootUpdate()
	{
		thePlantAttackCountDown -= Time.deltaTime;
		if (thePlantAttackCountDown < 0f)
		{
			thePlantAttackCountDown = thePlantAttackInterval;
			thePlantAttackCountDown += Random.Range(-0.1f, 0.1f);
			if (SearchZombie() != null)
			{
				anim.SetTrigger("shoot");
			}
			else if (Board.Instance.isScaredyDream && thePlantType == 9)
			{
				anim.SetTrigger("shoot");
			}
		}
	}

	public virtual void ProducerUpdate()
	{
	}

	protected void SetBrightness(GameObject obj, float b)
	{
		brightness = b;
		if (obj.TryGetComponent<SpriteRenderer>(out var component))
		{
			component.material.SetFloat("_Brightness", b);
		}
		if (obj.transform.childCount <= 0)
		{
			return;
		}
		foreach (Transform item in obj.transform)
		{
			SetBrightness(item.gameObject, b);
		}
	}

	public virtual void Recover(int health)
	{
		thePlantHealth += health;
		if(realMaxHealth != 0)
		{
            if (thePlantHealth > realMaxHealth)
            {
                thePlantHealth = realMaxHealth;
            }
        }
		else
		{
            if (thePlantHealth > thePlantMaxHealth)
            {
                thePlantHealth = thePlantMaxHealth;
            }
        }
			Vector3 position = shadow.transform.position;
		position = new Vector3(position.x, position.y + 0.7f, 1f);
		Object.Instantiate(GameAPP.particlePrefab[16], Board.Instance.transform).transform.position = position;
	}

    public virtual void ZomboniAttacked(Zombie z)
    {
        
        if (theMaxReceivedDamage == 0 && canBeCrashed)
        {
			Crashed();
        }
		else if(theMaxReceivedDamage != 0 && canBeCrashed)
		{
			if(theMaxReceivedDamage >= thePlantHealth)
			{
				Crashed();
			}
			else
			{
                z.Knockback(1f);
                GameAPP.PlaySound(Random.Range(72, 75));
                TakeDamage(theMaxReceivedDamage);
            }
				
        }
		else if(theMaxReceivedDamage == 0 && !canBeCrashed)
		{
            if (1500 >= thePlantHealth)
            {
                Crashed();
            }
            else
            {
                z.Knockback(1f);
                GameAPP.PlaySound(Random.Range(72, 75));
                TakeDamage(1500);
            }
        }
    }

    public virtual void Crashed()
	{
		if (isCrashed)
		{
			return;
		}
		isCrashed = true;
		if (TypeMgr.IsPotatoMine(thePlantType) && isAshy && thePlantType != 1015 && thePlantType != 1010)
		{
			return;
		}
		switch (thePlantType)
		{
		case 2:
			GetComponent<CherryBomb>().Bomb();
			break;
		case 1002:
			GetComponent<SunBomb>().Bomb();
			break;
		case 10:
            Board.Instance.CreateFreeze(shadow.transform.position);
			Die();
			break;
		case 11:
			GetComponent<DoomShroom>().AnimExplode();
			Die();
			break;
		case 1040:
			GetComponent<IceDoom>().AnimExplode();
			Die();
			break;
		case 16:
			GetComponent<Jalapeno>().AnimExplode();
			break;
		case 13:
		case 15:
		case 17:
		case 1049:
		case 1050:
		case 1051:
		case 1054:
		case 1057:
		case 1060:
		case 1066:
			return;
		case 1003:
		case 1010:
		case 1015:
		case 1052:
		case 1053:
			Die();
			break;
		}
		if (Board.Instance.boxType[thePlantColumn, thePlantRow] == 1)
		{
			Die();
			return;
		}
		if (Board.Instance.isIZ && !board.isEveStarted)
		{
			GiveSunInIZ();
		}
		Object.Destroy(anim);
		Vector3 position = shadow.transform.position;
		base.transform.localScale = new Vector3(base.transform.localScale.x, 0.3f * base.transform.localScale.y);
		Vector3 position2 = shadow.transform.position;
		Vector3 vector = position - position2;
		base.transform.position += vector;
		shadow.SetActive(value: false);
		if (Mouse.Instance.thePlantOnGlove == base.gameObject)
		{
			Object.Destroy(Mouse.Instance.theItemOnMouse);
			Mouse.Instance.theItemOnMouse = null;
			Mouse.Instance.thePlantTypeOnMouse = -1;
			Mouse.Instance.thePlantOnGlove = null;
		}
		TryRemoveFromList();
		Collider2D[] components = GetComponents<Collider2D>();
		for (int i = 0; i < components.Length; i++)
		{
			Object.Destroy(components[i]);
		}
		Object.Destroy(base.gameObject, 3f);
		Object.Destroy(this);
	}
	

    public void GiveSunInIZ()
	{
		if (thePlantType != 4)
		{
			CreateCoin.Instance.SetCoin(thePlantColumn, thePlantRow, 0, 0);
			CreateCoin.Instance.SetCoin(thePlantColumn, thePlantRow, 0, 0);
			CreateCoin.Instance.SetCoin(thePlantColumn, thePlantRow, 0, 0);
			CreateCoin.Instance.SetCoin(thePlantColumn, thePlantRow, 0, 0);
		}
	}

	protected virtual bool SearchUniqueZombie(Zombie zombie)
	{
		if (zombie == null)
		{
			return false;
		}
		if (zombie.isMindControlled)
		{
			return false;
		}
		int theStatus = zombie.theStatus;
		if (theStatus == 1 || theStatus == 3 || theStatus == 9)
		{
			return false;
		}
		if (thePlantType == 1004)
		{
			return true;
		}
		if (zombie.theStatus == 7)
		{
			return false;
		}
		return true;
	}

	protected bool AttackUniqueZombie(Zombie zombie)
	{
		if (zombie == null)
		{
			return false;
		}
		if (zombie.isMindControlled)
		{
			return false;
		}
		int theStatus = zombie.theStatus;
		if (theStatus == 3 || theStatus == 9)
		{
			return false;
		}
		return true;
	}

	public void SetColor(GameObject obj, Color color)
	{
		if (obj.name == "Shadow")
		{
			return;
		}
		if (obj.TryGetComponent<SpriteRenderer>(out var component))
		{
			component.color = color;
		}
		if (obj.transform.childCount == 0)
		{
			return;
		}
		foreach (Transform item in obj.transform)
		{
			SetColor(item.gameObject, color);
		}
	}
}
