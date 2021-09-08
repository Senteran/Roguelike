using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PublicClasses : MonoBehaviour
{
    public class Location
    {
        public Location(float xSet, float ySet)
        {
            x = xSet;
            y = ySet;
        }
        public float x;
        public float y;
    }

    public class Room
    {
        public Room(int xLoc, int yLoc, GameObject obj, Connections connections)
        {
            x = xLoc;
            y = yLoc;
            room = obj;
            con = connections;
        }
        public int x { get; set; }
        public int y { get; set; }
        public GameObject room { get; set; }
        public Connections con { get; set; }
    }

    public class Connections
    {
        public Connections(bool up = true, bool right = true, bool down = true, bool left = true)
        {
            isConnectedUp = up;
            isConnectedRight = right;
            isConnectedDown = down;
            isConnectedLeft = left;
        }
        public Connections(int up, int right, int down, int left)
        {
            if(up == 0)
            {
                isConnectedUp = Random.Range(0, 9) >= 7;
            }
            else if(up == 1)
            {
                isConnectedUp = true;
            }
            else if(up == 2)
            {
                isConnectedUp = false;
            }
            if(right == 0)
            {
                isConnectedRight = Random.Range(0, 9) >= 7;
            }
            else if(right == 1)
            {
                isConnectedRight = true;
            }
            else if(right == 2)
            {
                isConnectedRight = false;
            }
            if(down == 0)
            {
                isConnectedDown = Random.Range(0, 9) >= 7;
            }
            else if(down == 1)
            {
                isConnectedDown = true;
            }
            else if(down == 2)
            {
                isConnectedDown = false;
            }
            if(left == 0)
            {
                isConnectedLeft = Random.Range(0, 9) >= 7;
            }
            else if(left == 1)
            {
                isConnectedLeft = true;
            }
            else if(left == 2)
            {
                isConnectedLeft = false;
            }

        }
        public string GetLocationString()
        {
            return "Up: " + isConnectedUp + ", Right: " + isConnectedRight + ", Down: " + isConnectedDown + ", Left: " + isConnectedLeft;
        }

        public bool isConnectedUp { get; set; } = true;
        public bool isConnectedRight { get; set; } = true;
        public bool isConnectedDown { get; set; } = true;
        public bool isConnectedLeft { get; set; } = true;
    }
}
