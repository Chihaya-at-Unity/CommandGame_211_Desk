using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class PlayerAttack2 : MonoBehaviour
{

    public GameObject Time_Data;
    public GameObject PL;
    //0：赤　1：青　2：黄　3：紫　4：茶　5：緑　6:例外
    public static int[] Command_Set = { 0, 1, 0, 1, 3, 0, 5, 0, 0, 4, 4, 2, 2, 4, 4 };
    //ノーマルのため、処理し重複する
    //特殊のため、処理し重複しない、要素の6つ目は属性の数
    static int[,] Com_Normal = new int[,]
    {
        { 0, 0, 6, 6, 6, 1},//配列属性数が　6番目（固定）に保存されているため、注意！
        { 0, 0, 4, 4, 6, 2},//配列属性数が　6番目（固定）に保存されているため、注意！
        { 0, 0, 0, 4, 5, 3},//配列属性数が　6番目（固定）に保存されているため、注意！
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
    // Start is called before the first frame update
    void Start()
    {

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
    }


    //入力したコマンドの属性の各数を取得する
    public void Get_Set_Commnad()
    {
        int GetData = PL.GetComponent<PanelField>().SetPanelData();
        int AA = GetData - 1;

        if (AA > -1)
        {
            Type_Count[AA]++;

            /*switch (GetData)
            {
                case 1:
                    Debug.Log("player2赤は" + Type_Count[AA]);
                    break;
                case 2:
                    Debug.Log("player2青は" + Type_Count[AA]);
                    break;
                case 3:
                    Debug.Log("player2黄は" + Type_Count[AA]);
                    break;
                case 4:
                    Debug.Log("player2紫は" + Type_Count[AA]);
                    break;
                case 5:
                    Debug.Log("player2茶は" + Type_Count[AA]);
                    break;
                case 6:
                    Debug.Log("player2緑は" + Type_Count[AA]);
                    break;
            }*/
        }
        //switch (Check)　←ここも無駄処理があった。馬鹿だな！！
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

                if (i == 0)
                {
                    //条件が合えば何度でも
                    int kaisuu = Normal_Check_Count(i, Flag[i]);

                    while (kaisuu > 0)
                    {
                        Debug.Log("player2通常攻撃" + kaisuu + "回できる");
                        kaisuu--;
                    }
                }

                if (i == 1) Debug.Log("player2特殊スキル　１　発動可能！");//毎時１回のみ
                if (i == 2) Debug.Log("player2特殊スキル　２　発動可能！！");//毎時１回のみ
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
}