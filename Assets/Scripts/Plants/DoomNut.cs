using UnityEngine;

public class DoomNut : WallNut
{
	public override void Die(int reason = 0)
	{
        Vector2 position = new Vector2(shadow.transform.position.x - 0.3f, shadow.transform.position.y + 0.3f);
        board.SetDoom(thePlantColumn, thePlantRow, setPit: false, position);
        base.Die();
	}
}
