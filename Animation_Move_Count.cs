using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation_Move_Count : MonoBehaviour
{
    //プレイヤーのコンボ数を保存する
    private int Anime_Count;
    public void Start()
    {
        Anime_Count = default;
    }

    private void Update()
    {
    }

    //アニメーションを通常攻撃をコマンド回数分処理するため
    public void anime_movecount()
    {
        //呼び出されたときにコンボ数をプラスする
        Anime_Count++;
    }

    //現在のコンボ数を戻り値として返す。
    public int Get_Anime_C()
    {
        return Anime_Count;
    }
}
