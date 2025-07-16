using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickaxe : Bucket
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
                    case 1093:
                        component.Die();
                        GameAPP.board.GetComponent<CreatePlant>().SetPlant(component.thePlantColumn, component.thePlantRow, 1105);
                        Object.Destroy(base.gameObject);
                        break;
                    case 1098:
                        component.Die();
                        GameAPP.board.GetComponent<CreatePlant>().SetPlant(component.thePlantColumn, component.thePlantRow, 1108);
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
