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
}

public class GameMap : MonoBehaviour {

    BuildManager buildManager;
    public GameObject nodeBlueprint;
    private int mapWidth = 20;
    private int mapHeight = 70;
    //waypoints 
    public Transform Parent;
    public Transform End;
    public Turret turret;
    private nodeMem[,] nodeMap;
    public ArrayList bestRoute;

    public static GameMap instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one GameMap in scene!");
            return;
        }

        instance = this;
    }

    // Use this for initialization
    void Start () {

        bestRoute = new ArrayList();

        buildManager = BuildManager.instance;
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
                nodeMap[i,j].selectedNode.transform.position = new Vector3(j * .5f - 4.75f, -.45f, 98f - i * .5f);
            }
        }
    }
    

    public void findBestRoute()
    {
        bestRoute = new ArrayList();
        int i;
        int j;
        Transform newWaypoint = (Transform)Instantiate(End);
        // update the hastower bools
        for (i = 0; i < mapHeight; i++)
        {

            for (j = 0; j < mapWidth; j++)
            {
               
                if (nodeMap[i, j].script.turret != null)
                {
                    nodeMap[i, j].hasTower = true;
                    print("node i:" + i + " j:" + j + " has a turret!");
                }
            }
        }
        //get the first point in which the enemy will enter the maze
        firstPoint();
       



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
        int j = mapWidth / 2;
        int mag = 1;
        while (nodeMap[i, j].hasTower)
        {
            if (nodeMap[i, j + mag].hasTower == false)
            {
                j = j + mag;
            }
            if (nodeMap[i, j - mag].hasTower == false)
            {
                j = j - mag;
            }
            else
                mag++;
        }

        print("i is: " + i + "\nj is:" + j);
        Transform newWaypoint = (Transform)Instantiate(End);
        Vector3 offsetPosition = new Vector3(nodeMap[i, j].selectedNode.transform.position.x, nodeMap[i, j].selectedNode.transform.position.y + 1f, nodeMap[i, j].selectedNode.transform.position.z);
        newWaypoint.position = offsetPosition;
        bestRoute.Add(newWaypoint);
    }
}
