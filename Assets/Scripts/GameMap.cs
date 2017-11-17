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
        
        int i = 0;
        int j = 0;
        Transform newWaypoint = (Transform)Instantiate(End);
        // update the hastower bools
        for (i = 0; i < mapHeight; i++)
        {

            for (j = 0; j < mapWidth; j++)
            {
                Node node = nodeMap[i, j].script;
                if (node.turret)
                {
                    nodeMap[i, j].hasTower = true;
                    print("node i:" + i + " j:" + j + " has a turret!");
                }
            }
        }
        //To start find an empty box at the top, not a very efficient way but should work.
        j = 0;
        i = 0;
        print("i is: " + i + "\nj is:" + j);
        newWaypoint = Instantiate(End);
        Vector3 offsetPosition = new Vector3(nodeMap[i, j].selectedNode.transform.position.x, nodeMap[i, j].selectedNode.transform.position.y +1f, nodeMap[i, j].selectedNode.transform.position.z );
        newWaypoint.position = offsetPosition;
        bestRoute.Add(newWaypoint);
        bestRoute.Add(End);
        Waypoints instance = Waypoints.instance;
        instance.setPoints(bestRoute);
        /*
        newWaypoint.SetParent(Parent, true);
        newWaypoint.SetAsFirstSibling();
        End.SetAsLastSibling();
        */
    }

}
