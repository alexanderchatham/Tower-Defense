using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//struct for game map
struct nodeMem
{
    //has tower will be used in 
    public bool hasTower;
    public GameObject selectedNode;
    public Node script;
    public Transform waypoint;
}

public class GameMap : MonoBehaviour {
    
    public GameObject nodeBlueprint;
    private int mapWidth = 10;
    private int mapHeight = 30;
    private int iglobal;
    private int jglobal;
    private int lastMove;
    private bool loopBool = false;
    //waypoints 
    public Transform Parent;
    public Transform End;
    public Turret turret;
    private nodeMem[,] nodeMap;
    public ArrayList bestRoute;
    private Vector3 translation;
    public Transform bufferwaypoint;

    public static GameMap instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }
    }

    // Use this for initialization
    void Start () {

        bestRoute = new ArrayList();

        translation = new Vector3(0, .25f, 0);
        //initializing game map
        nodeMap = new nodeMem[mapHeight,mapWidth];

        //populate Gamemap
        int i = 0;
        int j = 0;

        for (i = 0; i < mapHeight; i++)
        {

            for (j = 0; j < mapWidth; j++) {

                nodeMap[i,j].hasTower = false;
                nodeMap[i,j].selectedNode = (GameObject)Instantiate(nodeBlueprint);
                nodeMap[i, j].script = nodeMap[i, j].selectedNode.GetComponent<Node>();
                nodeMap[i, j].script.i = i;
                nodeMap[i, j].script.j = j;
                nodeMap[i,j].selectedNode.transform.position = new Vector3(j * .5f - 4.75f, -.45f, 98f - i * .5f);

                nodeMap[i,j].waypoint = (Transform)Instantiate(End);
                Vector3 offsetPosition = new Vector3(nodeMap[i, j].selectedNode.transform.position.x, nodeMap[i, j].selectedNode.transform.position.y + 1f, nodeMap[i, j].selectedNode.transform.position.z);
                nodeMap[i, j].waypoint.position = offsetPosition;
            }
        }
    }
    
    public void SetNodeBool(int i, int j, bool a)
    {
        nodeMap[i, j].hasTower = a;
    }

    public bool GetNodeBool(int i, int j)
    {
        return nodeMap[i, j].hasTower;
    }

    public void findBestRoute()
    {
        bestRoute.Clear();
        // update the hastower bools
        /*
        for (i = 0; i < mapHeight; i++)
        {

            for (j = 0; j < mapWidth; j++)
            {
                if (nodeMap[i, j].script.turret == null)
                    nodeMap[i, j].hasTower = false;
                if (nodeMap[i, j].script.turret != null)
                {
                    nodeMap[i, j].hasTower = true;
                    print("node i:" + i + " j:" + j + " has a turret!");
                }
            }
        }
        */
        //get the first point in which the enemy will enter the maze
        

        firstPoint();

        findEndOfMaze(iglobal,jglobal);


        bestRoute.Add(End);

        // add points to instance
        Waypoints.instance.setPoints(bestRoute);
        /*
        
        newWaypoint.SetParent(Parent, true);
        newWaypoint.SetAsFirstSibling();
        End.SetAsLastSibling();
        */
    }

    private void firstPoint()
    {
        //To start find an empty box at the top
        int i = 0;
        loopBool = false;
        int j = (mapWidth / 2) -1 ;
        int mag = 1;
        while (!loopBool)
        {
            if (nodeMap[i, j].hasTower == false)
                loopBool = true;
            if (j < 0 || j > mapWidth -1)
            {
                //this is where I will add a method to tell the player they blocked the first row
            }
            //look at the j + mag position
            if (j + mag< mapWidth && nodeMap[i, j + mag].hasTower == false && !loopBool)
            {
                j = j + mag;
                print("j is :" + j);
                loopBool = true;
            }
            //look at the j - mag position
            if (j - mag >= 0 && nodeMap[i, j - mag].hasTower == false && !loopBool)
            {
                j = j - mag;

                print("j is :" + j);
                loopBool = true;

            }
            //if neither place works increase the magnitude to spread the search out
            else if(!loopBool)
                mag++;
            print("mag is :" + mag);
        }
        iglobal = i;
        jglobal = j;
        print("i is: " + i + "\nj is:" + j + " first");

        bufferwaypoint = nodeMap[i, j].waypoint.transform;
        bufferwaypoint.Translate(translation);
        bestRoute.Add(bufferwaypoint);


        bestRoute.Add(nodeMap[i,j].waypoint);
    }


    //for last move variable 1 is down, 2 is right, 3 is left, and 4 is up
    private void findEndOfMaze(int i, int j)
    {
       
        // if i is at the bottom of the map end the find method and add the last waypoint
        if (i == mapHeight - 1)
        {

            print("i is: " + i + " j is:" + j + " last");
            bestRoute.Add(nodeMap[i, j].waypoint);
            return;
        }
        // down
        if (!nodeMap[i + 1, j].hasTower && lastMove !=4)
        {
            i = i + 1;
            lastMove = 1;
            //add waypoint 

            print("i is: " + i + "\nj is:" + j + " down");
            bestRoute.Add(nodeMap[i, j].waypoint);
            //recursive call
            findEndOfMaze(i , j);
            return;
        }
        //add waypoint 
       //will change, go right then go left to look right/left
        //right
        if (j + 1 < mapWidth  && !nodeMap[i, j + 1].hasTower && lastMove != 3)
            {
                j = j + 1;

                print("i is: " + i + " j is:" + j + " right");
                bestRoute.Add(nodeMap[i, j].waypoint);
                lastMove = 2;
                findEndOfMaze(i , j);
                return;
        }
            //left
        if (j - 1 >= 0 && !nodeMap[i,j - 1].hasTower && lastMove != 2)
            {
                j = j - 1;
                print("i is: " + i + " j is:" + j + " left");
                bestRoute.Add(nodeMap[i, j].waypoint);
                lastMove = 3;
                findEndOfMaze(i, j);
                return;

        }
            //up
        if (i - 1 >= 0 && !nodeMap[i - 1, j].hasTower && lastMove != 1)
            {
                i = i - 1;
                print("i is: " + i + " j is:" + j + " up");
                bestRoute.Add(nodeMap[i, j].waypoint);
                lastMove = 4;
                findEndOfMaze(i, j);
                return;

        }
        
    }

}
