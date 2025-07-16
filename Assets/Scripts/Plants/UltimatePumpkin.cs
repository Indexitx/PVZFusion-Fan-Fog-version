using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltimatePumpkin : Pumpkin
{
    public float timering;
    public GameObject g;
    public GameObject g2;
    public Transform t;
    protected override void Start()
    {
        base.Start();
        g2 = Instantiate(g, t.position, Quaternion.identity);
        g2.transform.SetParent(Board.Instance.transform);
    }
    public void SummonBlover()
    {
        int column = 0;
        int row = 0;
        GameObject gameObject = null;
        if (board.boxType[column, row] == 1)
        {
            return;
        }
        gameObject = CreatePlant.Instance.SetPlant(thePlantColumn, thePlantRow, 257);
        if (gameObject == null)
        {
            column = thePlantColumn + 1;
            if (column <= 9)
            {
                gameObject = CreatePlant.Instance.SetPlant(column, thePlantRow, 257);
            }
        }
        if (gameObject == null)
        {
            column = thePlantColumn - 1;
            if (column >= 0)
            {
                gameObject = CreatePlant.Instance.SetPlant(column, thePlantRow, 257);
            }
        }
        if (gameObject == null)
        {
            row = thePlantRow - 1;
            if (row >= 0)
            {
                gameObject = CreatePlant.Instance.SetPlant(thePlantColumn, row, 257);
                if (gameObject == null)
                {
                    column = thePlantColumn + 1;
                    if (column <= 9)
                    {
                        gameObject = CreatePlant.Instance.SetPlant(column, row, 257);
                    }
                }
                if (gameObject == null)
                {
                    column = thePlantColumn - 1;
                    if (column >= 0)
                    {
                        gameObject = CreatePlant.Instance.SetPlant(column, row, 257);
                    }
                }
            }
        }
        if (gameObject == null)
        {
            row = thePlantRow + 1;
            if (row <= 4)
            {
                gameObject = CreatePlant.Instance.SetPlant(thePlantColumn, row, 257);
                if (gameObject == null)
                {
                    column = thePlantColumn + 1;
                    if (column <= 9)
                    {
                        gameObject = CreatePlant.Instance.SetPlant(column, row, 257);
                    }
                }
                if (gameObject == null)
                {
                    column = thePlantColumn - 1;
                    if (column >= 0)
                    {
                        gameObject = CreatePlant.Instance.SetPlant(column, row, 257);
                    }
                }
            }
        }
        if (gameObject != null)
        {
            Vector2 vector = gameObject.GetComponent<Plant>().shadow.transform.position;
            Object.Instantiate(position: new Vector2(vector.x, vector.y + 0.5f), original: GameAPP.particlePrefab[11], rotation: Quaternion.identity, parent: board.transform);
        }
    }
    public override void SpecialUpdate()
    {
        timering -= Time.deltaTime;
        if (timering <= 0)
        {
            SummonBlover();
            Blow();
            timering = 25;
        }
        g2.GetComponent<Drone>().startPosition = t.position;
    }
    public override void Die(int dieReason)
    {
        Destroy(g2.gameObject);
        base.Die(dieReason);
    }
    public virtual void Blow()
    {
        GameObject[] gs = GameObject.FindGameObjectsWithTag("Fog");
        foreach (GameObject g in gs)
        {
            if (g == null) continue;
            if (g.TryGetComponent<Fog>(out Fog fog))
            {
                fog.Dispel(true, 20);
            }
        }
        foreach (GameObject g in Board.Instance.zombieArray)
        {
            if (g == null) continue;
            if (g.TryGetComponent<Zombie>(out Zombie z))
            {
                z.CloverAttacked();
            }
        }
    }
}
