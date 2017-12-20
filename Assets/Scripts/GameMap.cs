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
    public int direction;
    public bool usedInFinal;
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
    public bool blocked = false;
    public GameObject blockedOverlay;
    enum Directions
    {
        DOWN = 0,
        RIGHT = 1,
        LEFT = 2,
        UP = 3
    };

    private int[] routelengths;
    

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
        routelengths = new int[mapWidth];
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
    public bool GetFinalBool(int i, int j)
    {
        return nodeMap[i, j].usedInFinal;
    }

    public void findBestRoute()
    {
        int i = 0;
        int j = 0;
        bestRoute.Clear();
        // update the hastower bools
        for (i = 0; i < mapHeight; i++)
        {

            for (j = 0; j < mapWidth; j++)
            {
                nodeMap[i, j].usedInFinal = false;
                if (nodeMap[i, j].script.turret == null)
                    nodeMap[i, j].hasTower = false;
                if (nodeMap[i, j].script.turret != null)
                {
                    nodeMap[i, j].hasTower = true;
                    print("node i:" + i + " j:" + j + " has a turret!");
                }
            }
        }
        

        firstPoint();

        routelengthmethod();
        if (blocked == false)
        {
            findEndOfMaze(iglobal, jglobal);
        }

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
        for(int j = 0; j < mapWidth; j++)
        {
            if (nodeMap[0,j].hasTower)
            {
                routelengths[j] = 0;
            }
            else
            {
                routelengths[j] = 1 ;
            }
        }
    }

    private void routelengthmethod()
    {
        int bestroute = 0;
        for(int j = 0; j< mapWidth; j++)
        {
            if(routelengths[j]== 1)
            {
                routelengths[j] = lookahead(0,j,-1);
                if (routelengths[bestroute] <= 0)
                {
                    bestroute = j;
                    jglobal = j;
                }
            }
            print("j is: " + j + "\n routelength is :" + routelengths[j]);
            
        }
        for (int j = 0; j < mapWidth; j++)
        {
            if (routelengths[j] >= mapHeight - 1 && routelengths[bestroute] > routelengths[j])
            {
                bestroute = j;
                jglobal = j;
                print("the best route is: " + j + " length of:" + routelengths[j]);
            }
        }

        iglobal = 0;
       
    }

    private int lookahead(int i, int j, int direction)
    {
        //end
        if (i == mapHeight - 1)
        {
            return 1;
        }

        //down
        if (direction != (int)Directions.UP && !nodeMap[i + 1, j].hasTower && i < mapHeight - 1)
        {
            nodeMap[i, j].direction = (int)Directions.DOWN;
            try
            {
                return 1 + lookahead(i + 1, j, nodeMap[i, j].direction);
            }
            catch
            {
                print("Catch down");
                return -1000;
            }
        }
        //right and left
        if ((direction == (int)Directions.UP || direction == (int)Directions.DOWN) && j + 1 < mapWidth && !nodeMap[i, j + 1].hasTower && j - 1 > 0 && !nodeMap[i, j - 1].hasTower)
        {
            int lookRight = lookahead(i, j + 1, 1);
            int lookLeft = lookahead(i, j - 1, 2);

            if ((lookLeft > lookRight && lookRight > 0) || (lookLeft < 0 && lookRight > 0))
            {
                nodeMap[i, j].direction = (int)Directions.RIGHT;
                print("right is: " + lookRight);
                return lookRight;
            }

            if ((lookRight > lookLeft && lookLeft > 0) || (lookLeft > 0 && lookRight < 0))
            {
                nodeMap[i, j].direction = (int)Directions.LEFT;
                print("left is: " + lookLeft);
                return lookLeft;
            }
        }
        //right
        if (direction != (int)Directions.LEFT && j + 1 < mapWidth && !nodeMap[i, j + 1].hasTower)
        {
            nodeMap[i, j].direction = (int)Directions.RIGHT;
            try
            {
                return 1 + lookahead(i, j + 1, nodeMap[i, j].direction);
            }
            catch
            {
                print("Catch right");
                return -1000;
            }
        }
        //left
        if (direction != (int)Directions.RIGHT && j > 0 && !nodeMap[i, j - 1].hasTower)
        {

            nodeMap[i, j].direction = (int)Directions.LEFT;
            try
            {
                return 1 + lookahead(i, j - 1, nodeMap[i, j].direction);
            }
            catch
            {
                print("Catch left");
                return -1000;
            }
            }
        //up
        if (direction != (int)Directions.DOWN && i > 1 && !nodeMap[i-1,j].hasTower)
        {
            nodeMap[i, j].direction = (int)Directions.UP;
            try
            {
                return 1 + lookahead(i - 1, j, nodeMap[i, j].direction);
            }
            catch
            {
                print("Catch up");
                return -1000;
            }
        }
        //catch means a bad route so we will give a bad value
        print("Catch at i:"+i + " j:"+j);
        return -1000;
    }

    //for last move variable 1 is down, 2 is right, 3 is left, and 4 is up
    private void findEndOfMaze(int i, int j)
    {
       
        // if i is at the bottom of the map end the find method and add the last waypoint
        if (i == mapHeight - 1)
        {

            print("i is: " + i + " j is:" + j + " last");
            bestRoute.Add(nodeMap[i, j].waypoint);
            nodeMap[i, j].usedInFinal = true;
            blocked = false;
            return;
        }
        // down
        if (nodeMap[i , j].direction == (int)Directions.DOWN)
        {
            //add waypoint 

            print("i is: " + i + "\nj is:" + j + " down");
            bestRoute.Add(nodeMap[i, j].waypoint);
            nodeMap[i, j].usedInFinal = true;
            //recursive call
            findEndOfMaze(i + 1 , j);
            return;
        }
        //right
        if (nodeMap[i, j].direction == (int)Directions.RIGHT)
            {

                print("i is: " + i + " j is:" + j + " right");
                bestRoute.Add(nodeMap[i, j].waypoint);
            nodeMap[i, j].usedInFinal = true;
            findEndOfMaze(i , j + 1);
                return;
        }
            //left
        if (nodeMap[i, j].direction == (int)Directions.LEFT)
            {
                print("i is: " + i + " j is:" + j + " left");
                bestRoute.Add(nodeMap[i, j].waypoint);
            nodeMap[i, j].usedInFinal = true;
            findEndOfMaze(i, j - 1);
                return;

        }
            //up
        if (nodeMap[i, j].direction == (int)Directions.UP)
            {
                print("i is: " + i + " j is:" + j + " up");
                bestRoute.Add(nodeMap[i, j].waypoint);
                nodeMap[i, j].usedInFinal = true;
                findEndOfMaze(i - 1, j);
                return;

        }
        //if it gets to here the path is blocked or there is an issue;
       
    }

 

}




/* LEGACY FIRST POINT CODE
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
               print("Out of Range error!");
               blockingError();

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

       if (bufferwaypoint != nodeMap[i, j].waypoint.transform)
       {
           bufferwaypoint = nodeMap[i, j].waypoint.transform;
           bufferwaypoint.Translate(translation);
       }
       bestRoute.Add(bufferwaypoint);


       bestRoute.Add(nodeMap[i,j].waypoint);
   }*/
