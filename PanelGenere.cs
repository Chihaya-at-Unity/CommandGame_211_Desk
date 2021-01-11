using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEditor;
using UnityEngine;

public class PanelGenere : MonoBehaviour
{
    float[] PanelPosX = { -132.0f, -66.0f, 0f, 66.0f, 132.0f };

    public GameObject REDPrefab;
    public GameObject BLUEPrefab;
    public GameObject YELLOWPrefab;
    public GameObject PURPLEPrefab;
    public GameObject BROWNPrefab;
    public GameObject GREENPrefab;

    //Transform PanelParent;
    int PrefabCount = 0;

    Transform GetParent;

    GameObject GetMainObject;

    GameObject PanelObject;

    const int ON = 1;
    const int OFF = 0;

    // PanelType
    List<GameObject> MainPlayerlist = new List<GameObject>();
    int RereceFlag = OFF;

    GameObject[] PanelField;

    const int PanelMIN = 0;　//存在しない
    const int PanelMAX = 1;　//存在する

    //GameObject PanelHandType;

    Transform GetParentPanel;

    //GameObject[] PanelTagObject;
    // Start is called before the first frame update
    void Start()
    {
        //PanelTagObject = GameObject.FindGameObjectsWithTag("PanelPosition");

        //このプログラムを適用している子オブジェクトの状態を確認する。
        foreach(Transform child in transform)
        {
            //Debug.Log(child.transform + "  子オブジェクトの数は  " + child.transform.childCount);
            //ALLPanel配下の子オブジェクトにパネルがなければ生成処理を実施する
            if (PanelMIN >= child.transform.childCount)
            {
                //Debug.Log("①①①①①①①①" + PanelTagObject[i].transform + " が親になりますよ！！   " + PanelTagObject[i].gameObject);
                //各新規パネルをALLPanel配下の子オブジェクト内に生成処理する
                PanelRerece(child.transform, child.gameObject);
            }
        }

        //プレイヤー情報を生成する
        MainPlayerlist.Add(GameObject.Find("MainPlayerLeft"));
        MainPlayerlist.Add(GameObject.Find("MainPlayerCenter"));
        MainPlayerlist.Add(GameObject.Find("MainPlayerRight"));
    }

    void Update()
    {
        //Debug.Log(PanelType.GetComponent<PanelField>().DeleteFlagAnsow() + "ですよ！");
        //PanelRerece();

        for(int w = 0; w < 3; )
        {
            GetMainObject = MainPlayerlist[w].GetComponent<PanelField>().SetMainObjectName();
            //GetParentPanel = GetMainObject.GetComponent<PanelField>().SetPanelObTra();

            if (GetMainObject.GetComponent<PanelField>().DeleteFlagAnsow() == ON)
            {
                RereceFlag = ON;
            }

            if (RereceFlag == ON)
            {
                foreach (Transform child in transform)
                {
                    if (PanelMIN == child.transform.childCount)
                    {
                        //Debug.Log("②②②②②②②②②" + child.transform + " が親になりますよ！！   "+ child.gameObject);
                        Transform PanelParent = child.transform;
                        //GetParentPanel = GetMainObject.GetComponent<PanelField>().SetPanelObTra();
                        PanelRerece(PanelParent, child.gameObject);
                        break;
                    }
                }
            }
            w++;
            RereceFlag = OFF;
        }
    }
    // Update is called once per frame

    //新規パネル生成処理 親情報　
    void PanelRerece(Transform y, GameObject PanelType)
    {
        int TypeSelect = -1;
        int random_number = 2;
        //パネル枚数のランダムで選ぶ　基本は６
        TypeSelect = Random.Range(0, 60);
        TypeSelect = (TypeSelect + random_number) % random_number;

        //Debug.Log(y + "  子オブジェクトの数");
        switch (TypeSelect)
        {
            case 0:
                PanelType = Instantiate(REDPrefab) as GameObject;
                PanelType.name = "REDPrefab" + PrefabCount;
                break;
            case 1:
                PanelType = Instantiate(BLUEPrefab) as GameObject;
                PanelType.name = "BLUEPrefab" + PrefabCount;
                break;
            case 2:
                PanelType = Instantiate(YELLOWPrefab) as GameObject;
                PanelType.name = "YELLOWPrefab" + PrefabCount;
                break;
            case 3:
                PanelType = Instantiate(PURPLEPrefab) as GameObject;
                PanelType.name = "PURPLEPrefab" + PrefabCount;
                break;
            case 4:
                PanelType = Instantiate(BROWNPrefab) as GameObject;
                PanelType.name = "BROWNPrefab" + PrefabCount;
                break;
            case 5:
                PanelType = Instantiate(GREENPrefab) as GameObject;
                PanelType.name = "GREENPrefab" + PrefabCount;
                break;
        }
        PanelType.transform.SetParent(y, false);

        PrefabCount++;
    }
    public int DeleteFlagData()
    {
        return RereceFlag;
    }
}
