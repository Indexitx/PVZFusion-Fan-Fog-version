using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloverPumpkin : Pumpkin
{
    public float timering;
    public void SummonBlover()
    {
        int num = 1;
        GameObject gameObject;
        do
        {
            if (board.boxType[thePlantColumn + num, thePlantRow] == 1)
            {
                CreatePlant.Instance.SetPlant(thePlantColumn + num, thePlantRow, 22);
            }
            gameObject = CreatePlant.Instance.SetPlant(thePlantColumn + num, thePlantRow, 22);
            if (thePlantColumn + num > 9)
            {
                break;
            }
            num++;
        }
        while (gameObject == null);
        if (gameObject != null)
        {
            Vector2 vector = gameObject.GetComponent<Plant>().shadow.transform.position;
            Object.Instantiate(position: new Vector2(vector.x, vector.y + 0.5f), original: GameAPP.particlePrefab[11], rotation: Quaternion.identity, parent: board.transform);
        }
    }
    public override void SpecialUpdate()
    {
        timering -= Time.deltaTime;
        if(timering <= 0)
        {
            SummonBlover();
            
            timering = 25;
        }
    }
    
}
