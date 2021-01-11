using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class PanelField : MonoBehaviour, IDropHandler
{
    int count = 0;
    const int ON = 1;
    const int OFF = 0;
    public enum CommandSignal{RED, BLUE, YELLOW, PURPLE, BROWN, GREEN, NOMAL}


    float[] PosX = {-104.0f, -52.0f, 0f, 52.0f, 104.0f};
    float[] PosY = 
        {95.0f, 95.0f, 95.0f, 95.0f, 95.0f,
        64.0f, 64.0f, 64.0f, 64.0f, 64.0f,
        33.0f, 33.0f, 33.0f, 33.0f, 33.0f,
        2.0f, 2.0f, 2.0f, 2.0f, 2.0f,
        -28.0f, -28.0f, -28.0f, -28.0f, -28.0f,
        -60.0f, -60.0f, -60.0f, -60.0f, -60.0f,
        -91.0f, -91.0f, -91.0f, -91.0f, -91.0f};

    public const int MAXC = 35;//最大コマンド数
    //public GameObject BlueComPrefab;

    int SetPanelType = default;

    public GameObject RedComPrefab;
    public GameObject BlueComPrefab;
    public GameObject YellowComPrefab;
    public GameObject PurpleComPrefab;
    public GameObject BrownComPrefab;
    public GameObject GreenComPrefab;

    public GameObject LeftData;

    GameObject BattleCommand;
    Transform GetParent;
    GameObject C;

    public GameObject PLr1;
    public GameObject PLr2;
    public GameObject PLr3;

    bool BattleStartFlag = default;

    GameObject PanData;

    Transform dataDrag;

    GameObject BattleSP;
    GameObject BattleC;

    CommandSignal q = CommandSignal.NOMAL;

    int DeleteFlag = OFF;

    //bool BattleEndFlag = default;

    GameObject ComD;

    public void Start()
    {     
        PanData = GameObject.Find("AllPanelField");
        BattleSP = GameObject.Find("SpeedGage");
        BattleC = GameObject.Find("BattleCounter");

        foreach (Transform child in gameObject.transform)
        {
            for(int i = 0; i < 23; i++)
            {
                if (child.name == "RedComPrefab" + i ||
                    child.name == "BlueComPrefab" + i ||
                    child.name == "YellowComPrefab" + i ||
                    child.name == "PurpleComPrefab" + i ||
                    child.name == "BrownComPrefab" + i ||
                    child.name == "GreenComPrefab" + i)
                {
                    GameObject.Destroy(child.gameObject);
                }
            }
        }

        BattleStartFlag = false;
        count = default;
    }

    void Update()
    {

    }

    public void OnDrop(PointerEventData data)
    {
        if (BattleStartFlag == false)
        {
            BattleStartFlag = true;
        }

        DeleteFlag = OFF;
        dataDrag = data.pointerDrag.transform;

        //Debug.Log(" ショリキターーーーーーーーーーーー 　" + data.pointerDrag.name + "回目です");
        //Debug.Log(PanData.GetComponent<PanelGenere>().DeleteFlagData() + gameObject.name + "  ② ");

        //(PanData.GetComponent<PanelGenere>().DeleteFlagData() == OFF)
        //{
        if (count < MAXC)
        {
            //Debug.Log(" 処理しますよーーーーーーーーーーーーーー　  ");
            CommandSignal a = SignalH(data);
            //Debug.Log(" ショリキターーーーーーーーーーーー 　" + count + "回目です");
            switch (a)
            {
                case CommandSignal.RED:
                    SetPanelType = (int)CommandSignal.RED + 1;
                    C = Instantiate(RedComPrefab) as GameObject;
                    C.name = "RedComPrefab" + count;
                    break;
                case CommandSignal.BLUE:
                    SetPanelType = (int)CommandSignal.BLUE + 1;
                    C = Instantiate(BlueComPrefab) as GameObject;
                    C.name = "BlueComPrefab" + count;
                    break;
                case CommandSignal.YELLOW:
                    SetPanelType = (int)CommandSignal.YELLOW + 1;
                    C = Instantiate(YellowComPrefab) as GameObject;
                    C.name = "YellowComPrefab" + count;
                    break;
                case CommandSignal.PURPLE:
                    SetPanelType = (int)CommandSignal.PURPLE + 1;
                    C = Instantiate(PurpleComPrefab) as GameObject;
                    C.name = "PurpleComPrefab" + count;
                    //Debug.Log("紫です！！！！！");
                    break;
                case CommandSignal.BROWN:
                    SetPanelType = (int)CommandSignal.BROWN + 1;
                    C = Instantiate(BrownComPrefab) as GameObject;
                    C.name = "BrownComPrefab" + count;
                    //Debug.Log("茶色です！！！！！");
                    break;
                case CommandSignal.GREEN:
                    SetPanelType = (int)CommandSignal.GREEN + 1;
                    C = Instantiate(GreenComPrefab) as GameObject;
                    C.name = "GreenComPrefab" + count;
                    break;
            }

            count = LocalP(C, count);

            DeleteFlag = ON;
            GameObject CC = GameObject.Find(dataDrag.name);
            int CommFlag = CC.GetComponent<BattleCommand>().SetEndFlag();

            //コマンドをプレイヤーに適用後に削除し、新規パネルを出す準備
            GetParent = CC.GetComponent<BattleCommand>().SetBeoforeParent();
            Destroy(CC.gameObject);

            if(gameObject.name == "MainPlayerLeft") PLr1.GetComponent<PlayerAttack>().Get_Set_Commnad();
            if (gameObject.name == "MainPlayerCenter") PLr2.GetComponent<PlayerAttack2>().Get_Set_Commnad();
            if (gameObject.name == "MainPlayerRight") PLr3.GetComponent<PlayerAttack3>().Get_Set_Commnad();

            SetPanelType = default;
        }
        //}

    }

    public int LocalP(GameObject i,int e)
    {
        //Debug.Log(i + "オブジェクトです");
        i.transform.localPosition = new Vector3(PosX[e%5], PosY[e], 0f);
        i.transform.SetParent(gameObject.transform, false);
        e++;
        //Debug.Log(e + "カウントです");
        return e;
    }

    public int DeleteFlagAnsow()
    {
        return DeleteFlag;
    }

    public GameObject SetMainObjectName()
    {
        return gameObject;
    }

    public Transform SetPanelObTra()
    {
        return GetParent;
    }

    /*public void DeleteObject()
    {
        CC = GameObject.Find(dataDrag.name);
        int CommFlag = CC.GetComponent<BattleCommand>().SetEndFlag();

        if (CommFlag == ON)
        {
            //コマンドをプレイヤーに適用後に削除し、新規パネルを出す準備
            GameObject CC = GameObject.Find(dataDrag.name);
            GetParent = CC.GetComponent<BattleCommand>().SetBeoforeParent();

            Debug.Log(dataDrag.name + "ドロップ情報を確認！！！！！");
            Debug.Log(GetParent + "親情報を確認！！！！！");
            Destroy(CC.gameObject);

            CommFlag = OFF;
        }
    }*/
    public CommandSignal SignalH(PointerEventData i)
    {
        //Debug.Log(i.pointerDrag.name + "名前ですよーーーーーーーーーーーーーーーーーー");

        if (i.pointerDrag.name.Contains("REDPrefab"))
        {
            q = CommandSignal.RED;
        }
        if (i.pointerDrag.name.Contains("BLUEPrefab"))
        {
            q = CommandSignal.BLUE;
        }
        if (i.pointerDrag.name.Contains("YELLOWPrefab"))
        {
            q = CommandSignal.YELLOW;
        }
        if (i.pointerDrag.name.Contains("PURPLEPrefab"))
        {
            q = CommandSignal.PURPLE;
        }
        if (i.pointerDrag.name.Contains("BROWNPrefab"))
        {
            q = CommandSignal.BROWN;
        }
        if (i.pointerDrag.name.Contains("GREENPrefab"))
        {
            q = CommandSignal.GREEN;
        }

        //Debug.Log(q + "カウントです");

        return q;
    }

    public int SetCount()
    {
        return count;
    }

    public bool SetBattleStartFlag()
    {
        return BattleStartFlag;
    }

    public int SetPanelData()
    {
        return SetPanelType;
    }

}