using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyAI : PublicClasses
{
    public GameObject attackIndicatorPrefab;
    private GameObject attackIndicatorObject;

    public void Turn(WorldLocation LocPlayer, WorldLocation LocOwn, RoomScript rs, EnemyTurnScript ets)
    {
        if(attackIndicatorObject != null)
        {
            Destroy(attackIndicatorObject);
        }

        if (LocPlayer.AdjacentIncDiagonal(LocOwn))
        {
            rs.SetTileDanger(LocPlayer.x, LocPlayer.y, 2);
            ets.attackTurns = 2;
            attackIndicatorObject = Instantiate(attackIndicatorPrefab, transform);
            attackIndicatorObject.transform.localPosition = new Vector3(0f, 0f, -0.1f);
            attackIndicatorObject.transform.up = GameObject.FindGameObjectWithTag("Player").transform.position - attackIndicatorObject.transform.position;
        }
        else
        {
            int xDiff = LocPlayer.x - LocOwn.x;
            int yDiff = LocPlayer.y - LocOwn.y;

            WorldLocation oldLoc = new WorldLocation(LocOwn);

            if(Mathf.Abs(xDiff) > Mathf.Abs(yDiff))
            {
                if(xDiff >= 0)
                {
                    LocOwn.x++;
                }
                else
                {
                    LocOwn.x--;
                }
            }
            else
            {
                if(yDiff >= 0)
                {
                    LocOwn.y++;
                }
                else
                {
                    LocOwn.y--;
                }
            }
            Move(LocOwn, oldLoc, ets, rs);
        }
    }

    private void Move(WorldLocation loc, WorldLocation old, EnemyTurnScript ets, RoomScript rs)
    {
        Debug.Log(loc.x + " " + loc.y + " " + old.x + " " + old.y);
        ets.Move(loc);
        rs.UpdateTile(loc.x, loc.y, false, false, false, false, gameObject);
        rs.UpdateTile(old.x, old.y);
        transform.position = rs.GetTileLocationAsVector(loc.x, loc.y);
    }
}
