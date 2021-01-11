using System.Collections;
using System.Collections.Generic;
using System.Net.Cache;
using UnityEngine;
using UnityEngine.UI;

public class BattleSPeed : MonoBehaviour
{
    public GameObject PlayerSpeed;
    public GameObject BT;

    const float Max_Gage = 1.0f;

    public float PlayerSpeedAverage = 0.01f;

    bool FFlag = default;

    // Start is called before the first frame update
    void Start()
    {
        //this.PlayerSpeed = GameObject.Find("Speed");
    }

    // Update is called once per frame
    void Update()
    {
        //スピードゲージの状態を確認する
        if (this.PlayerSpeed.GetComponent<Image>().fillAmount < Max_Gage)
        {
            this.PlayerSpeed.GetComponent<Image>().fillAmount += PlayerSpeedAverage * Time.deltaTime;
            FFlag = false;
        }

        if (this.PlayerSpeed.GetComponent<Image>().fillAmount >= Max_Gage)
        {

            gameObject.transform.localPosition = new Vector3(0, 500.0f, 0);
            BT.transform.localPosition = new Vector3(0, -100.0f, 0);
            FFlag = true;
        }
        else
        {
            gameObject.transform.localPosition = new Vector3(0, -258.0f, 0);
            BT.transform.localPosition = new Vector3(0, -460.0f, 0);
        }
    }

    public bool FillFlag()
    {
        return FFlag;
    }
}
