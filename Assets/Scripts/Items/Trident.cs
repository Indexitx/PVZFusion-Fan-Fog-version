using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trident : Bucket
{
    public override void Use()
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
