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

        for (i = 0; i < mapWidth; i++)
        {

            for (j = 0; j < mapHeight; j++) {

                nodeMap[j,i].hasTower = false;
                nodeMap[j,i].selectedNode = Instantiate(nodeBlueprint);
                nodeMap[j,i].selectedNode.transform.position = new Vector3(i * .5f - 4.75f, -.45f, 98f - j * .5f);
            }
        }

    }

    
}
