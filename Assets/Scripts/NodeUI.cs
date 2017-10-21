using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour {

    private SellUpgrade target;

    public GameObject ui;
    public Text Leveltext;
    public Text Upgradetext;

    public void SetTarget(SellUpgrade _target)
    {
        target = _target;

        if (transform.position != target.transform.position)
        {
            transform.position = target.transform.position;
            Leveltext.text = "Level: " + target.turret.upgrades;
            Upgradetext.text = "UPGRADE $" + (100 + target.turret.upgrades * 50);
            ui.SetActive(true);
            return;

        }
        if (!ui.activeSelf)
            ui.SetActive(true);
        else
            ui.SetActive(false);
    }

    public void hide()
    {
        ui.SetActive(false);
    }

    public void upgrade()
    {
        target.upgrade();
        Leveltext.text = "Level: " + target.turret.upgrades;
        Upgradetext.text = "UPGRADE $" + (100 + target.turret.upgrades * 50);
    }

    public void sell()
    {
        target.sell();
        hide();
    }
}
