using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : PublicClasses
{
    public GameObject attachedRoom;
    public GameObject roomManager;

    bool playerTurn = true;

    public int x = 0, y = 0;
    int old_x = 0, old_y = 0;

    public int maxHealth = 5;
    int health;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        Location loc = attachedRoom.GetComponent<RoomScript>().GetStartingPlayerLocation();
        x = (int)loc.x;
        y = (int)loc.y;
        Location loc_world = attachedRoom.GetComponent<RoomScript>().GetTileLocation(x, y);
        transform.position = new Vector2(loc_world.x, loc_world.y);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTurn)
        {
            if(Input.GetKeyDown("d")){
                if (attachedRoom.GetComponent<RoomScript>().MoveRight(x, y))
                {
                    old_x = x; old_y = y;
                    x++;
                    Move(1);
                }
            }
            else if(Input.GetKeyDown("a")){
                if (attachedRoom.GetComponent<RoomScript>().MoveLeft(x, y))
                {
                    old_x = x; old_y = y;
                    x--;
                    Move(3);
                }
            }
            else if(Input.GetKeyDown("w")){
                if (attachedRoom.GetComponent<RoomScript>().MoveUp(x, y))
                {
                    old_x = x; old_y = y;
                    y--;
                    Move(0);
                }
            }
            else if(Input.GetKeyDown("s")){
                if (attachedRoom.GetComponent<RoomScript>().MoveDown(x, y))
                {
                    old_x = x; old_y = y;
                    y++;
                    Move(2);
                }
            }
        }
    }

    void Move(int direction)
    {
        playerTurn = false;
        Debug.Log("check if new room");
        if (attachedRoom.GetComponent<RoomScript>().CheckIfMoveToNewRoom(x, y))
        {
            roomManager.GetComponent<RoomManager>().AttemptEnter(direction);
        }
        else {
            attachedRoom.GetComponent<RoomScript>().UpdateTile(x, y, false, true);
            attachedRoom.GetComponent<RoomScript>().ResetTile(old_x, old_y);
            Location loc = attachedRoom.GetComponent<RoomScript>().GetTileLocation(x, y);
            transform.position = new Vector2(loc.x, loc.y);
            attachedRoom.GetComponent<RoomScript>().Turn(gameObject.GetComponent<PlayerScript>(), x, y); 
        }
    }

    public void Turn()
    {
        playerTurn = true;
    }

    public void TakeDamage()
    {
        Debug.Log("Took damage");
        health--;
        if(health <= 0)
        {
            Death();
        }
    }
    void Death()
    {
        Debug.Log("Died");
    }
}
