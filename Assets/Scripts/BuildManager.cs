using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BuildManager : MonoBehaviour
{

    public static BuildManager instance;
    public NodeUI nodeUI;

    public GameObject nodeBlueprint;
    public GameMap gameMap;

    void Awake ()
    {
        if (instance != null)
        {
            Debug.LogError("More than one BuildManager in scene!");
            return;
        }

        instance = this;
    }

    private void Start()
    {
        gameMap = GameMap.instance;
    }

    public GameObject buildEffect;


    public bool CanBuild { get { return turretToBuild != null; } }
    public bool HasMoney { get { return PlayerStats.Money >= turretToBuild.cost; } }

    public void BuildTurretOn(Node node)
    {
        if (PlayerStats.Money < turretToBuild.cost)
        {
            Debug.Log("Not enough money to build that!");
            return;
        }
        if (turretToBuild.prefab != null)
        {
            PlayerStats.Money -= turretToBuild.cost;
            GameObject turret = (GameObject)Instantiate(turretToBuild.prefab, node.GetBuildPosition(), Quaternion.identity);
            node.turret = turret;
            

            GameObject effect = (GameObject)Instantiate(buildEffect, node.GetBuildPosition(), Quaternion.identity);
            Destroy(effect, 5f);

            Debug.Log("Turret built! Money left: " + PlayerStats.Money);
            gameMap.findBestRoute();
        }
    }

    public void SelectTurret (SellUpgrade turret)
    {
        turretToBuild = null;
        nodeUI.SetTarget(turret);
    }

    public void DeselectNode()
    {
        nodeUI.hide();
    }

    public TurretBlueprint turretToBuild;

    public void SelectTurretToBuild (TurretBlueprint turret)
    {
        turretToBuild = turret;
        DeselectNode();
    }
}
