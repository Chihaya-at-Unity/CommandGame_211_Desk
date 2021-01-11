using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleTimeDisplay : MonoBehaviour
{
    public GameObject BTDisplay;
    public Vector3 V3LocalPosition = default;
    bool strtFlag = default;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        bool DDTime = BTDisplay.GetComponent<BattleCounter>().SetFlagA();

        if(DDTime == true && strtFlag == false)
        {
            strtFlag = true;
        }
        else if(DDTime == false && strtFlag == true)
        {
            strtFlag = false;
        }

        DisplayPosition();
    }

    void DisplayPosition()
    {
        if (strtFlag == true)  gameObject.transform.localPosition = new Vector3(0, 220.0f, 0);
        else gameObject.transform.localPosition = new Vector3(0, 400.0f, 0);
    }
}
