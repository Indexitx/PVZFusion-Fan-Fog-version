using Unity.VisualScripting;
using UnityEngine;

public class QuickJackson : Zombie
{
	private float moonWalkTime;
	public bool inCollision;

	private GameObject[] dancer = new GameObject[4];

	private bool isMoonWalkFinish;

	public bool isExploded;

	private float explodeTime = 20f;

	private bool isAbledToAttack;

	protected override void Update()
	{
		base.Update();
		
		if (GameAPP.theGameStatus == 0)
		{
			if (!isMoonWalkFinish)
			{
                Knockback(5 * Time.deltaTime);
            }
			
			moonWalkTime += Time.deltaTime;
		}
		if (moonWalkTime > 3f && !isMoonWalkFinish)
		{
			theOriginSpeed = 1f;
			anim.SetTrigger("summon");
			isMoonWalkFinish = true;
		}
		if(explodeTime != 0 && theStatus != 1 && !isExploded)
		{
            explodeTime -= Time.deltaTime;
            if (explodeTime <= 0)
            {
				isExploded = true;
				explodeTime = 0;
                GameObject g = Object.Instantiate(GameAPP.particlePrefab[25], shadow.transform.position, Quaternion.identity);
				g.GetComponent<Doom>().theDoomType = 6;
                ScreenShake.TriggerShake();
                GameAPP.PlaySound(41);
				Charred();
            }
        }
		
	}
	protected override void FixedUpdate()
	{
		base.FixedUpdate();
		if (theZombieRow == 0)
		{
			if (dancer[1] == null || dancer[2] == null || dancer[3] == null)
			{
				anim.SetBool("loseDancer", value: true);
			}
		}
		else if (theZombieRow == 4)
		{
			if (dancer[0] == null || dancer[1] == null || dancer[2] == null)
			{
				anim.SetBool("loseDancer", value: true);
			}
		}
		else if (dancer[0] == null || dancer[1] == null || dancer[2] == null || dancer[3] == null)
		{
			anim.SetBool("loseDancer", value: true);
		}
	}

	protected override void OnTriggerStay2D(Collider2D collision)
	{
        if (theStatus != 1 && theAttackTarget == null)
		{
			if (collision.gameObject.CompareTag("Plant") && !isMindControlled)
			{
				Plant component = collision.gameObject.GetComponent<Plant>();
				if (component.thePlantRow == theZombieRow)
				{
					if (!TypeMgr.IsCaltrop(component.thePlantType))
					{
						if (isAbledToAttack)
						{
							theAttackTarget = collision.gameObject;
							anim.SetBool("isAttacking", value: true);
							isAttacking = true;
						}
						else if (!isMoonWalkFinish)
						{
							anim.SetTrigger("summon");
							isMoonWalkFinish = true;
                            Knockback(-2f);
                        }
					}
					return;
				}
			}
			if (collision.gameObject.CompareTag("Zombie"))
			{
				Zombie component2 = collision.gameObject.GetComponent<Zombie>();
				if (component2.theZombieRow == theZombieRow && component2.isMindControlled == !isMindControlled)
				{
					if (isAbledToAttack)
					{
						theAttackTarget = collision.gameObject;
						anim.SetBool("isAttacking", value: true);
						isAttacking = true;
					}
					else if (!isMoonWalkFinish)
					{
						anim.SetTrigger("summon");
						isMoonWalkFinish = true;
                        Knockback(-2f);
                    }
					return;
				}
			}
			if (collision.TryGetComponent<IZEBrains>(out var component3) && component3.theRow == theZombieRow && !isMindControlled)
			{
				if (isAbledToAttack)
				{
					theAttackTarget = collision.gameObject;
					anim.SetBool("isAttacking", value: true);
					isAttacking = true;
				}
				else if (!isMoonWalkFinish)
				{
					anim.SetTrigger("summon");
					Knockback(-2f);
					isMoonWalkFinish = true;
				}
				return;
			}
		}
		if (theStatus != 1)
		{
			if (collision.gameObject == theAttackTarget)
			{
				Zombie component5;
				if (theAttackTarget.TryGetComponent<Plant>(out var component4) && component4.thePlantRow != theZombieRow)
				{
					theAttackTarget = null;
					isAttacking = false;
					anim.SetBool("isAttacking", value: false);
				}
				else if (theAttackTarget.TryGetComponent<Zombie>(out component5) && component5.theZombieRow != theZombieRow)
				{
					theAttackTarget = null;
					isAttacking = false;
					anim.SetBool("isAttacking", value: false);
				}
			}
		}
		else if (theStatus == 1)
		{
			theAttackTarget = null;
			isAttacking = false;
		}
	}

	private void PointOver()
	{
		isAbledToAttack = true;
	}

