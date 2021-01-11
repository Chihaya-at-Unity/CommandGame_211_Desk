using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleCounter : MonoBehaviour
{
    GameObject Bcounter;
    bool SetFlag = false;
    //bool MoveCheck = false;

    const float Base_BattoleTime = 10.0f;
    float CheckBattleTime = Base_BattoleTime;

    //bool CheckFlag = default;

    public GameObject BtSpeed;
    public GameObject BtSpeedFill;

    public PanelField PanelLeft;
    public PanelField PanelCenter;
    public PanelField PanelRight;

    public float Ready_Time = default;
    public bool Ready_Time_Fla = true;

    // Start is called before the first frame update
    void Start()
    {
        Bcounter = GameObject.Find("BattleTime");
    }

    // Update is called once per frame
    void Update()
    {
        if ((true == PanelLeft.GetComponent<PanelField>().SetBattleStartFlag() ||
            true == PanelCenter.GetComponent<PanelField>().SetBattleStartFlag() ||
            true == PanelRight.GetComponent<PanelField>().SetBattleStartFlag()) &&
            SetFlag == false)
        {
            SetFlag = true;
            Ready_Time_Fla = false;
        }
        if(SetFlag == true)    TimeData();

        if (Ready_Time > 0)
        {
            Ready_Time -= 1.0f * Time.deltaTime;

            if (Ready_Time <= 0)
            {
                BtSpeedFill.GetComponent<Image>().fillAmount = 0.0f;
                this.CheckBattleTime = Base_BattoleTime;
                Ready_Time = default;
                Ready_Time_Fla = true;
                SetFlag = false;
            }
        }
    }

    public void TimeData()
    {
        this.CheckBattleTime -= 1.0f * Time.deltaTime;

        if (CheckBattleTime > 0)
        {
            this.Bcounter.GetComponent<Text>().fontSize = 170;
            this.Bcounter.GetComponent<Text>().text = this.CheckBattleTime.ToString("F0");
        }
        else if (CheckBattleTime <= 0 && SetFlag == true)
        {
            this.CheckBattleTime = default;

            this.Bcounter.GetComponent<Text>().fontSize = 80;
            this.Bcounter.GetComponent<Text>().text = this.CheckBattleTime.ToString("Ready!");

            BtSpeedFill.GetComponent<Image>().fillAmount = 0.0f;

            if (Ready_Time == default) Ready_Time = 5.0f;
        }
    }

    public bool Ready_Time_Flag()
    {
        return Ready_Time_Fla;
    }
    public float CheckTime()
    {
        return this.CheckBattleTime;
    }
    public bool SetFlagA()
    {
        return SetFlag;
    }
}
