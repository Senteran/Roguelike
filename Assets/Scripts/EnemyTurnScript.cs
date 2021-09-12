using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurnScript : PublicClasses
{
    public int monsterType = 0;

    public int x, y;

    public int attackTurns = 0;

    public void Turn(int xPlayer, int yPlayer, RoomScript rs)
    {
        if (attackTurns == 0)
        {
            if (monsterType == 0) gameObject.GetComponent<BasicEnemyAI>().Turn(new WorldLocation(xPlayer, yPlayer), new WorldLocation(x, y), rs, this);
            else Debug.Log("Monster type not implemented");
        }
        else attackTurns--;
    }

    public void Create(int xLoc, int yLoc)
    {
        x = xLoc;
        y = yLoc;
    }

    public void Move(WorldLocation loc)
    {
        x = loc.x;
        y = loc.y;
    }
}
