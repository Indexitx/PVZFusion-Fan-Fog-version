using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarTeor : MonoBehaviour
{
    public Transform target;
    public Quaternion targetRotation;
    public float speed = 2.0f;
    public bool isExploded;
    public GameObject sprite;
    public int damage;
    public int bulletType;
    private GameObject[] gs = new GameObject[240];
    public float rotationSpeed = 90.0f;
    public int curShootCount = 0;
    public int maxShootCount = 4;
    public bool isAutotargeting;
    public GameObject particle;
    public bool isAttackFlying;
    public void Awake()
    {
        GameObject[] g = GameObject.FindGameObjectsWithTag("METEORTarget");
        target = g[0].transform;
        targetRotation = Quaternion.Euler(0, 0, 90);
    }

    public void Update()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
        float step2 = rotationSpeed * Time.deltaTime;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, step2);
        if (transform.rotation == targetRotation)
        {
            if(targetRotation == Quaternion.Euler(0, 0, 90))
            {
                targetRotation = Quaternion.Euler(0, 0, 180);
            }
            else if(targetRotation == Quaternion.Euler(0, 0, 180))
            {
                targetRotation = Quaternion.Euler(0, 0, 270);
            }
            else if (targetRotation == Quaternion.Euler(0, 0, 270))
            {
                targetRotation = Quaternion.Euler(0, 0, 360);
            }
            else if (targetRotation == Quaternion.Euler(0, 0, 360))
            {
                targetRotation = Quaternion.Euler(0, 0, 90);
            }
        }
        if (transform.position == target.position && !isExploded)
        {
            Explode();
            isExploded = true;
            Destroy(sprite);
        }
    }
    
    public virtual void Explode()
    {
        Instantiate(particle, transform.position, Quaternion.identity);
        StartCoroutine(BulletShoot());
        GameAPP.PlaySound(41);
    }
    IEnumerator BulletShoot()
    {
        curShootCount++;
        gs[0] = CreateBullet.Instance.SetBullet(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y), 0, bulletType, 0, canAttackOtherLanes:true);
        gs[1] = CreateBullet.Instance.SetBullet(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y), 0, bulletType, 0, Quaternion.Euler(0, 0, 22.5f), true);
        gs[2] = CreateBullet.Instance.SetBullet(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y), 0, bulletType, 0, Quaternion.Euler(0, 0, 45f), true);
        gs[3] = CreateBullet.Instance.SetBullet(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y), 0, bulletType, 0, Quaternion.Euler(0, 0, 67.5f), true);
        gs[4] = CreateBullet.Instance.SetBullet(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y), 0, bulletType, 0, Quaternion.Euler(0, 0, 90f), true);
        gs[5] = CreateBullet.Instance.SetBullet(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y), 0, bulletType, 0, Quaternion.Euler(0, 0, 112.5f), true);
        gs[6] = CreateBullet.Instance.SetBullet(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y), 0, bulletType, 0, Quaternion.Euler(0, 0, 135f), true);
        gs[7] = CreateBullet.Instance.SetBullet(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y), 0, bulletType, 0, Quaternion.Euler(0, 0, 157.5f), true);
        gs[8] = CreateBullet.Instance.SetBullet(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y), 0, bulletType, 7, canAttackOtherLanes: true);
        gs[9] = CreateBullet.Instance.SetBullet(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y), 0, bulletType, 0, Quaternion.Euler(0, 0, -22.5f), true);
        gs[10] = CreateBullet.Instance.SetBullet(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y), 0, bulletType, 0, Quaternion.Euler(0, 0, -45f), true);
        gs[11] = CreateBullet.Instance.SetBullet(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y), 0, bulletType, 0, Quaternion.Euler(0, 0, -67.5f), true);
        gs[12] = CreateBullet.Instance.SetBullet(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y), 0, bulletType, 0, Quaternion.Euler(0, 0, -90f), true);
        gs[13] = CreateBullet.Instance.SetBullet(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y), 0, bulletType, 0, Quaternion.Euler(0, 0, -112.5f), true);
        gs[14] = CreateBullet.Instance.SetBullet(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y), 0, bulletType, 0, Quaternion.Euler(0, 0, -135f), true);
        gs[15] = CreateBullet.Instance.SetBullet(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y), 0, bulletType, 0, Quaternion.Euler(0, 0, -157.5f), true);
        gs[16] = CreateBullet.Instance.SetBullet(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y), 0, bulletType, 0, Quaternion.Euler(0, 0, 11.25f), true);
        gs[17] = CreateBullet.Instance.SetBullet(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y), 0, bulletType, 0, Quaternion.Euler(0, 0, 56.25f), true);
        gs[18] = CreateBullet.Instance.SetBullet(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y), 0, bulletType, 0, Quaternion.Euler(0, 0, 78.75f), true);
        gs[19] = CreateBullet.Instance.SetBullet(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y), 0, bulletType, 0, Quaternion.Euler(0, 0, 101.25f), true);
        gs[20] = CreateBullet.Instance.SetBullet(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y), 0, bulletType, 0, Quaternion.Euler(0, 0, 123.75f), true);
        gs[21] = CreateBullet.Instance.SetBullet(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y), 0, bulletType, 0, Quaternion.Euler(0, 0, 146.25f), true);
        gs[22] = CreateBullet.Instance.SetBullet(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y), 0, bulletType, 0, Quaternion.Euler(0, 0, 168.75f), true);
        gs[23] = CreateBullet.Instance.SetBullet(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y), 0, bulletType, 0, Quaternion.Euler(0, 0, -11.25f), true);
        gs[24] = CreateBullet.Instance.SetBullet(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y), 0, bulletType, 0, Quaternion.Euler(0, 0, -56.25f), true);
        gs[25] = CreateBullet.Instance.SetBullet(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y), 0, bulletType, 0, Quaternion.Euler(0, 0, -78.75f), true);
        gs[26] = CreateBullet.Instance.SetBullet(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y), 0, bulletType, 0, Quaternion.Euler(0, 0, -101.25f), true);
        gs[27] = CreateBullet.Instance.SetBullet(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y), 0, bulletType, 0, Quaternion.Euler(0, 0, -123.75f), true);
        gs[28] = CreateBullet.Instance.SetBullet(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y), 0, bulletType, 0, Quaternion.Euler(0, 0, -146.25f), true);
        gs[29] = CreateBullet.Instance.SetBullet(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y), 0, bulletType, 0, Quaternion.Euler(0, 0, -168.75f), true);
        foreach (GameObject g in gs)
        {
            if (g == null) continue;
            else
            {
                g.GetComponent<Bullet>().theBulletDamage = damage;
            }
        }
        if (isAutotargeting)
        {
            StartCoroutine(ChangeMoveWay());
        }
        if (isAttackFlying)
        {
            foreach (GameObject g in gs)
            {
                if (g == null) continue;
                else
                {
                    g.GetComponent<Bullet>().isAttackFlying = true;
                }
            }
        }
        yield return new WaitForSeconds(0.05f);
        if(curShootCount < maxShootCount)
        {
            StartCoroutine(BulletShoot());
        }
        else
        {
            Destroy(gameObject, 1.5f);
        }
    }
    IEnumerator ChangeMoveWay()
    {
        yield return new WaitForSeconds(0.02f);
        foreach(GameObject g in gs)
        {
            if (g == null) continue;
            else
            {
                g.GetComponent<Bullet>().theMovingWay = 8;
            }
        }
    }
}
