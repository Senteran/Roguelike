using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : PublicClasses
{
    List<Room> Rooms = new List<Room>();

    public GameObject StartingRoom;
    public GameObject RoomPrefab;

    public GameObject Player;

    int playerX, playerY;

    // Start is called before the first frame update
    void Start()
    {
        Rooms.Add(new Room(0, 0, StartingRoom, new Connections(true, true, true, true)));
        StartingRoom.GetComponent<RoomScript>().isActive = false;
        playerX = 0;
        playerY = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AttemptEnter(int direction)
    {
        int oldX = playerX, oldY = playerY;

        if (direction == 0) playerY--;
        else if (direction == 1) playerX++;
        else if (direction == 2) playerY++;
        else if (direction == 3) playerX--;

        bool exists = false;

        foreach (Room rm in Rooms)
        {
            if (rm.x == oldX && rm.y == oldY)
            {
                rm.room.GetComponent<RoomScript>().isActive = false;
                rm.room.SetActive(false);
            }
            if (rm.x == playerX && rm.y == playerY)
            {
                exists = true;
            }
        }
        if (!exists) { GenerateRoom(playerX, playerY); }

        foreach(Room rm in Rooms)
        {
            if (rm.x == playerX && rm.y == playerY)
            {
                rm.room.GetComponent<RoomScript>().isActive = true;
                Player.GetComponent<PlayerScript>().attachedRoom = rm.room;
                rm.room.SetActive(true);
                int x = 0;
                int y = 0;
                switch (direction)
                {
                    case 0:
                        x = rm.room.GetComponent<RoomScript>().ROOMSIZEX / 2;
                        y = rm.room.GetComponent<RoomScript>().ROOMSIZEY - 1;
                        break;
                    case 1:
                        x = 0;
                        y = rm.room.GetComponent<RoomScript>().ROOMSIZEY / 2;
                        break;
                    case 2:
                        x = rm.room.GetComponent<RoomScript>().ROOMSIZEX / 2;
                        y = 0;
                        break;
                    case 3:
                        x = rm.room.GetComponent<RoomScript>().ROOMSIZEX - 1;
                        y = rm.room.GetComponent<RoomScript>().ROOMSIZEY / 2;
                        break;
                }
                rm.room.GetComponent<RoomScript>().UpdateTile(x, y, false, true, false, false, 0, Player);
                Player.GetComponent<PlayerScript>().x = x;
                Player.GetComponent<PlayerScript>().y = y;

                Location loc_world = Player.GetComponent<PlayerScript>().attachedRoom.GetComponent<RoomScript>().GetTileLocation(x, y);
                Player.transform.position = new Vector2(loc_world.x, loc_world.y);
                Player.GetComponent<PlayerScript>().Turn();

                break;
            }
        }
    }

    public void GenerateRoom(int x, int y)
    {
        // TODO Check for the connections to other rooms here first
        int up = 0, right = 0, down = 0, left = 0;
        foreach(Room rm in Rooms)
        {
            if(rm.x == x && rm.y == y - 1)
            {
                if (rm.con.isConnectedDown) up = 1;
                else up = 2;
            }
            else if(rm.x == x && rm.y == y + 1)
            {
                if (rm.con.isConnectedUp) down = 1;
                else down = 2;
            }
            else if(rm.x == x - 1 && rm.y == y)
            {
                if (rm.con.isConnectedRight) left = 1;
                else left = 2;
            }
            else if(rm.x == x + 1 && rm.y == y)
            {
                if (rm.con.isConnectedLeft) right = 1;
                else right = 2;
            }
        }
        Connections con = new Connections(up, right, down, left);
        GameObject o = Instantiate(RoomPrefab, transform);
        // TODO Set the rooms location here
        // TODO Pass arguments (such as connections) to the created room for better generation
        o.GetComponent<RoomScript>().GenerateRoom(con);
        o.SetActive(false);
        o.name = "Room(" + x + ", " + y + ")";
        Rooms.Add(new Room(x, y, o, con));
    }
}
