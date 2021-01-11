using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class PlayerAttack : MonoBehaviour
{
    public GameObject Time_Data;
    public GameObject PL;
    Transform ChildObject;
    Animator main;
    int deffo = default;

    //0：赤　1：青　2：黄　3：紫　4：茶　5：緑　6:例外
    //ノーマルのため、処理し重複する
    //特殊のため、処理し重複しない、要素の6つ目は属性の数
    static int[,] Com_Normal = new int[,]
    {
        { 0, 1, 6, 6, 6, 2},//配列属性数が　6番目（固定）に保存されているため、注意！
        { 0, 0, 0, 1, 6, 2},//配列属性数が　6番目（固定）に保存されているため、注意！
        { 0, 0, 1, 1, 5, 3},//配列属性数が　6番目（固定）に保存されているため、注意！
    };

    //各スキルの属性の数を保持する。
    static int[,] Command_Skill_Count = new int[3, 6];
    //処理を1度のみのため、必須確認
    static bool Command_Sett_Flag;
    //入力した属性するを保持する。Command_Setの情報を属性するに置き換え保管する。
    static int[] Type_Count = new int[6];
    //ノーマル攻撃回数を保管する変数
    static int[] kk = new int[6];
    //ノーマルの情報で0以上かどうかの確認
    static bool[] kkflag = new bool[6];

    static public int anime_main = default;
    static public int anime_move = default;

    //bool FF = false;
    static bool move_Flag = default;
    int AC = default;
    const int no_animation = 0;

    // Start is called before the first frame update
    void Start()
    {
        anime_main = default;
        anime_move = default;
        //AC = default;
        deffo = default;

        move_Flag = false;

        //FF = false;

        for (int i = 0; i < 6; i++)
        {
            Type_Count[i] = default;
            kkflag[i] = default;
            kk[i] = default;

            for (int j = 0; j < 3; j++)
            {
                Command_Skill_Count[j, i] = default;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

        Get_Set_Commnad();

        if (Time_Data.GetComponent<BattleCounter>().CheckTime() <= 0)
        {
            Normal_Command();
            Check_Com();
        }

        if(anime_main > no_animation && move_Flag == true)
        {
            //Debug.Log(anime_main + "コンボだ！！！");
            
            animete_Con(anime_main);
        }

        // 再生中のクリップ名
        string ACName = AnimationCheak();
        Debug.Log(ACName + " 状態です！！");
        Debug.Log(AC + "アニメカウントです");

        if (ACName == "animetion_Deffo" && AC > 0)
        {
            this.gameObject.transform.localPosition = new Vector3(0.8f, 0.09f, 0.35f);
            AC = default;
        }
    }


    //入力したコマンドの属性の各数を取得する
    public void Get_Set_Commnad()
    {

        int GetData = PL.GetComponent<PanelField>().SetPanelData() - 1;

        if (GetData >= 0) {
            Type_Count[GetData]++;
        }
    }

    //キャラが保持するコマンドの属性を取得
    static void Normal_Command()
    {
        //0:ノーマル 1:スキ１　2：スキ２
        for (int i = 0; i < 3; i++)
        {
            //各最大5コマンド分取得! 6番目の要素は属性の種類のため絶対取らない
            //コマンド入力よりいくつ属性分あるかを見る。
            for (int j = 0; j < 5; j++)
            {
                int k = Com_Normal[i, j];
                if (k != 6) Command_Skill_Count[i, k]++;
                //switch (k)で6属性分処理していた!!無駄処理です。馬鹿です。
            }
        }
    }

    //各スキルが使用可能か処理をする
    static void Check_Com()
    {
        int[] level = new int[3];
        bool[] Flag = new bool[3];

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                //各スキルの属性の数　＜＝　入力した属性の数　が条件
                if (Command_Skill_Count[i, j] <= Type_Count[j] &&
                    Command_Skill_Count[i, j] > 0 && Type_Count[j] > 0)
                {
                    level[i]++;
                }
            }
            if (level[i] == Com_Normal[i, 5])
            {
                Flag[i] = true;

                //2回攻撃の場合は1回攻撃を2回表示している　＊＊ノーマル攻撃回数分処理するため、
                if (i == 0)
                {
                    //条件が合えば何度でも
                    //int kaisuu = Normal_Check_Count(i, Flag[i]);

                    if (anime_main == default )
                    {
                        anime_main = Normal_Check_Count(i, Flag[i]);
                        move_Flag = true;

                        Debug.Log(anime_main + "     " );
                    }

                    /*while (kaisuu > 0)
                    {
                        Debug.Log("player1通常攻撃" + kaisuu + "回できる");
                        kaisuu--;
                    }*/
                }

                if (i == 1) Debug.Log("player1特殊スキル　１　発動可能！");//毎時１回のみ
                if (i == 2) Debug.Log("player1特殊スキル　２　発動可能！！");//毎時１回のみ
            }
        }
    }

    //通常攻撃の場合条件満たせば何回でも使用可能なため、最大数の計算
    static int Normal_Check_Count(int i, bool j)
    {
        //int Normal_Attack_Cnt = 0;　//ノーマル攻撃の回数を返す。
        int hikaku1 = 0, hikaku2 = 0;　//比較するための2つの変数であり最後に返り値
        int l = 0; //ただの処理用

        if (j == true)
        {
            //ノーマル用の計算　可能回数
            for (int k = 0; k < 6; k++)
            {
                if (Command_Skill_Count[0, k] > 0 && Type_Count[k] > 0)
                {
                    kk[k] = Type_Count[k] / Command_Skill_Count[0, k];
                }
            }
            //6属性分の回数計算(内部で比較処理がある)
            while (l < 6)
            {
                //0以上かを必ず確認。0以上の場合のみ計算対象にする
                if (kk[l] > 0)
                {
                    if (hikaku1 == 0) hikaku1 = kk[l];
                    if (hikaku2 == 0) hikaku2 = kk[l];

                    //比較対象処理し、大きい数字側の変数を初期化(0)にする。
                    if (hikaku1 != 0 && hikaku2 != 0)
                    {
                        if (hikaku1 < hikaku2)
                        {
                            //Console.WriteLine(hikaku1 + "  " + hikaku2);
                            hikaku2 = default;
                        }
                        else
                        {
                            //Console.WriteLine(hikaku1 + "  " + hikaku2);
                            hikaku1 = default;
                        }
                    }
                    l++;
                }
                else
                {
                    if (hikaku1 == 0 || hikaku2 == 0) l++;
                }
                //Console.WriteLine(l + "回");
            }
        }
        //比較処理で最少数の数字を広い、返す
        if (hikaku1 > 0) return hikaku1;
        else return hikaku2;
    }

    public void animete_Con(int move_count)
    {
        foreach (Transform child in transform)
        {
            ChildObject = child.transform;
        }

        GameObject ChildGo = ChildObject.gameObject;

        main = ChildGo.GetComponent<Animator>();
        AnimatorStateInfo Ani_State = main.GetCurrentAnimatorStateInfo(0);

        if (AC < move_count)
        {
            this.gameObject.transform.localPosition = new Vector3(0.1f, 0.09f, 0.0f);

            AC = ChildGo.GetComponent<Animation_Move_Count>().Get_Anime_C();

            if (AC > 0 &&
                AC < move_count)
            {
                int iii = (AC + 1) % 2;
                if (iii == 0) deffo = 1;
                else deffo = 2;
            }

            switch (deffo)
            {
                case 0: //1
                    main.SetTrigger("attack1");
                    //AC = ChildGo.GetComponent<Animation_Move_Count>().Get_Anime_C();
                    //Debug.Log("処理しました");
                    //main.Play("attack1");
                    break;
                case 1: //2
                    main.SetTrigger("attack2");

                    //main.Play("attack2");
                    break;
                case 2: //3
                    main.SetTrigger("attack3");
                    //main.Play("attack3");
                    break;
            }
        }

        //string clipName = clipInfo[0].clip.name;
        //AC = ChildGo.GetComponent<Animation_Move_Count>().Get_Anime_C();

        //処理が終わったら通常に戻る
        if (ChildGo.GetComponent<Animation_Move_Count>().Get_Anime_C() == move_count)
        {
            main.SetTrigger("deff");
            //AC = default;
            Start();
            PL.GetComponent<PanelField>().Start();
            ChildGo.GetComponent<Animation_Move_Count>().Start();
        }
    }

    public string AnimationCheak()
    {
        foreach (Transform child in transform)
        {
            ChildObject = child.transform;
        }

        GameObject ChildGo = ChildObject.gameObject;
        main = ChildGo.GetComponent<Animator>();

        // アニメーションの情報取得
        AnimatorClipInfo[] clipInfo = main.GetCurrentAnimatorClipInfo(0);
        string clipName = clipInfo[0].clip.name;
        return clipName;
    }
}