using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurnScript : MonoBehaviour
{
    public int monsterType = 0;

    public void Turn(int xPlayer, int yPlayer, int xOwn, int yOwn)
    {
        if (monsterType == 0) gameObject.GetComponent<BasicEnemyAI>().Turn(xPlayer, yPlayer, xOwn, yOwn);
    }
}
