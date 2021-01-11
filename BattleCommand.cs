using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BattleCommand : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public PanelField PanelField;
    Vector3 BeforePos;
    GameObject Btime;

    int OnEndFlag = 0;

    Transform SetParent;

    void Start()
    {
        //コマンドバトルができる状態を管理するゲームオブジェクト
        Btime = GameObject.Find("BattleCounter");
    }
    
    public Transform SetBeoforeParent()
    {
        return SetParent;
    }

    public int SetEndFlag()
    {
        return OnEndFlag;
    }

    void Update()
    {
        //バトル時間の状態をチェックする
        //バトル時間外になるとパネルを触れなくする
        if(Btime.GetComponent<BattleCounter>().CheckTime() <= 0)     GetComponent<Image>().raycastTarget = false;
        else GetComponent<Image>().raycastTarget = true;

    }
    public void OnBeginDrag(PointerEventData PanelObject)
    {   
        BeforePos = transform.localPosition;
        SetParent = transform.parent;
        //手札パネルの元の親情報
        //Debug.Log(SetParent + "  ①  " + transform.parent);

        GetComponent<CanvasGroup>().blocksRaycasts = false;
        //transform.SetParent(transform.parent);
    }
    public void OnDrag(PointerEventData PanelObject)
    {
        transform.position = PanelObject.position;
    }

    public void  OnEndDrag(PointerEventData PanelObject)
    {
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        //transform.SetParent(SetParent);
        transform.localPosition = BeforePos;
        //Debug.Log(SetParent + "  ①''''''''''''''''''''''''  ");
    }
}
