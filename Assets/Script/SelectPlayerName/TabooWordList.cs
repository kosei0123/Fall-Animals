using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabooWordList : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool TabooWord(string playerName)
    {
        //あ行
        if (playerName.Contains("青姦")) return true;
        if (playerName.Contains("アヘ")) return true;
        if (playerName.Contains("あなる")) return true;
        if (playerName.Contains("アナル")) return true;
        if (playerName.Contains("あほ")) return true;
        if (playerName.Contains("アホ")) return true;
        if (playerName.Contains("イク")) return true;
        if (playerName.Contains("淫")) return true;
        if (playerName.Contains("うんこ")) return true;
        if (playerName.Contains("ウンコ")) return true;
        if (playerName.Contains("穢多")) return true;
        if (playerName.Contains("えろ")) return true;
        if (playerName.Contains("エロ")) return true;
        if (playerName.Contains("援助")) return true;
        if (playerName.Contains("おっぱい")) return true;
        if (playerName.Contains("オッパイ")) return true;
        if (playerName.Contains("オナニー")) return true;

        //か行
        if (playerName.Contains("ガキ")) return true;
        if (playerName.Contains("餓鬼")) return true;
        if (playerName.Contains("カス")) return true;
        if (playerName.Contains("かたわ")) return true;
        if (playerName.Contains("皮被り")) return true;
        if (playerName.Contains("看護婦")) return true;
        if (playerName.Contains("姦通")) return true;
        if (playerName.Contains("騎乗位")) return true;
        if (playerName.Contains("きっしょ")) return true;
        if (playerName.Contains("クズ")) return true;
        if (playerName.Contains("糞")) return true;
        if (playerName.Contains("クソ")) return true;
        if (playerName.Contains("くりとりす")) return true;
        if (playerName.Contains("クリトリス")) return true;
        if (playerName.Contains("クロ")) return true;
        if (playerName.Contains("黒んぼ")) return true;
        if (playerName.Contains("くんに")) return true;
        if (playerName.Contains("クンニ")) return true;

        //さ行
        if (playerName.Contains("雑魚")) return true;
        if (playerName.Contains("ざこ")) return true;
        if (playerName.Contains("ザコ")) return true;
        if (playerName.Contains("殺")) return true;
        if (playerName.Contains("死")) return true;
        if (playerName.Contains("潮吹き")) return true;
        if (playerName.Contains("しね")) return true;
        if (playerName.Contains("シネ")) return true;
        if (playerName.Contains("氏ね")) return true;
        if (playerName.Contains("市ね")) return true;
        if (playerName.Contains("射精")) return true;
        if (playerName.Contains("障害")) return true;
        if (playerName.Contains("尻軽")) return true;
        if (playerName.Contains("スカトロ")) return true;
        if (playerName.Contains("スラム街")) return true;
        if (playerName.Contains("性行為")) return true;
        if (playerName.Contains("正常位")) return true;
        if (playerName.Contains("せっくす")) return true;
        if (playerName.Contains("セックス")) return true;
        if (playerName.Contains("SEX")) return true;
        if (playerName.Contains("sex")) return true;
        if (playerName.Contains("せふれ")) return true;
        if (playerName.Contains("セフレ")) return true;
        if (playerName.Contains("ソープ")) return true;

        //た行
        if (playerName.Contains("タヒ")) return true;
        if (playerName.Contains("乳")) return true;
        if (playerName.Contains("ちんこ")) return true;
        if (playerName.Contains("チンコ")) return true;
        if (playerName.Contains("ちんちん")) return true;
        if (playerName.Contains("チンチン")) return true;
        if (playerName.Contains("つんぼ")) return true;
        if (playerName.Contains("ツンボ")) return true;
        if (playerName.Contains("出会い")) return true;
        if (playerName.Contains("であい")) return true;
        if (playerName.Contains("手コキ")) return true;
        if (playerName.Contains("デブ")) return true;
        if (playerName.Contains("でぶ")) return true;
        if (playerName.Contains("手マン")) return true;
        if (playerName.Contains("童貞")) return true;

        //な行
        if (playerName.Contains("中出し")) return true;
        if (playerName.Contains("にくべんき")) return true;
        if (playerName.Contains("肉便器")) return true;


        //は行
        if (playerName.Contains("売春")) return true;
        if (playerName.Contains("パイパン")) return true;
        if (playerName.Contains("ビッチ")) return true;
        if (playerName.Contains("風俗")) return true;
        if (playerName.Contains("フェラ")) return true;
        if (playerName.Contains("筆おろし")) return true;
        if (playerName.Contains("筆下ろし")) return true;
        if (playerName.Contains("不倫")) return true;
        if (playerName.Contains("包茎")) return true;

        //ま行
        if (playerName.Contains("まんこ")) return true;
        if (playerName.Contains("マンコ")) return true;

        //や行
        if (playerName.Contains("野獣")) return true;
        if (playerName.Contains("やじゅう")) return true;
        if (playerName.Contains("ヤジュウ")) return true;
        if (playerName.Contains("810")) return true;
        if (playerName.Contains("やらせて")) return true;
        if (playerName.Contains("ヤリチン")) return true;
        if (playerName.Contains("ヤリマン")) return true;

        //ら行
        if (playerName.Contains("ラブホ")) return true;
        if (playerName.Contains("レイプ")) return true;
        if (playerName.Contains("レズ")) return true;

        //わ行


        //英数字
        if (playerName.Contains("3P")) return true;
        if (playerName.Contains("ASS")) return true;
        if (playerName.Contains("ass")) return true;
        if (playerName.Contains("AV")) return true;
        if (playerName.Contains("KKK")) return true;
        if (playerName.Contains("kkk")) return true;


        //禁止用語を含んでいなければfalseを返す
        return false;
    }
}
