using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class DestoryPanel : MonoBehaviour, IDropHandler
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    public void OnDrop(PointerEventData data)
    {
        //if (BattleStartFlag == 0) BattleStartFlag = 1;

        GameObject CC = GameObject.Find(data.pointerDrag.name);
        int CommFlag = CC.GetComponent<BattleCommand>().SetEndFlag();

        //コマンドをプレイヤーに適用後に削除し、新規パネルを出す準備
        Destroy(CC.gameObject);
        //}

    }
}
