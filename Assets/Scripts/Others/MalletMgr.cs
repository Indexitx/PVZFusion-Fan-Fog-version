using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MalletMgr : MonoBehaviour
{
    public bool isPickUp;

    public GameObject defaultParent;

    private float fullCD = 45f;

    public float CD;

    public bool avaliable = true;

    private RectTransform r;

    protected Mouse m;

    private void Start()
    {
        r = base.transform.GetChild(0).gameObject.GetComponent<RectTransform>();
        m = GameAPP.board.GetComponent<Mouse>();
        if (GameAPP.theBoardType == 1)
        {
            fullCD = 45f;
        }
    }

    public void PickUp()
    {
        isPickUp = true;
        GetComponent<BoxCollider2D>().enabled = false;
        base.transform.SetParent(GameAPP.canvasUp.transform);
    }

    public void PutDown()
    {
        isPickUp = false;
        GetComponent<BoxCollider2D>().enabled = true;
        base.transform.SetParent(defaultParent.transform);
        GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, 0f);
    }

    private void OnMouseEnter()
    {
        if (m.theItemOnMouse == null)
        {
            CursorChange.SetClickCursor();
        }
    }

    private void OnMouseExit()
    {
        CursorChange.SetDefaultCursor();
    }

    private void Update()
    {
        CDUpdate();
        if (Input.GetKeyDown(KeyCode.Alpha2) && GameAPP.theGameStatus == 0 && !isPickUp && avaliable && m.theItemOnMouse == null)
        {
            m.theItemOnMouse = base.gameObject;
            GameAPP.PlaySound(19);
            PickUp();
        }
    }

    private void CDUpdate()
    {
        r.anchoredPosition = new Vector2(0f, CD * (10f / fullCD) * 7.5f);
        if (GameAPP.developerMode)
        {
            CD = fullCD;
        }
        if (CD < fullCD)
        {
            CD += Time.deltaTime;
            avaliable = false;
            if (CD > fullCD)
            {
                avaliable = true;
                CD = fullCD;
            }
        }
        if (CD >= fullCD)
        {
            avaliable = true;
        }
    }
    public void Attack(Vector2 v)
    {
        if(GameAPP.theGameStatus != 0)
        {
            return;
        }
        GameObject gameObject = GameAPP.particlePrefab[51];
        Vector3 p = new Vector3(v.x, v.y, 0);
        Collider2D[] col = Physics2D.OverlapCircleAll(p, 0.4f);
        foreach(Collider2D c in col)
        {
            if(c == null) continue;
            if(c.TryGetComponent<Zombie>(out var z))
            {
                z.TakeDamage(0, 1000000);
            }
        }
        GameAPP.PlaySound(Random.Range(85, 87));
    }
}
