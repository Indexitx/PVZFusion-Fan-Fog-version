using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateItem : MonoBehaviour
{
    public static CreateItem Instance;

    private void Awake()
    {
        Instance = this;
    }

    public GameObject SetItem(float theX, float theY, int theItemType, Vector3 pos = default(Vector3))
    {
        GameObject gameObject = GameAPP.itemPrefab[theItemType];
        GameObject g = Instantiate(gameObject, new Vector3(theX, theY, -2f), Quaternion.identity);
        g.transform.SetParent(Board.Instance.transform);
        return g;
    }
}