	private void AnimSummon()
	{
		if (board.isEveStarted && shadow.transform.position.x < 1f)
		{
			return;
		}
		GameAPP.PlaySound(69);
		if (theStatus != 0)
		{
			return;
		}
		anim.SetBool("loseDancer", value: false);
		if (dancer[0] == null && theZombieRow != 0 && board.roadType[theZombieRow - 1] != 1)
		{
			if (isMindControlled)
			{
				dancer[0] = CreateZombie.Instance.SetZombieWithMindControl(0, theZombieRow - 1, 6, shadow.transform.position.x);
			}
			else
			{
				dancer[0] = CreateZombie.Instance.SetZombie(0, theZombieRow - 1, 6, shadow.transform.position.x);
			}
			CreateParticle(dancer[0].transform.Find("Shadow").position);
		}
		if (dancer[1] == null)
		{
			if (isMindControlled)
			{
				dancer[1] = CreateZombie.Instance.SetZombieWithMindControl(0, theZombieRow, 6, shadow.transform.position.x + 1f);
			}
			else
			{
				dancer[1] = CreateZombie.Instance.SetZombie(0, theZombieRow, 6, shadow.transform.position.x + 1f);
			}
			CreateParticle(dancer[1].transform.Find("Shadow").position);
		}
		if (dancer[2] == null)
		{
			if (isMindControlled)
			{
				dancer[2] = CreateZombie.Instance.SetZombieWithMindControl(0, theZombieRow, 6, shadow.transform.position.x - 1.5f);
			}
			else
			{
				dancer[2] = CreateZombie.Instance.SetZombie(0, theZombieRow, 6, shadow.transform.position.x - 1.5f);
			}
			CreateParticle(dancer[2].transform.Find("Shadow").position);
		}
		if (dancer[3] == null && theZombieRow != board.roadNum - 1 && board.roadType[theZombieRow + 1] != 1)
		{
			if (isMindControlled)
			{
				dancer[3] = CreateZombie.Instance.SetZombieWithMindControl(0, theZombieRow + 1, 6, shadow.transform.position.x);
			}
			else
			{
				dancer[3] = CreateZombie.Instance.SetZombie(0, theZombieRow + 1, 6, shadow.transform.position.x);
			}
			CreateParticle(dancer[3].transform.Find("Shadow").position);
		}
	}

	public override void SetMindControl(bool mustControl = false)
	{
		base.SetMindControl(mustControl);
		for (int i = 0; i < dancer.Length; i++)
		{
			dancer[i] = null;
		}
	}

	private void LookForward()
	{
		if (shadow != null)
		{
			if (isMindControlled)
			{
				Vector2 vector = shadow.transform.position;
				base.transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
				AdjustPosition(base.gameObject, vector);
			}
			else
			{
				Vector2 vector2 = shadow.transform.position;
				base.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
				AdjustPosition(base.gameObject, vector2);
			}
		}
	}

	private void LookBack()
	{
		if (shadow != null)
		{
			if (isMindControlled)
			{
				Vector2 vector = shadow.transform.position;
				base.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
				AdjustPosition(base.gameObject, vector);
			}
			else
			{
				Vector2 vector2 = shadow.transform.position;
				base.transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
				AdjustPosition(base.gameObject, vector2);
			}
		}
	}

	protected override void BodyTakeDamage(int theDamage)
	{
		theHealth -= theDamage;
		if (!isLoseHand && theHealth < (float)(theMaxHealth * 2 / 3))
		{
			isLoseHand = true;
			GameAPP.PlaySound(7);
			for (int i = 0; i < base.transform.childCount; i++)
			{
				Transform child = base.transform.GetChild(i);
				if (child.CompareTag("ZombieHand"))
				{
					Object.Destroy(child.gameObject);
				}
				if (child.CompareTag("ZombieArmUpper"))
				{
					child.GetComponent<SpriteRenderer>().sprite = GameAPP.spritePrefab[26];
				}
				if (child.name == "LoseArm")
				{
					child.gameObject.SetActive(value: true);
					child.gameObject.GetComponent<ParticleSystemRenderer>().sortingLayerName = $"zombie{theZombieRow}";
					child.gameObject.GetComponent<ParticleSystemRenderer>().sortingOrder += baseLayer + 29;
					child.gameObject.GetComponent<ParticleSystem>().collision.AddPlane(board.transform.GetChild(2 + theZombieRow));
					child.AddComponent<ZombieHead>();
				}
			}
		}
		if (!(theHealth < (float)(theMaxHealth / 3)) || theStatus == 1)
		{
			return;
		}
		theStatus = 1;
		GameAPP.PlaySound(7);
		for (int j = 0; j < base.transform.childCount; j++)
		{
			Transform child2 = base.transform.GetChild(j);
			if (child2.CompareTag("ZombieHead"))
			{
				Object.Destroy(child2.gameObject);
			}
			if (child2.name == "LoseHead")
			{
				child2.gameObject.SetActive(value: true);
				child2.gameObject.GetComponent<ParticleSystem>().collision.AddPlane(board.transform.GetChild(2 + theZombieRow));
				child2.gameObject.GetComponent<ParticleSystemRenderer>().sortingLayerName = $"zombie{theZombieRow}";
				child2.gameObject.GetComponent<ParticleSystemRenderer>().sortingOrder += baseLayer + 29;
				child2.AddComponent<ZombieHead>();
				Vector3 localScale = child2.transform.localScale;
				child2.transform.SetParent(board.transform);
				child2.transform.localScale = localScale;
			}
		}
	}

	private void AdjustAttackPosition()
	{
		if (shadow != null)
		{
			if (isMindControlled)
			{
				Vector2 vector = shadow.transform.position;
				base.transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
				AdjustPosition(base.gameObject, vector);
			}
			else
			{
				Vector2 vector2 = shadow.transform.position;
				base.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
				AdjustPosition(base.gameObject, vector2);
			}
		}
	}

	private void CreateParticle(Vector3 position)
	{
		Object.Instantiate(position: new Vector3(position.x, position.y + 0.7f), original: GameAPP.particlePrefab[11], rotation: Quaternion.identity, parent: board.transform);
	}
}
