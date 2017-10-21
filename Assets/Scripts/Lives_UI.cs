using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lives_UI : MonoBehaviour {


    public Text livesText;

    // Update is called once per frame
    void Update()
    {
        livesText.text = "   Lives:" + PlayerStats.Lives.ToString();
    }
}
