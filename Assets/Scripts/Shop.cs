using UnityEngine;

public class Shop : MonoBehaviour {

    public TurretBlueprint standardTurret;
    public TurretBlueprint missileLauncher;
    public TurretBlueprint laserBeamer;
    public TurretBlueprint upgradedTower;
    public TurretBlueprint basicTower;

    BuildManager buildManager;

    private void Start()
    {
        buildManager = BuildManager.instance;
    }

    public void SelectStandardTurret ()
    {
        Debug.Log("Standard Turret Selected");
        buildManager.SelectTurretToBuild(standardTurret);
    }

    public void SelectUpgradedTower()
    {
        Debug.Log("Upgraded Tower Selected");
        buildManager.SelectTurretToBuild(upgradedTower);
    }

    public void SelectBasicTower()
    {
        Debug.Log("Basic Tower Selected");
        buildManager.SelectTurretToBuild(basicTower);
    }

    public void SelectMissleTurret()
    {
        Debug.Log("Missle Turret Selected");
        buildManager.SelectTurretToBuild(missileLauncher);
    }

    public void SelectLaserBeamer()
    {
        Debug.Log("Laser Beamer Selected");
        buildManager.SelectTurretToBuild(laserBeamer);
    }
}
