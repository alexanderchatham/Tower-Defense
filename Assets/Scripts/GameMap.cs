using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//struct for game map
struct nodeMem
{
    //has tower will be used in 
    public bool hasTower;
    public GameObject selectedNode;
}

public class GameMap : MonoBehaviour {

    BuildManager buildManager;
    public GameObject nodeBlueprint;
    private int mapWidth = 20;
    private int mapHeight = 70;
    //waypoints 
    public GameObject Parent;
    public GameObject End;
    public GameObject[] BestRoute;
    public Turret turret;

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

        buildManager = BuildManager.instance;
        //initializing game map
        nodeMem[,] nodeMap = new nodeMem[mapHeight,mapWidth];

        //populate Gamemap
        int i = 0;
        int j = 0;

        for (i = 0; i < mapHeight; i++)
        {

            for (j = 0; j < mapWidth; j++) {

                nodeMap[i,j].hasTower = false;
                nodeMap[i,j].selectedNode = Instantiate(nodeBlueprint);
                nodeMap[i,j].selectedNode.transform.position = new Vector3(j * .5f - 4.75f, -.45f, 98f - i * .5f);
            }
        }
        StartCoroutine("findBestRoute",nodeMap);
    }

    private void Update()
    {
        
    }

    private void findBestRoute(nodeMem[,] nodeMap)
    {

        int i = 0;
        int j = 0;


        // update the hastower bools
        for (i = 0; i < mapHeight; i++)
        {

            for (j = 0; j < mapWidth; j++)
            {
                turret = null;
                turret = nodeMap[i, j].selectedNode.GetComponentInChildren(typeof(Turret)) as Turret;
                if (turret)
                {
                    nodeMap[i, j].hasTower = true;
                }
            }
        }
        //To start find an empty box at the top
        i = mapWidth / 2;
        while (nodeMap[0, i].hasTower)
        {
           if(i  <  mapWidth)
                i ++;
           if(i == mapWidth)
                i = 0;
        }
    }

}
