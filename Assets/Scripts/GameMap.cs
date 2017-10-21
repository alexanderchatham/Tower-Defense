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
    // Use this for initialization
    void Start () {

        buildManager = BuildManager.instance;
        //initializing game map
        nodeMem[] nodeMap = new nodeMem[320];

        //populate Gamemap
        int i = 0;
        float x = 0;
        float z = 0;

        while (i < nodeMap.Length)
        {

            x = i % 16;

            if (i >= (16 + 16 * z))
                z++;

            nodeMap[i].hasTower = false;
            nodeMap[i].selectedNode = Instantiate(nodeBlueprint);
            nodeMap[i].selectedNode.transform.position = new Vector3(x*.5f-4f,-.45f,90f-z*.5f);
            i++;
        }

    }

    
}
