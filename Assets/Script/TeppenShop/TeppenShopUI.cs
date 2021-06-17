using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TeppenShopUI : MonoBehaviour
{
    //SoundManagerのスクリプトの関数使用
    SoundManager soundManager;
    //TeppenMenuUIのスクリプトの関数使用
    TeppenMenuUI teppenMenuUI;

    //コインの枚数を表示
    [SerializeField]
    private Text MyCoinText;

    //BuyPriceTextの表示
    [SerializeField]
    private Text BuyPriceText;
    private int BuyPriceInt = 0;

    //商品の詳細説明文
    [SerializeField]
    private Text ShopDetailsText;
    //商品の詳細イメージの親オブジェクト
    [SerializeField]
    private GameObject ShopDetailsPanel;

    //Updownの詳細イメージ表示
    [SerializeField]
    private GameObject UpdownImage;
    //Missonの詳細イメージ表示
    [SerializeField]
    private GameObject MissionImage;

    //DShoes
    //ボタン表示
    [SerializeField]
    private GameObject DShoesButton;
    //値段
    private int DShoesPrice = 30;
    //値段表示
    [SerializeField]
    private Text DShoesPriceText;
    //DShoesの詳細イメージ表示
    [SerializeField]
    private GameObject DShoesImage;
    //trueであれば合計金額に追加
    private bool DShoesFlag = false;
    //DShoe2
    //ボタン表示
    [SerializeField]
    private GameObject DShoes2Button;
    //値段
    private int DShoes2Price = 60;
    //値段表示
    [SerializeField]
    private Text DShoes2PriceText;
    //trueであれば合計金額に追加
    private bool DShoes2Flag = false;
    //DShoe3
    //ボタン表示
    [SerializeField]
    private GameObject DShoes3Button;
    //値段
    private int DShoes3Price = 90;
    //値段表示
    [SerializeField]
    private Text DShoes3PriceText;
    //trueであれば合計金額に追加
    private bool DShoes3Flag = false;

    //JShoes
    //ボタン表示
    [SerializeField]
    private GameObject JShoesButton;
    //値段
    private int JShoesPrice = 30;
    //値段表示
    [SerializeField]
    private Text JShoesPriceText;
    //JShoesの詳細イメージ表示
    [SerializeField]
    private GameObject JShoesImage;
    //trueであれば合計金額に追加
    private bool JShoesFlag = false;
    //JShoes2
    //ボタン表示
    [SerializeField]
    private GameObject JShoes2Button;
    //値段
    private int JShoes2Price = 60;
    //値段表示
    [SerializeField]
    private Text JShoes2PriceText;
    //trueであれば合計金額に追加
    private bool JShoes2Flag = false;
    //JShoes3
    //ボタン表示
    [SerializeField]
    private GameObject JShoes3Button;
    //値段
    private int JShoes3Price = 90;
    //値段表示
    [SerializeField]
    private Text JShoes3PriceText;
    //trueであれば合計金額に追加
    private bool JShoes3Flag = false;

    //AShoes
    //ボタン表示
    [SerializeField]
    private GameObject AShoesButton;
    //値段
    private int AShoesPrice = 30;
    //値段表示
    [SerializeField]
    private Text AShoesPriceText;
    //AShoesの詳細イメージ表示
    [SerializeField]
    private GameObject AShoesImage;
    //trueであれば合計金額に追加
    private bool AShoesFlag = false;
    //AShoes2
    //ボタン表示
    [SerializeField]
    private GameObject AShoes2Button;
    //値段
    private int AShoes2Price = 60;
    //値段表示
    [SerializeField]
    private Text AShoes2PriceText;
    //trueであれば合計金額に追加
    private bool AShoes2Flag = false;
    //AShoes3
    //ボタン表示
    [SerializeField]
    private GameObject AShoes3Button;
    //値段
    private int AShoes3Price = 90;
    //値段表示
    [SerializeField]
    private Text AShoes3PriceText;
    //trueであれば合計金額に追加
    private bool AShoes3Flag = false;

    //SuperHand
    //ボタン表示
    [SerializeField]
    private GameObject SuperHandButton;
    //値段
    private int SuperHandPrice = 60;
    //値段表示
    [SerializeField]
    private Text SuperHandPriceText;
    //SuperHandの詳細イメージ表示
    [SerializeField]
    private GameObject SuperHandImage;
    //trueであれば合計金額に追加
    private bool SuperHandFlag = false;

    //JWing
    //ボタン表示
    [SerializeField]
    private GameObject JWingButton;
    //値段
    private int JWingPrice = 90;
    //値段表示
    [SerializeField]
    private Text JWingPriceText;
    //JWingの詳細イメージ表示
    [SerializeField]
    private GameObject JWingImage;
    //trueであれば合計金額に追加
    private bool JWingFlag = false;

    //MinusTime
    //ボタン表示
    [SerializeField]
    private GameObject MinusTimeButton;
    //値段
    private int MinusTimePrice = 30;
    //値段表示
    [SerializeField]
    private Text MinusTimePriceText;
    //trueであれば合計金額に追加
    private bool MinusTimeFlag = false;
    //MinusTime2
    //ボタン表示
    [SerializeField]
    private GameObject MinusTime2Button;
    //値段
    private int MinusTime2Price = 60;
    //値段表示
    [SerializeField]
    private Text MinusTime2PriceText;
    //trueであれば合計金額に追加
    private bool MinusTime2Flag = false;
    //MinusTime3
    //ボタン表示
    [SerializeField]
    private GameObject MinusTime3Button;
    //値段
    private int MinusTime3Price = 90;
    //値段表示
    [SerializeField]
    private Text MinusTime3PriceText;
    //trueであれば合計金額に追加
    private bool MinusTime3Flag = false;
    //実際にバトルでマイナスする時間の合計
    public static float MinusTimeRealTotal = 0;

    //RockSlow
    //ボタン表示
    [SerializeField]
    private GameObject RockSlowButton;
    //値段
    private int RockSlowPrice = 30;
    //値段表示
    [SerializeField]
    private Text RockSlowPriceText;
    //trueであれば合計金額に追加
    private bool RockSlowFlag = false;
    //RockSlow2
    //ボタン表示
    [SerializeField]
    private GameObject RockSlow2Button;
    //値段
    private int RockSlow2Price = 60;
    //値段表示
    [SerializeField]
    private Text RockSlow2PriceText;
    //trueであれば合計金額に追加
    private bool RockSlow2Flag = false;
    //RockSlow3
    //ボタン表示
    [SerializeField]
    private GameObject RockSlow3Button;
    //値段
    private int RockSlow3Price = 90;
    //値段表示
    [SerializeField]
    private Text RockSlow3PriceText;
    //trueであれば合計金額に追加
    private bool RockSlow3Flag = false;
    //実際にバトルでマイナスする時間の合計
    public static float RockSlowRealTotal = 0;

    //AirplaneSlow
    //ボタン表示
    [SerializeField]
    private GameObject AirplaneSlowButton;
    //値段
    private int AirplaneSlowPrice = 30;
    //値段表示
    [SerializeField]
    private Text AirplaneSlowPriceText;
    //trueであれば合計金額に追加
    private bool AirplaneSlowFlag = false;
    //AirplaneSlow2
    //ボタン表示
    [SerializeField]
    private GameObject AirplaneSlow2Button;
    //値段
    private int AirplaneSlow2Price = 60;
    //値段表示
    [SerializeField]
    private Text AirplaneSlow2PriceText;
    //trueであれば合計金額に追加
    private bool AirplaneSlow2Flag = false;
    //AirplaneSlow3
    //ボタン表示
    [SerializeField]
    private GameObject AirplaneSlow3Button;
    //値段
    private int AirplaneSlow3Price = 90;
    //値段表示
    [SerializeField]
    private Text AirplaneSlow3PriceText;
    //trueであれば合計金額に追加
    private bool AirplaneSlow3Flag = false;
    //実際にバトルでマイナスする時間の合計
    public static float AirplaneSlowRealTotal = 0;

    //MissionA
    //ボタン表示
    [SerializeField]
    private GameObject MissionAButton;
    //値段
    private int MissionAPrice = 100;
    //値段表示
    [SerializeField]
    private Text MissionAPriceText;
    //trueであれば合計金額に追加
    private bool MissionAFlag = false;
    //実際にバトルで使用する変数
    public static bool MissionASuccessFlag = false;
    //MissionB
    //ボタン表示
    [SerializeField]
    private GameObject MissionBButton;
    //値段
    private int MissionBPrice = 100;
    //値段表示
    [SerializeField]
    private Text MissionBPriceText;
    //trueであれば合計金額に追加
    private bool MissionBFlag = false;
    //実際にバトルで使用する変数
    public static bool MissionBSuccessFlag = false;

    //Stage1
    //ボタン表示
    [SerializeField]
    private GameObject Stage1AssignmentButton;
    //値段
    private int Stage1AssignmentPrice = 30;
    //値段表示
    [SerializeField]
    private Text Stage1AssignmentPriceText;
    //Stage1の詳細イメージ表示
    [SerializeField]
    private GameObject Stage1AssignmentImage;
    //trueであれば合計金額に追加
    private bool Stage1AssignmentFlag = false;
    //Stage2
    //ボタン表示
    [SerializeField]
    private GameObject Stage2AssignmentButton;
    //値段
    private int Stage2AssignmentPrice = 30;
    //値段表示
    [SerializeField]
    private Text Stage2AssignmentPriceText;
    //Stage2の詳細イメージ表示
    [SerializeField]
    private GameObject Stage2AssignmentImage;
    //trueであれば合計金額に追加
    private bool Stage2AssignmentFlag = false;
    //Stage3
    //ボタン表示
    [SerializeField]
    private GameObject Stage3AssignmentButton;
    //値段
    private int Stage3AssignmentPrice = 30;
    //値段表示
    [SerializeField]
    private Text Stage3AssignmentPriceText;
    //Stage3の詳細イメージ表示
    [SerializeField]
    private GameObject Stage3AssignmentImage;
    //trueであれば合計金額に追加
    private bool Stage3AssignmentFlag = false;
    //Stage4
    //ボタン表示
    [SerializeField]
    private GameObject Stage4AssignmentButton;
    [SerializeField]
    private Button Stage4AssignmentButtonButton;
    //値段
    private int Stage4AssignmentPrice = 30;
    //値段表示
    [SerializeField]
    private Text Stage4AssignmentPriceText;
    //Stage4の詳細イメージ表示
    [SerializeField]
    private GameObject Stage4AssignmentImage;
    //trueであれば合計金額に追加
    private bool Stage4AssignmentFlag = false;
    //Stage5
    //ボタン表示
    [SerializeField]
    private GameObject Stage5AssignmentButton;
    [SerializeField]
    private Button Stage5AssignmentButtonButton;
    //値段
    private int Stage5AssignmentPrice = 30;
    //値段表示
    [SerializeField]
    private Text Stage5AssignmentPriceText;
    //Stage5の詳細イメージ表示
    [SerializeField]
    private GameObject Stage5AssignmentImage;
    //trueであれば合計金額に追加
    private bool Stage5AssignmentFlag = false;
    //Stage6
    //ボタン表示
    [SerializeField]
    private GameObject Stage6AssignmentButton;
    [SerializeField]
    private Button Stage6AssignmentButtonButton;
    //値段
    private int Stage6AssignmentPrice = 30;
    //値段表示
    [SerializeField]
    private Text Stage6AssignmentPriceText;
    //Stage6の詳細イメージ表示
    [SerializeField]
    private GameObject Stage6AssignmentImage;
    //trueであれば合計金額に追加
    private bool Stage6AssignmentFlag = false;
    //selectStage関数に渡す
    public static int stageAssignmentNumber = 0;

    //Trampoline
    //ボタン表示
    [SerializeField]
    private GameObject TrampolineButton;
    //値段
    private int TrampolinePrice = 30;
    //値段表示
    [SerializeField]
    private Text TrampolinePriceText;
    //trueであれば合計金額に追加
    private bool TrampolineFlag = false;
    //実際にバトルで使用するかを判断する
    public static bool TrampolineRealFlag = false;

    //DogAdd
    //ボタン表示
    [SerializeField]
    private GameObject DogAddButton;
    //値段
    private int DogAddPrice = 200;
    //値段表示
    [SerializeField]
    private Text DogAddPriceText;
    //DogAddの詳細イメージ表示
    [SerializeField]
    private GameObject DogAddImage;
    //trueであれば合計金額に追加
    private bool DogAddFlag = false;
    //GiraffeAdd
    //ボタン表示
    [SerializeField]
    private GameObject GiraffeAddButton;
    [SerializeField]
    private Button GiraffeAddButtonButton;
    //値段
    private int GiraffeAddPrice = 150;
    //値段表示
    [SerializeField]
    private Text GiraffeAddPriceText;
    //GiraffeAddの詳細イメージ表示
    [SerializeField]
    private GameObject GiraffeAddImage;
    //trueであれば合計金額に追加
    private bool GiraffeAddFlag = false;
    //ElephantAdd
    //ボタン表示
    [SerializeField]
    private GameObject ElephantAddButton;
    [SerializeField]
    private Button ElephantAddButtonButton;
    //値段
    private int ElephantAddPrice = 150;
    //値段表示
    [SerializeField]
    private Text ElephantAddPriceText;
    //ElephantAddの詳細イメージ表示
    [SerializeField]
    private GameObject ElephantAddImage;
    //trueであれば合計金額に追加
    private bool ElephantAddFlag = false;
    //TigerAdd
    //ボタン表示
    [SerializeField]
    private GameObject TigerAddButton;
    [SerializeField]
    private Button TigerAddButtonButton;
    //値段
    private int TigerAddPrice = 200;
    //値段表示
    [SerializeField]
    private Text TigerAddPriceText;
    //TigerAddの詳細イメージ表示
    [SerializeField]
    private GameObject TigerAddImage;
    //trueであれば合計金額に追加
    private bool TigerAddFlag = false;
    //CatAdd
    //ボタン表示
    [SerializeField]
    private GameObject CatAddButton;
    [SerializeField]
    private Button CatAddButtonButton;
    //値段
    private int CatAddPrice = 200;
    //値段表示
    [SerializeField]
    private Text CatAddPriceText;
    //CatAddの詳細イメージ表示
    [SerializeField]
    private GameObject CatAddImage;
    //trueであれば合計金額に追加
    private bool CatAddFlag = false;
    //RabbitAdd
    //ボタン表示
    [SerializeField]
    private GameObject RabbitAddButton;
    [SerializeField]
    private Button RabbitAddButtonButton;
    //値段
    private int RabbitAddPrice = 200;
    //値段表示
    [SerializeField]
    private Text RabbitAddPriceText;
    //RabbitAddの詳細イメージ表示
    [SerializeField]
    private GameObject RabbitAddImage;
    //trueであれば合計金額に追加
    private bool RabbitAddFlag = false;

    
    //テッペンショップゲームオブジェクト
    [SerializeField]
    private GameObject TeppenShopCanvas;
    //テッペンバトルシーンゲームオブジェクト
    [SerializeField]
    private GameObject TeppenBattleScene;

    //これからリストに追加するかもしれないキャラクター数
    private int characterListAdd;

    //キャラクターリストの初期化
    public static List<string> characterList = new List<string>();

    //ボタン押下時間
    private float closeButtomTime = 2.0f;
    //クローズボタン
    [SerializeField]
    private Button CloseButton;

    // Start is called before the first frame update
    void Start()
    {
        //SoundManagerのスクリプトの関数使用
        soundManager = GameObject.Find("Sound").GetComponent<SoundManager>();
        //TeppenMenuUIのスクリプトの関数使用
        teppenMenuUI = GameObject.Find("TeppenMenuCanvas").GetComponent<TeppenMenuUI>();

        //SEの使用(猫の鳴き声)
        soundManager.SEManager("CatNakigoe_sound1");

        //猫の言葉
        CatTalk();

        //キャラクターリスト取得
        characterList.Clear();
        CharacterList();

        //ショップリストの追加(ランダム)
        for (int i = 0; i < 3; i++)
        {
            int[] randomShopList = new int[3];
            randomShopList[i] = Random.Range(0, 35);
            randomShopList[0] = 34;

            if (PlayerPrefs.GetInt("TeppenRandomShopList1") == -1 && i == 0) PlayerPrefs.SetInt("TeppenRandomShopList1", randomShopList[i]);
            else if(PlayerPrefs.GetInt("TeppenRandomShopList2") == -1 && i == 1) PlayerPrefs.SetInt("TeppenRandomShopList2", randomShopList[i]);
            else if (PlayerPrefs.GetInt("TeppenRandomShopList3") == -1 && i == 2) PlayerPrefs.SetInt("TeppenRandomShopList3", randomShopList[i]);
            else
            {
                randomShopList[0] = PlayerPrefs.GetInt("TeppenRandomShopList1");
                randomShopList[1] = PlayerPrefs.GetInt("TeppenRandomShopList2");
                randomShopList[2] = PlayerPrefs.GetInt("TeppenRandomShopList3");
            }

            //商品の表示
            switch (randomShopList[i])
            {
                case 0:
                    if (PlayerPrefs.GetInt("DShoesFlag") == 0)
                    {
                        //ボタンの表示
                        DShoesButton.SetActive(true);
                        //値段の表示
                        DShoesPriceText.text = DShoesPrice.ToString("");
                    }
                    break;
                case 1:
                    if (PlayerPrefs.GetInt("DShoes2Flag") == 0)
                    {
                        //ボタンの表示
                        DShoes2Button.SetActive(true);
                        //値段の表示
                        DShoes2PriceText.text = DShoes2Price.ToString("");
                    }
                    break;
                case 2:
                    if (PlayerPrefs.GetInt("DShoes3Flag") == 0)
                    {
                        //ボタンの表示
                        DShoes3Button.SetActive(true);
                        //値段の表示
                        DShoes3PriceText.text = DShoes3Price.ToString("");
                    }
                    break;
                case 3:
                    //ボタンの表示
                    MinusTimeButton.SetActive(true);
                    //値段の表示
                    MinusTimePriceText.text = MinusTimePrice.ToString("");
                    break;
                case 4:
                    //ボタンの表示
                    MinusTime2Button.SetActive(true);
                    //値段の表示
                    MinusTime2PriceText.text = MinusTime2Price.ToString("");
                    break;
                case 5:
                    //ボタンの表示
                    MinusTime3Button.SetActive(true);
                    //値段の表示
                    MinusTime3PriceText.text = MinusTime3Price.ToString("");
                    break;
                case 6:
                    if (PlayerPrefs.GetInt("TeppenDogCanUse") == 0 && PlayerPrefs.GetString("TeppenAnimalName") != "Dog" && characterList.Count + characterListAdd < 3)
                    {
                        //ボタンの表示
                        DogAddButton.SetActive(true);
                        //キャラクター上限3を超えないようにする
                        characterListAdd++;
                        //値段の表示
                        DogAddPriceText.text = DogAddPrice.ToString("");
                    }
                    break;
                case 7:
                    if (PlayerPrefs.GetInt("TeppenGiraffeCanUse") == 0 && PlayerPrefs.GetString("TeppenAnimalName") != "Giraffe" && characterList.Count + characterListAdd < 3)
                    {
                        //ボタンの表示
                        GiraffeAddButton.SetActive(true);
                        //ボタンを押下できるか判断
                        if (PlayerPrefs.GetInt("Unlock_Giraffe") == 0) GiraffeAddButtonButton.interactable = false;
                        //キャラクター上限3を超えないようにする
                        characterListAdd++;
                        //値段の表示
                        GiraffeAddPriceText.text = GiraffeAddPrice.ToString("");
                    }
                    break;
                case 8:
                    if (PlayerPrefs.GetInt("TeppenElephantCanUse") == 0 && PlayerPrefs.GetString("TeppenAnimalName") != "Elephant" && characterList.Count + characterListAdd < 3)
                    {
                        //ボタンの表示
                        ElephantAddButton.SetActive(true);
                        //ボタンを押下できるか判断
                        if (PlayerPrefs.GetInt("Unlock_Elephant") == 0) ElephantAddButtonButton.interactable = false;
                        //キャラクター上限3を超えないようにする
                        characterListAdd++;
                        //値段の表示
                        ElephantAddPriceText.text = ElephantAddPrice.ToString("");
                    }
                    break;
                case 9:
                    if (PlayerPrefs.GetInt("TeppenTigerCanUse") == 0 && PlayerPrefs.GetString("TeppenAnimalName") != "Tiger" && characterList.Count + characterListAdd < 3)
                    {
                        //ボタンの表示
                        TigerAddButton.SetActive(true);
                        //ボタンを押下できるか判断
                        if (PlayerPrefs.GetInt("Unlock_Tiger") == 0) TigerAddButtonButton.interactable = false;
                        //キャラクター上限3を超えないようにする
                        characterListAdd++;
                        //値段の表示
                        TigerAddPriceText.text = TigerAddPrice.ToString("");
                    }
                    break;
                case 10:
                    if (PlayerPrefs.GetInt("TeppenCatCanUse") == 0 && PlayerPrefs.GetString("TeppenAnimalName") != "Cat" &&  characterList.Count + characterListAdd < 3)
                    {
                        //ボタンの表示
                        CatAddButton.SetActive(true);
                        //ボタンを押下できるか判断
                        if (PlayerPrefs.GetInt("Unlock_Cat") == 0) CatAddButtonButton.interactable = false;
                        //キャラクター上限3を超えないようにする
                        characterListAdd++;
                        //値段の表示
                        CatAddPriceText.text = CatAddPrice.ToString("");
                    }
                    break;
                case 11:
                    if (PlayerPrefs.GetInt("TeppenRabbitCanUse") == 0 && PlayerPrefs.GetString("TeppenAnimalName") != "Rabbit" && characterList.Count + characterListAdd < 3)
                    {
                        //ボタンの表示
                        RabbitAddButton.SetActive(true);
                        //ボタンを押下できるか判断
                        if (PlayerPrefs.GetInt("Unlock_Rabbit") == 0) RabbitAddButtonButton.interactable = false;
                        //キャラクター上限3を超えないようにする
                        characterListAdd++;
                        //値段の表示
                        RabbitAddPriceText.text = RabbitAddPrice.ToString("");
                    }
                    break;
                case 12:
                    if (PlayerPrefs.GetInt("JShoesFlag") == 0)
                    {
                        //ボタンの表示
                        JShoesButton.SetActive(true);
                        //値段の表示
                        JShoesPriceText.text = JShoesPrice.ToString("");
                    }
                    break;
                case 13:
                    if (PlayerPrefs.GetInt("JShoes2Flag") == 0)
                    {
                        //ボタンの表示
                        JShoes2Button.SetActive(true);
                        //値段の表示
                        JShoes2PriceText.text = JShoes2Price.ToString("");
                    }
                    break;
                case 14:
                    if (PlayerPrefs.GetInt("JShoes3Flag") == 0)
                    {
                        //ボタンの表示
                        JShoes3Button.SetActive(true);
                        //値段の表示
                        JShoes3PriceText.text = JShoes3Price.ToString("");
                    }
                    break;
                case 15:
                    //ボタンの表示
                    RockSlowButton.SetActive(true);
                    //値段の表示
                    RockSlowPriceText.text = RockSlowPrice.ToString("");
                    break;
                case 16:
                    //ボタンの表示
                    RockSlow2Button.SetActive(true);
                    //値段の表示
                    RockSlow2PriceText.text = RockSlow2Price.ToString("");
                    break;
                case 17:
                    //ボタンの表示
                    RockSlow3Button.SetActive(true);
                    //値段の表示
                    RockSlow3PriceText.text = RockSlow3Price.ToString("");
                    break;
                case 18:
                    if (PlayerPrefs.GetInt("AShoesFlag") == 0)
                    {
                        //ボタンの表示
                        AShoesButton.SetActive(true);
                        //値段の表示
                        AShoesPriceText.text = AShoesPrice.ToString("");
                    }
                    break;
                case 19:
                    if (PlayerPrefs.GetInt("AShoes2Flag") == 0)
                    {
                        //ボタンの表示
                        AShoes2Button.SetActive(true);
                        //値段の表示
                        AShoes2PriceText.text = AShoes2Price.ToString("");
                    }
                    break;
                case 20:
                    if (PlayerPrefs.GetInt("AShoes3Flag") == 0)
                    {
                        //ボタンの表示
                        AShoes3Button.SetActive(true);
                        //値段の表示
                        AShoes3PriceText.text = AShoes3Price.ToString("");
                    }
                    break;
                case 21:
                    if (stageAssignmentNumber == 0)
                    {
                        //ボタンの表示
                        Stage1AssignmentButton.SetActive(true);
                        //値段の表示
                        Stage1AssignmentPriceText.text = Stage1AssignmentPrice.ToString("");
                    }
                    break;
                case 22:
                    if (stageAssignmentNumber == 0)
                    {
                        //ボタンの表示
                        Stage2AssignmentButton.SetActive(true);
                        //値段の表示
                        Stage2AssignmentPriceText.text = Stage2AssignmentPrice.ToString("");
                    }
                    break;
                case 23:
                    if (stageAssignmentNumber == 0)
                    {
                        //ボタンの表示
                        Stage3AssignmentButton.SetActive(true);
                        //値段の表示
                        Stage3AssignmentPriceText.text = Stage3AssignmentPrice.ToString("");
                    }
                    break;
                case 24:
                    if (stageAssignmentNumber == 0)
                    {
                        //ボタンの表示
                        Stage4AssignmentButton.SetActive(true);
                        //ボタンを押下できるか判断
                        if (PlayerPrefs.GetInt("Unlock_Stage4") == 0) Stage4AssignmentButtonButton.interactable = false;
                        //値段の表示
                        Stage4AssignmentPriceText.text = Stage4AssignmentPrice.ToString("");
                    }
                    break;
                case 25:
                    if (stageAssignmentNumber == 0)
                    {
                        //ボタンの表示
                        Stage5AssignmentButton.SetActive(true);
                        //ボタンを押下できるか判断
                        if (PlayerPrefs.GetInt("Unlock_Stage5") == 0) Stage5AssignmentButtonButton.interactable = false;
                        //値段の表示
                        Stage5AssignmentPriceText.text = Stage5AssignmentPrice.ToString("");
                    }
                    break;
                case 26:
                    if (stageAssignmentNumber == 0)
                    {
                        //ボタンの表示
                        Stage6AssignmentButton.SetActive(true);
                        //ボタンを押下できるか判断
                        if (PlayerPrefs.GetInt("Unlock_Stage6") == 0) Stage6AssignmentButtonButton.interactable = false;
                        //値段の表示
                        Stage6AssignmentPriceText.text = Stage6AssignmentPrice.ToString("");
                    }
                    break;
                case 27:
                    if (PlayerPrefs.GetInt("SuperHandFlag") == 0)
                    {
                        //ボタンの表示
                        SuperHandButton.SetActive(true);
                        //値段の表示
                        SuperHandPriceText.text = SuperHandPrice.ToString("");
                    }
                    break;
                case 28:
                    if (PlayerPrefs.GetInt("JWingFlag") == 0)
                    {
                        //ボタンの表示
                        JWingButton.SetActive(true);
                        //値段の表示
                        JWingPriceText.text = JWingPrice.ToString("");
                    }
                    break;
                case 29:
                    //ボタンの表示
                    AirplaneSlowButton.SetActive(true);
                    //値段の表示
                    AirplaneSlowPriceText.text = AirplaneSlowPrice.ToString("");
                    break;
                case 30:
                    //ボタンの表示
                    AirplaneSlow2Button.SetActive(true);
                    //値段の表示
                    AirplaneSlow2PriceText.text = AirplaneSlow2Price.ToString("");
                    break;
                case 31:
                    //ボタンの表示
                    AirplaneSlow3Button.SetActive(true);
                    //値段の表示
                    AirplaneSlow3PriceText.text = AirplaneSlow3Price.ToString("");
                    break;
                case 32:
                    //ボタンの表示
                    MissionAButton.SetActive(true);
                    //値段の表示
                    MissionAPriceText.text = MissionAPrice.ToString("");
                    break;
                case 33:
                    //ボタンの表示
                    MissionBButton.SetActive(true);
                    //値段の表示
                    MissionBPriceText.text = MissionBPrice.ToString("");
                    break;
                case 34:
                    //ボタンの表示
                    TrampolineButton.SetActive(true);
                    //値段の表示
                    TrampolinePriceText.text = TrampolinePrice.ToString("");
                    break;
                default:
                    break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //合計値段テキスト表示
        BuyPriceText.text = "計：" + BuyPriceInt.ToString("") + "コイン";
        //合計値段テキストの色変更
        if (PlayerPrefs.GetInt("myCoin") >= BuyPriceInt) BuyPriceText.color = Color.black;
        else { BuyPriceText.color = Color.red; }
        //マイコインの表示
        MyCoinText.text = PlayerPrefs.GetInt("myCoin").ToString("");

        if (closeButtomTime <= 0) CloseButton.interactable = true;
        else { closeButtomTime -= Time.deltaTime; }

    }

    //猫の言葉
    private void CatTalk()
    {
        int randomCatTalk = Random.Range(0,7);
        switch (randomCatTalk)
        {
            case 0:
                ShopDetailsText.text = "(ねこ助)いらっしゃいにゃーん！";
                break;
            case 1:
                ShopDetailsText.text = "(ねこ助)商品みていってにゃん";
                break;
            case 2:
                ShopDetailsText.text = "(ねこ助)なにか気にいるものがあるといいにゃん！";
                break;
            case 4:
                ShopDetailsText.text = "(ねこ助)今日は何をお求めですにゃん？";
                break;
            case 5:
                ShopDetailsText.text = "(ねこ助)Zzzz...Zzzz...いらっしゃいにゃん";
                break;
            case 6:
                ShopDetailsText.text = "(ねこ助)お客さんにゃん！";
                break;
            default:
                break;
        }
        if(PlayerPrefs.GetInt("TeppenFloor") == 10) ShopDetailsText.text = "(ねこ助)10階到達おめでとにゃん！";
        if (PlayerPrefs.GetInt("TeppenFloor") == 20) ShopDetailsText.text = "(ねこ助)20階まで到達すごいにゃん";
        if (PlayerPrefs.GetInt("TeppenFloor") == 30) ShopDetailsText.text = "(ねこ助)いつの間にか30階まで到達してるにゃん！";
        if (PlayerPrefs.GetInt("TeppenFloor") == 40) ShopDetailsText.text = "(ねこ助)40階到達久しぶりに見たにゃん...";
        if (PlayerPrefs.GetInt("TeppenFloor") == 50) ShopDetailsText.text = "(ねこ助)根性がすごいにゃん...50階到達おめでとにゃん！";
        if (PlayerPrefs.GetInt("TeppenFloor") == 60) ShopDetailsText.text = "(ねこ助)どこまで行くにゃん...？";
        if (PlayerPrefs.GetInt("TeppenFloor") == 70) ShopDetailsText.text = "(ねこ助)70階！？どこまで行けるにゃん";
        if (PlayerPrefs.GetInt("TeppenFloor") == 80) ShopDetailsText.text = "(ねこ助)80回もこうして顔を合わせていると友達のような気分にゃん♪";
        if (PlayerPrefs.GetInt("TeppenFloor") == 90) ShopDetailsText.text = "(ねこ助)100回まであと10回！頑張るにゃん！";
        if (PlayerPrefs.GetInt("TeppenFloor") == 100) ShopDetailsText.text = "(ねこ助)" + PlayerPrefs.GetString("NickName") + "さん、本当にすごいにゃん..！ついに100回到達したにゃーん！！";
        if (PlayerPrefs.GetInt("TeppenFloor") == 150) ShopDetailsText.text = "(ねこ助)" + PlayerPrefs.GetString("NickName") + "さん、まだのぼるにゃん...？";
        if (PlayerPrefs.GetInt("TeppenFloor") == 200) ShopDetailsText.text = "(ねこ助)" + PlayerPrefs.GetString("NickName") + "さんは歴史に残るにゃん！";
    }

    //キャラクターリスト
    public static void CharacterList()
    {
        if(PlayerPrefs.GetInt("TeppenDogCanUse") == 1 || PlayerPrefs.GetString("TeppenAnimalName") == "Dog")characterList.Add("Dog");
        if (PlayerPrefs.GetInt("TeppenGiraffeCanUse") == 1 || PlayerPrefs.GetString("TeppenAnimalName") == "Giraffe") characterList.Add("Giraffe");
        if (PlayerPrefs.GetInt("TeppenElephantCanUse") == 1 || PlayerPrefs.GetString("TeppenAnimalName") == "Elephant") characterList.Add("Elephant");
        if (PlayerPrefs.GetInt("TeppenTigerCanUse") == 1 || PlayerPrefs.GetString("TeppenAnimalName") == "Tiger") characterList.Add("Tiger");
        if (PlayerPrefs.GetInt("TeppenCatCanUse") == 1 || PlayerPrefs.GetString("TeppenAnimalName") == "Cat") characterList.Add("Cat");
        if (PlayerPrefs.GetInt("TeppenRabbitCanUse") == 1 || PlayerPrefs.GetString("TeppenAnimalName") == "Rabbit") characterList.Add("Rabbit");
    }

    //DShoesボタンを押した時
    public void OnClick_DShoesButton()
    {
        //SEの使用
        soundManager.SEManager("CharacterSelect_sound1");

        //trueであれば合計金額に追加
        if (DShoesFlag == false)
        {
            //詳細表示
            ShopDetailsText.text = "ダッシューズ\n\n移動速度+5";
            //DShoesのイメージ表示
            foreach (Transform childTransform in ShopDetailsPanel.transform) childTransform.gameObject.SetActive(false);
            DShoesImage.SetActive(true);
            //値段の追加
            BuyPriceInt += DShoesPrice;
            //ボタンの色変更
            DShoesButton.GetComponent<Image>().color = new Color32(238, 255, 227, 255);
            //合計金額に追加している状態
            DShoesFlag = true;
        }
        else
        {
            ShopDetailsText.text = "";
            foreach (Transform childTransform in ShopDetailsPanel.transform) childTransform.gameObject.SetActive(false);
            BuyPriceInt -= DShoesPrice;
            DShoesButton.GetComponent<Image>().color = new Color32(208, 255, 177, 255);
            DShoesFlag = false;
        }
    }

    //DShoes2ボタンを押した時
    public void OnClick_DShoes2Button()
    {
        //SEの使用
        soundManager.SEManager("CharacterSelect_sound1");

        //trueであれば合計金額に追加
        if (DShoes2Flag == false)
        {
            //詳細表示
            ShopDetailsText.text = "ダッシューズ2\n\n移動速度+10";
            //DShoesのイメージ表示
            foreach (Transform childTransform in ShopDetailsPanel.transform) childTransform.gameObject.SetActive(false);
            DShoesImage.SetActive(true);
            //値段の追加
            BuyPriceInt += DShoes2Price;
            //ボタンの色変更
            DShoes2Button.GetComponent<Image>().color = new Color32(238, 255, 227, 255);
            //合計金額に追加している状態
            DShoes2Flag = true;
        }
        else
        {
            ShopDetailsText.text = "";
            foreach (Transform childTransform in ShopDetailsPanel.transform) childTransform.gameObject.SetActive(false);
            BuyPriceInt -= DShoes2Price;
            DShoes2Button.GetComponent<Image>().color = new Color32(208, 255, 177, 255);
            DShoes2Flag = false;
        }
    }

    //DShoes3ボタンを押した時
    public void OnClick_DShoes3Button()
    {
        //SEの使用
        soundManager.SEManager("CharacterSelect_sound1");

        //trueであれば合計金額に追加
        if (DShoes3Flag == false)
        {
            //詳細表示
            ShopDetailsText.text = "ダッシューズ3\n\n移動速度+15";
            //DShoesのイメージ表示
            foreach (Transform childTransform in ShopDetailsPanel.transform) childTransform.gameObject.SetActive(false);
            DShoesImage.SetActive(true);
            //値段の追加
            BuyPriceInt += DShoes3Price;
            //ボタンの色変更
            DShoes3Button.GetComponent<Image>().color = new Color32(238, 255, 227, 255);
            //合計金額に追加している状態
            DShoes3Flag = true;
        }
        else
        {
            ShopDetailsText.text = "";
            foreach (Transform childTransform in ShopDetailsPanel.transform) childTransform.gameObject.SetActive(false);
            BuyPriceInt -= DShoes3Price;
            DShoes3Button.GetComponent<Image>().color = new Color32(208, 255, 177, 255);
            DShoes3Flag = false;
        }
    }

    //JShoesボタンを押した時
    public void OnClick_JShoesButton()
    {
        //SEの使用
        soundManager.SEManager("CharacterSelect_sound1");

        //trueであれば合計金額に追加
        if (JShoesFlag == false)
        {
            //詳細表示
            ShopDetailsText.text = "ジャンシューズ\n\nジャンプ力+2";
            //JShoesのイメージ表示
            foreach (Transform childTransform in ShopDetailsPanel.transform) childTransform.gameObject.SetActive(false);
            JShoesImage.SetActive(true);
            //値段の追加
            BuyPriceInt += JShoesPrice;
            //ボタンの色変更
            JShoesButton.GetComponent<Image>().color = new Color32(238, 255, 227, 255);
            //合計金額に追加している状態
            JShoesFlag = true;
        }
        else
        {
            ShopDetailsText.text = "";
            foreach (Transform childTransform in ShopDetailsPanel.transform) childTransform.gameObject.SetActive(false);
            BuyPriceInt -= JShoesPrice;
            JShoesButton.GetComponent<Image>().color = new Color32(208, 255, 177, 255);
            JShoesFlag = false;
        }
    }

    //JShoes2ボタンを押した時
    public void OnClick_JShoes2Button()
    {
        //SEの使用
        soundManager.SEManager("CharacterSelect_sound1");

        //trueであれば合計金額に追加
        if (JShoes2Flag == false)
        {
            //詳細表示
            ShopDetailsText.text = "ジャンシューズ2\n\nジャンプ力+4";
            //JShoesのイメージ表示
            foreach (Transform childTransform in ShopDetailsPanel.transform) childTransform.gameObject.SetActive(false);
            JShoesImage.SetActive(true);
            //値段の追加
            BuyPriceInt += JShoes2Price;
            //ボタンの色変更
            JShoes2Button.GetComponent<Image>().color = new Color32(238, 255, 227, 255);
            //合計金額に追加している状態
            JShoes2Flag = true;
        }
        else
        {
            ShopDetailsText.text = "";
            foreach (Transform childTransform in ShopDetailsPanel.transform) childTransform.gameObject.SetActive(false);
            BuyPriceInt -= JShoes2Price;
            JShoes2Button.GetComponent<Image>().color = new Color32(208, 255, 177, 255);
            JShoes2Flag = false;
        }
    }

    //JShoes3ボタンを押した時
    public void OnClick_JShoes3Button()
    {
        //SEの使用
        soundManager.SEManager("CharacterSelect_sound1");

        //trueであれば合計金額に追加
        if (JShoes3Flag == false)
        {
            //詳細表示
            ShopDetailsText.text = "ジャンシューズ3\n\nジャンプ力+6";
            //JShoesのイメージ表示
            foreach (Transform childTransform in ShopDetailsPanel.transform) childTransform.gameObject.SetActive(false);
            JShoesImage.SetActive(true);
            //値段の追加
            BuyPriceInt += JShoes3Price;
            //ボタンの色変更
            JShoes3Button.GetComponent<Image>().color = new Color32(238, 255, 227, 255);
            //合計金額に追加している状態
            JShoes3Flag = true;
        }
        else
        {
            ShopDetailsText.text = "";
            foreach (Transform childTransform in ShopDetailsPanel.transform) childTransform.gameObject.SetActive(false);
            BuyPriceInt -= JShoes3Price;
            JShoes3Button.GetComponent<Image>().color = new Color32(208, 255, 177, 255);
            JShoes3Flag = false;
        }
    }

    //AShoesボタンを押した時
    public void OnClick_AShoesButton()
    {
        //SEの使用
        soundManager.SEManager("CharacterSelect_sound1");

        //trueであれば合計金額に追加
        if (AShoesFlag == false)
        {
            //詳細表示
            ShopDetailsText.text = "空ダッシューズ\n\n空中移動速度+5";
            //AShoesのイメージ表示
            foreach (Transform childTransform in ShopDetailsPanel.transform) childTransform.gameObject.SetActive(false);
            AShoesImage.SetActive(true);
            //値段の追加
            BuyPriceInt += AShoesPrice;
            //ボタンの色変更
            AShoesButton.GetComponent<Image>().color = new Color32(238, 255, 227, 255);
            //合計金額に追加している状態
            AShoesFlag = true;
        }
        else
        {
            ShopDetailsText.text = "";
            foreach (Transform childTransform in ShopDetailsPanel.transform) childTransform.gameObject.SetActive(false);
            BuyPriceInt -= AShoesPrice;
            AShoesButton.GetComponent<Image>().color = new Color32(208, 255, 177, 255);
            AShoesFlag = false;
        }
    }

    //AShoes2ボタンを押した時
    public void OnClick_AShoes2Button()
    {
        //SEの使用
        soundManager.SEManager("CharacterSelect_sound1");

        //trueであれば合計金額に追加
        if (AShoes2Flag == false)
        {
            //詳細表示
            ShopDetailsText.text = "空ダッシューズ2\n\n空中移動速度+10";
            //AShoesのイメージ表示
            foreach (Transform childTransform in ShopDetailsPanel.transform) childTransform.gameObject.SetActive(false);
            AShoesImage.SetActive(true);
            //値段の追加
            BuyPriceInt += AShoes2Price;
            //ボタンの色変更
            AShoes2Button.GetComponent<Image>().color = new Color32(238, 255, 227, 255);
            //合計金額に追加している状態
            AShoes2Flag = true;
        }
        else
        {
            ShopDetailsText.text = "";
            foreach (Transform childTransform in ShopDetailsPanel.transform) childTransform.gameObject.SetActive(false);
            BuyPriceInt -= AShoes2Price;
            AShoes2Button.GetComponent<Image>().color = new Color32(208, 255, 177, 255);
            AShoes2Flag = false;
        }
    }

    //AShoes3ボタンを押した時
    public void OnClick_AShoes3Button()
    {
        //SEの使用
        soundManager.SEManager("CharacterSelect_sound1");

        //trueであれば合計金額に追加
        if (AShoes3Flag == false)
        {
            //詳細表示
            ShopDetailsText.text = "空ダッシューズ3\n\n空中移動速度+15";
            //AShoesのイメージ表示
            foreach (Transform childTransform in ShopDetailsPanel.transform) childTransform.gameObject.SetActive(false);
            AShoesImage.SetActive(true);
            //値段の追加
            BuyPriceInt += AShoes3Price;
            //ボタンの色変更
            AShoes3Button.GetComponent<Image>().color = new Color32(238, 255, 227, 255);
            //合計金額に追加している状態
            AShoes3Flag = true;
        }
        else
        {
            ShopDetailsText.text = "";
            foreach (Transform childTransform in ShopDetailsPanel.transform) childTransform.gameObject.SetActive(false);
            BuyPriceInt -= AShoes3Price;
            AShoes3Button.GetComponent<Image>().color = new Color32(208, 255, 177, 255);
            AShoes3Flag = false;
        }
    }

    //SuperHandボタンを押した時
    public void OnClick_SuperHandButton()
    {
        //SEの使用
        soundManager.SEManager("CharacterSelect_sound1");

        //trueであれば合計金額に追加
        if (SuperHandFlag == false)
        {
            //詳細表示
            ShopDetailsText.text = "スーパーハンド\n\n開始から10秒間は物との接触が可能";
            //SuperHandのイメージ表示
            foreach (Transform childTransform in ShopDetailsPanel.transform) childTransform.gameObject.SetActive(false);
            SuperHandImage.SetActive(true);
            //値段の追加
            BuyPriceInt += SuperHandPrice;
            //ボタンの色変更
            SuperHandButton.GetComponent<Image>().color = new Color32(238, 255, 227, 255);
            //合計金額に追加している状態
            SuperHandFlag = true;
        }
        else
        {
            ShopDetailsText.text = "";
            foreach (Transform childTransform in ShopDetailsPanel.transform) childTransform.gameObject.SetActive(false);
            BuyPriceInt -= SuperHandPrice;
            SuperHandButton.GetComponent<Image>().color = new Color32(208, 255, 177, 255);
            SuperHandFlag = false;
        }
    }

    //JWingボタンを押した時
    public void OnClick_JWingButton()
    {
        //SEの使用
        soundManager.SEManager("CharacterSelect_sound1");

        //trueであれば合計金額に追加
        if (JWingFlag == false)
        {
            //詳細表示
            ShopDetailsText.text = "翼羽\n\nジャンプ回数+1";
            //SuperHandのイメージ表示
            foreach (Transform childTransform in ShopDetailsPanel.transform) childTransform.gameObject.SetActive(false);
            JWingImage.SetActive(true);
            //値段の追加
            BuyPriceInt += JWingPrice;
            //ボタンの色変更
            JWingButton.GetComponent<Image>().color = new Color32(238, 255, 227, 255);
            //合計金額に追加している状態
            JWingFlag = true;
        }
        else
        {
            ShopDetailsText.text = "";
            foreach (Transform childTransform in ShopDetailsPanel.transform) childTransform.gameObject.SetActive(false);
            BuyPriceInt -= JWingPrice;
            JWingButton.GetComponent<Image>().color = new Color32(208, 255, 177, 255);
            JWingFlag = false;
        }
    }

    //MinusTimeButtonボタンを押した時
    public void OnClick_MinusTimeButton()
    {
        //SEの使用
        soundManager.SEManager("CharacterSelect_sound1");

        //trueであれば合計金額に追加
        if (MinusTimeFlag == false)
        {
            //詳細表示
            ShopDetailsText.text = "マイナスタイム\n\n時間-5";
            //MinusTimeのイメージ表示
            foreach (Transform childTransform in ShopDetailsPanel.transform) childTransform.gameObject.SetActive(false);
            UpdownImage.SetActive(true);
            //値段の追加
            BuyPriceInt += MinusTimePrice;
            //ボタンの色変更
            MinusTimeButton.GetComponent<Image>().color = new Color32(238, 255, 227, 255);
            //合計金額に追加している状態
            MinusTimeFlag = true;
        }
        else
        {
            ShopDetailsText.text = "";
            foreach (Transform childTransform in ShopDetailsPanel.transform) childTransform.gameObject.SetActive(false);
            BuyPriceInt -= MinusTimePrice;
            MinusTimeButton.GetComponent<Image>().color = new Color32(208, 255, 177, 255);
            MinusTimeFlag = false;
        }
    }

    //MinusTime2Buttonボタンを押した時
    public void OnClick_MinusTime2Button()
    {
        //SEの使用
        soundManager.SEManager("CharacterSelect_sound1");

        //trueであれば合計金額に追加
        if (MinusTime2Flag == false)
        {
            //詳細表示
            ShopDetailsText.text = "マイナスタイム2\n\n時間-20";
            //MinusTimeのイメージ表示
            foreach (Transform childTransform in ShopDetailsPanel.transform) childTransform.gameObject.SetActive(false);
            UpdownImage.SetActive(true);
            //値段の追加
            BuyPriceInt += MinusTime2Price;
            //ボタンの色変更
            MinusTime2Button.GetComponent<Image>().color = new Color32(238, 255, 227, 255);
            //合計金額に追加している状態
            MinusTime2Flag = true;
        }
        else
        {
            ShopDetailsText.text = "";
            foreach (Transform childTransform in ShopDetailsPanel.transform) childTransform.gameObject.SetActive(false);
            BuyPriceInt -= MinusTime2Price;
            MinusTime2Button.GetComponent<Image>().color = new Color32(208, 255, 177, 255);
            MinusTime2Flag = false;
        }
    }

    //MinusTime3Buttonボタンを押した時
    public void OnClick_MinusTime3Button()
    {
        //SEの使用
        soundManager.SEManager("CharacterSelect_sound1");

        //trueであれば合計金額に追加
        if (MinusTime3Flag == false)
        {
            //詳細表示
            ShopDetailsText.text = "マイナスタイム3\n\n時間-50";
            //MinusTimeのイメージ表示
            foreach (Transform childTransform in ShopDetailsPanel.transform) childTransform.gameObject.SetActive(false);
            UpdownImage.SetActive(true);
            //値段の追加
            BuyPriceInt += MinusTime3Price;
            //ボタンの色変更
            MinusTime3Button.GetComponent<Image>().color = new Color32(238, 255, 227, 255);
            //合計金額に追加している状態
            MinusTime3Flag = true;
        }
        else
        {
            ShopDetailsText.text = "";
            foreach (Transform childTransform in ShopDetailsPanel.transform) childTransform.gameObject.SetActive(false);
            BuyPriceInt -= MinusTime3Price;
            MinusTime3Button.GetComponent<Image>().color = new Color32(208, 255, 177, 255);
            MinusTime3Flag = false;
        }
    }

    //RockSlowButtonボタンを押した時
    public void OnClick_RockSlowButton()
    {
        //SEの使用
        soundManager.SEManager("CharacterSelect_sound1");

        //trueであれば合計金額に追加
        if (RockSlowFlag == false)
        {
            //詳細表示
            ShopDetailsText.text = "ロックスロー\n\n岩の速度-5";
            //RockSlowのイメージ表示
            foreach (Transform childTransform in ShopDetailsPanel.transform) childTransform.gameObject.SetActive(false);
            UpdownImage.SetActive(true);
            //値段の追加
            BuyPriceInt += RockSlowPrice;
            //ボタンの色変更
            RockSlowButton.GetComponent<Image>().color = new Color32(238, 255, 227, 255);
            //合計金額に追加している状態
            RockSlowFlag = true;
        }
        else
        {
            ShopDetailsText.text = "";
            foreach (Transform childTransform in ShopDetailsPanel.transform) childTransform.gameObject.SetActive(false);
            BuyPriceInt -= RockSlowPrice;
            RockSlowButton.GetComponent<Image>().color = new Color32(208, 255, 177, 255);
            RockSlowFlag = false;
        }
    }

    //RockSlow2Buttonボタンを押した時
    public void OnClick_RockSlow2Button()
    {
        //SEの使用
        soundManager.SEManager("CharacterSelect_sound1");

        //trueであれば合計金額に追加
        if (RockSlow2Flag == false)
        {
            //詳細表示
            ShopDetailsText.text = "ロックスロー\n\n岩の速度-20";
            //MinusTimeのイメージ表示
            foreach (Transform childTransform in ShopDetailsPanel.transform) childTransform.gameObject.SetActive(false);
            UpdownImage.SetActive(true);
            //値段の追加
            BuyPriceInt += RockSlow2Price;
            //ボタンの色変更
            RockSlow2Button.GetComponent<Image>().color = new Color32(238, 255, 227, 255);
            //合計金額に追加している状態
            RockSlow2Flag = true;
        }
        else
        {
            ShopDetailsText.text = "";
            foreach (Transform childTransform in ShopDetailsPanel.transform) childTransform.gameObject.SetActive(false);
            BuyPriceInt -= RockSlow2Price;
            RockSlow2Button.GetComponent<Image>().color = new Color32(208, 255, 177, 255);
            RockSlow2Flag = false;
        }
    }

    //RockSlow3Buttonボタンを押した時
    public void OnClick_RockSlow3Button()
    {
        //SEの使用
        soundManager.SEManager("CharacterSelect_sound1");

        //trueであれば合計金額に追加
        if (RockSlow3Flag == false)
        {
            //詳細表示
            ShopDetailsText.text = "ロックスロー\n\n岩の速度-50";
            //MinusTimeのイメージ表示
            foreach (Transform childTransform in ShopDetailsPanel.transform) childTransform.gameObject.SetActive(false);
            UpdownImage.SetActive(true);
            //値段の追加
            BuyPriceInt += RockSlow3Price;
            //ボタンの色変更
            RockSlow3Button.GetComponent<Image>().color = new Color32(238, 255, 227, 255);
            //合計金額に追加している状態
            RockSlow3Flag = true;
        }
        else
        {
            ShopDetailsText.text = "";
            foreach (Transform childTransform in ShopDetailsPanel.transform) childTransform.gameObject.SetActive(false);
            BuyPriceInt -= RockSlow3Price;
            RockSlow3Button.GetComponent<Image>().color = new Color32(208, 255, 177, 255);
            RockSlow3Flag = false;
        }
    }

    //AirplaneSlowButtonボタンを押した時
    public void OnClick_AirplaneSlowButton()
    {
        //SEの使用
        soundManager.SEManager("CharacterSelect_sound1");

        //trueであれば合計金額に追加
        if (AirplaneSlowFlag == false)
        {
            //詳細表示
            ShopDetailsText.text = "ヒコーキスロー\n\n紙飛行機の速度-5";
            //AirplaneSlowのイメージ表示
            foreach (Transform childTransform in ShopDetailsPanel.transform) childTransform.gameObject.SetActive(false);
            UpdownImage.SetActive(true);
            //値段の追加
            BuyPriceInt += AirplaneSlowPrice;
            //ボタンの色変更
            AirplaneSlowButton.GetComponent<Image>().color = new Color32(238, 255, 227, 255);
            //合計金額に追加している状態
            AirplaneSlowFlag = true;
        }
        else
        {
            ShopDetailsText.text = "";
            foreach (Transform childTransform in ShopDetailsPanel.transform) childTransform.gameObject.SetActive(false);
            BuyPriceInt -= AirplaneSlowPrice;
            AirplaneSlowButton.GetComponent<Image>().color = new Color32(208, 255, 177, 255);
            AirplaneSlowFlag = false;
        }
    }

    //AirplaneSlow2Buttonボタンを押した時
    public void OnClick_AirplaneSlow2Button()
    {
        //SEの使用
        soundManager.SEManager("CharacterSelect_sound1");

        //trueであれば合計金額に追加
        if (AirplaneSlow2Flag == false)
        {
            //詳細表示
            ShopDetailsText.text = "ヒコーキスロー2\n\n紙飛行機の速度-20";
            //MinusTimeのイメージ表示
            foreach (Transform childTransform in ShopDetailsPanel.transform) childTransform.gameObject.SetActive(false);
            UpdownImage.SetActive(true);
            //値段の追加
            BuyPriceInt += AirplaneSlow2Price;
            //ボタンの色変更
            AirplaneSlow2Button.GetComponent<Image>().color = new Color32(238, 255, 227, 255);
            //合計金額に追加している状態
            AirplaneSlow2Flag = true;
        }
        else
        {
            ShopDetailsText.text = "";
            foreach (Transform childTransform in ShopDetailsPanel.transform) childTransform.gameObject.SetActive(false);
            BuyPriceInt -= AirplaneSlow2Price;
            AirplaneSlow2Button.GetComponent<Image>().color = new Color32(208, 255, 177, 255);
            AirplaneSlow2Flag = false;
        }
    }

    //AirplaneSlow3Buttonボタンを押した時
    public void OnClick_AirplaneSlow3Button()
    {
        //SEの使用
        soundManager.SEManager("CharacterSelect_sound1");

        //trueであれば合計金額に追加
        if (AirplaneSlow3Flag == false)
        {
            //詳細表示
            ShopDetailsText.text = "ヒコーキスロー3\n\n紙飛行機の速度-50";
            //MinusTimeのイメージ表示
            foreach (Transform childTransform in ShopDetailsPanel.transform) childTransform.gameObject.SetActive(false);
            UpdownImage.SetActive(true);
            //値段の追加
            BuyPriceInt += AirplaneSlow3Price;
            //ボタンの色変更
            AirplaneSlow3Button.GetComponent<Image>().color = new Color32(238, 255, 227, 255);
            //合計金額に追加している状態
            AirplaneSlow3Flag = true;
        }
        else
        {
            ShopDetailsText.text = "";
            foreach (Transform childTransform in ShopDetailsPanel.transform) childTransform.gameObject.SetActive(false);
            BuyPriceInt -= AirplaneSlow3Price;
            AirplaneSlow3Button.GetComponent<Image>().color = new Color32(208, 255, 177, 255);
            AirplaneSlow3Flag = false;
        }
    }

    /// <summary>
    /// ミッション
    /// </summary>
    //MissonAButtonボタンを押した時
    public void OnClick_MissionAButton()
    {
        //SEの使用
        soundManager.SEManager("CharacterSelect_sound1");

        //trueであれば合計金額に追加
        if (MissionAFlag == false)
        {
            //詳細表示
            ShopDetailsText.text = "ミッションA\n\n1度もジャンプしなければ、コイン+200";
            //RockSlowのイメージ表示
            foreach (Transform childTransform in ShopDetailsPanel.transform) childTransform.gameObject.SetActive(false);
            MissionImage.SetActive(true);
            //値段の追加
            BuyPriceInt += MissionAPrice;
            //ボタンの色変更
            MissionAButton.GetComponent<Image>().color = new Color32(238, 255, 227, 255);
            //合計金額に追加している状態
            MissionAFlag = true;
        }
        else
        {
            ShopDetailsText.text = "";
            foreach (Transform childTransform in ShopDetailsPanel.transform) childTransform.gameObject.SetActive(false);
            BuyPriceInt -= MissionAPrice;
            MissionAButton.GetComponent<Image>().color = new Color32(208, 255, 177, 255);
            MissionAFlag = false;
        }
    }

    //MissonAButtonボタンを押した時
    public void OnClick_MissionBButton()
    {
        //SEの使用
        soundManager.SEManager("CharacterSelect_sound1");

        //trueであれば合計金額に追加
        if (MissionBFlag == false)
        {
            //詳細表示
            ShopDetailsText.text = "ミッションB\n\n1度も左右に移動しなければ、コイン+150";
            //RockSlowのイメージ表示
            foreach (Transform childTransform in ShopDetailsPanel.transform) childTransform.gameObject.SetActive(false);
            MissionImage.SetActive(true);
            //値段の追加
            BuyPriceInt += MissionBPrice;
            //ボタンの色変更
            MissionBButton.GetComponent<Image>().color = new Color32(238, 255, 227, 255);
            //合計金額に追加している状態
            MissionBFlag = true;
        }
        else
        {
            ShopDetailsText.text = "";
            foreach (Transform childTransform in ShopDetailsPanel.transform) childTransform.gameObject.SetActive(false);
            BuyPriceInt -= MissionBPrice;
            MissionBButton.GetComponent<Image>().color = new Color32(208, 255, 177, 255);
            MissionBFlag = false;
        }
    }

    //TrampolineButtonボタンを押した時
    public void OnClick_TrampolineButton()
    {
        //SEの使用
        soundManager.SEManager("CharacterSelect_sound1");

        //trueであれば合計金額に追加
        if (TrampolineFlag == false)
        {
            //詳細表示
            ShopDetailsText.text = "トランポリン出現\n\nトランポリンがサポートアイテムとして出現する";
            //MinusTimeのイメージ表示
            foreach (Transform childTransform in ShopDetailsPanel.transform) childTransform.gameObject.SetActive(false);
            UpdownImage.SetActive(true);
            //値段の追加
            BuyPriceInt += TrampolinePrice;
            //ボタンの色変更
            TrampolineButton.GetComponent<Image>().color = new Color32(238, 255, 227, 255);
            //合計金額に追加している状態
            TrampolineFlag = true;
        }
        else
        {
            ShopDetailsText.text = "";
            foreach (Transform childTransform in ShopDetailsPanel.transform) childTransform.gameObject.SetActive(false);
            BuyPriceInt -= TrampolinePrice;
            TrampolineButton.GetComponent<Image>().color = new Color32(208, 255, 177, 255);
            TrampolineFlag = false;
        }
    }



    /// <summary>
    /// StageAssignmentボタン
    /// </summary>
    //Stage1Assignmentボタンを押した時
    public void OnClick_Stage1AssignmentButton()
    {
        //SEの使用
        soundManager.SEManager("CharacterSelect_sound1");

        //trueであれば合計金額に追加
        if (Stage1AssignmentFlag == false)
        {
            //その他のボタンの動き
            StageAssignmentButtonChange();
            //詳細表示
            ShopDetailsText.text = "ステージ1指定\n\nこの階は必ずステージ1でプレイできます";
            //Stage1Assignmentのイメージ表示
            foreach (Transform childTransform in ShopDetailsPanel.transform) childTransform.gameObject.SetActive(false);
            Stage1AssignmentImage.SetActive(true);
            //値段の追加
            BuyPriceInt += Stage1AssignmentPrice;
            //ボタンの色変更
            Stage1AssignmentButton.GetComponent<Image>().color = new Color32(238, 255, 227, 255);
            //合計金額に追加している状態
            Stage1AssignmentFlag = true;
        }
        else
        {
            ShopDetailsText.text = "";
            foreach (Transform childTransform in ShopDetailsPanel.transform) childTransform.gameObject.SetActive(false);
            BuyPriceInt -= Stage1AssignmentPrice;
            Stage1AssignmentButton.GetComponent<Image>().color = new Color32(208, 255, 177, 255);
            Stage1AssignmentFlag = false;
        }
    }

    //Stage2Assignmentボタンを押した時
    public void OnClick_Stage2AssignmentButton()
    {
        //SEの使用
        soundManager.SEManager("CharacterSelect_sound1");

        //trueであれば合計金額に追加
        if (Stage2AssignmentFlag == false)
        {
            //その他のボタンの動き
            StageAssignmentButtonChange();
            //詳細表示
            ShopDetailsText.text = "ステージ2指定\n\nこの階は必ずステージ2でプレイできます";
            //Stage2Assignmentのイメージ表示
            foreach (Transform childTransform in ShopDetailsPanel.transform) childTransform.gameObject.SetActive(false);
            Stage2AssignmentImage.SetActive(true);
            //値段の追加
            BuyPriceInt += Stage2AssignmentPrice;
            //ボタンの色変更
            Stage2AssignmentButton.GetComponent<Image>().color = new Color32(238, 255, 227, 255);
            //合計金額に追加している状態
            Stage2AssignmentFlag = true;
        }
        else
        {
            ShopDetailsText.text = "";
            foreach (Transform childTransform in ShopDetailsPanel.transform) childTransform.gameObject.SetActive(false);
            BuyPriceInt -= Stage2AssignmentPrice;
            Stage2AssignmentButton.GetComponent<Image>().color = new Color32(208, 255, 177, 255);
            Stage2AssignmentFlag = false;
        }
    }

    //Stage3Assignmentボタンを押した時
    public void OnClick_Stage3AssignmentButton()
    {
        //SEの使用
        soundManager.SEManager("CharacterSelect_sound1");

        //trueであれば合計金額に追加
        if (Stage3AssignmentFlag == false)
        {
            //その他のボタンの動き
            StageAssignmentButtonChange();
            //詳細表示
            ShopDetailsText.text = "ステージ3指定\n\nこの階は必ずステージ3でプレイできます";
            //Stage3Assignmentのイメージ表示
            foreach (Transform childTransform in ShopDetailsPanel.transform) childTransform.gameObject.SetActive(false);
            Stage3AssignmentImage.SetActive(true);
            //値段の追加
            BuyPriceInt += Stage3AssignmentPrice;
            //ボタンの色変更
            Stage3AssignmentButton.GetComponent<Image>().color = new Color32(238, 255, 227, 255);
            //合計金額に追加している状態
            Stage3AssignmentFlag = true;
        }
        else
        {
            ShopDetailsText.text = "";
            foreach (Transform childTransform in ShopDetailsPanel.transform) childTransform.gameObject.SetActive(false);
            BuyPriceInt -= Stage3AssignmentPrice;
            Stage3AssignmentButton.GetComponent<Image>().color = new Color32(208, 255, 177, 255);
            Stage3AssignmentFlag = false;
        }
    }

    //Stage4Assignmentボタンを押した時
    public void OnClick_Stage4AssignmentButton()
    {
        //SEの使用
        soundManager.SEManager("CharacterSelect_sound1");

        //trueであれば合計金額に追加
        if (Stage4AssignmentFlag == false)
        {
            //その他のボタンの動き
            StageAssignmentButtonChange();
            //詳細表示
            ShopDetailsText.text = "ステージ4指定\n\nこの階は必ずステージ4でプレイできます";
            //Stage4Assignmentのイメージ表示
            foreach (Transform childTransform in ShopDetailsPanel.transform) childTransform.gameObject.SetActive(false);
            Stage4AssignmentImage.SetActive(true);
            //値段の追加
            BuyPriceInt += Stage4AssignmentPrice;
            //ボタンの色変更
            Stage4AssignmentButton.GetComponent<Image>().color = new Color32(238, 255, 227, 255);
            //合計金額に追加している状態
            Stage4AssignmentFlag = true;
        }
        else
        {
            ShopDetailsText.text = "";
            foreach (Transform childTransform in ShopDetailsPanel.transform) childTransform.gameObject.SetActive(false);
            BuyPriceInt -= Stage4AssignmentPrice;
            Stage4AssignmentButton.GetComponent<Image>().color = new Color32(208, 255, 177, 255);
            Stage4AssignmentFlag = false;
        }
    }

    //Stage5Assignmentボタンを押した時
    public void OnClick_Stage5AssignmentButton()
    {
        //SEの使用
        soundManager.SEManager("CharacterSelect_sound1");

        //trueであれば合計金額に追加
        if (Stage5AssignmentFlag == false)
        {
            //その他のボタンの動き
            StageAssignmentButtonChange();
            //詳細表示
            ShopDetailsText.text = "ステージ5指定\n\nこの階は必ずステージ5でプレイできます";
            //Stage1Assignmentのイメージ表示
            foreach (Transform childTransform in ShopDetailsPanel.transform) childTransform.gameObject.SetActive(false);
            Stage5AssignmentImage.SetActive(true);
            //値段の追加
            BuyPriceInt += Stage5AssignmentPrice;
            //ボタンの色変更
            Stage5AssignmentButton.GetComponent<Image>().color = new Color32(238, 255, 227, 255);
            //合計金額に追加している状態
            Stage5AssignmentFlag = true;
        }
        else
        {
            ShopDetailsText.text = "";
            foreach (Transform childTransform in ShopDetailsPanel.transform) childTransform.gameObject.SetActive(false);
            BuyPriceInt -= Stage5AssignmentPrice;
            Stage5AssignmentButton.GetComponent<Image>().color = new Color32(208, 255, 177, 255);
            Stage5AssignmentFlag = false;
        }
    }

    //Stage6Assignmentボタンを押した時
    public void OnClick_Stage6AssignmentButton()
    {
        //SEの使用
        soundManager.SEManager("CharacterSelect_sound1");

        //trueであれば合計金額に追加
        if (Stage6AssignmentFlag == false)
        {
            //その他のボタンの動き
            StageAssignmentButtonChange();
            //詳細表示
            ShopDetailsText.text = "ステージ6指定\n\nこの階は必ずステージ6でプレイできます";
            //Stage1Assignmentのイメージ表示
            foreach (Transform childTransform in ShopDetailsPanel.transform) childTransform.gameObject.SetActive(false);
            Stage6AssignmentImage.SetActive(true);
            //値段の追加
            BuyPriceInt += Stage6AssignmentPrice;
            //ボタンの色変更
            Stage6AssignmentButton.GetComponent<Image>().color = new Color32(238, 255, 227, 255);
            //合計金額に追加している状態
            Stage6AssignmentFlag = true;
        }
        else
        {
            ShopDetailsText.text = "";
            foreach (Transform childTransform in ShopDetailsPanel.transform) childTransform.gameObject.SetActive(false);
            BuyPriceInt -= Stage6AssignmentPrice;
            Stage6AssignmentButton.GetComponent<Image>().color = new Color32(208, 255, 177, 255);
            Stage6AssignmentFlag = false;
        }
    }

    //StageAssignmentボタンにおける連動しての変更
    private void StageAssignmentButtonChange()
    {
        if(Stage1AssignmentFlag == true)
        {
            BuyPriceInt -= Stage1AssignmentPrice;
            Stage1AssignmentButton.GetComponent<Image>().color = new Color32(208, 255, 177, 255);
            Stage1AssignmentFlag = false;
        }
        else if (Stage2AssignmentFlag == true)
        {
            BuyPriceInt -= Stage2AssignmentPrice;
            Stage2AssignmentButton.GetComponent<Image>().color = new Color32(208, 255, 177, 255);
            Stage2AssignmentFlag = false;
        }
        else if (Stage3AssignmentFlag == true)
        {
            BuyPriceInt -= Stage3AssignmentPrice;
            Stage3AssignmentButton.GetComponent<Image>().color = new Color32(208, 255, 177, 255);
            Stage3AssignmentFlag = false;
        }
        else if (Stage4AssignmentFlag == true)
        {
            BuyPriceInt -= Stage4AssignmentPrice;
            Stage4AssignmentButton.GetComponent<Image>().color = new Color32(208, 255, 177, 255);
            Stage4AssignmentFlag = false;
        }
        else if (Stage5AssignmentFlag == true)
        {
            BuyPriceInt -= Stage5AssignmentPrice;
            Stage5AssignmentButton.GetComponent<Image>().color = new Color32(208, 255, 177, 255);
            Stage5AssignmentFlag = false;
        }
        else if (Stage6AssignmentFlag == true)
        {
            BuyPriceInt -= Stage6AssignmentPrice;
            Stage6AssignmentButton.GetComponent<Image>().color = new Color32(208, 255, 177, 255);
            Stage6AssignmentFlag = false;
        }
    }

    /// <summary>
    /// AnimalAddボタン
    /// </summary>
    //DogAddボタンを押した時
    public void OnClick_DogAddButton()
    {
        //SEの使用
        soundManager.SEManager("CharacterSelect_sound1");

        //trueであれば合計金額に追加
        if (DogAddFlag == false)
        {
            //詳細表示
            ShopDetailsText.text = "犬を誘う\n\nサポートキャラとして犬を追加\n※サポートキャラは2キャラまで";
            //DogAddのイメージ表示
            foreach (Transform childTransform in ShopDetailsPanel.transform) childTransform.gameObject.SetActive(false);
            DogAddImage.SetActive(true);
            //値段の追加
            BuyPriceInt += DogAddPrice;
            //ボタンの色変更
            DogAddButton.GetComponent<Image>().color = new Color32(238, 255, 227, 255);
            //合計金額に追加している状態
            DogAddFlag = true;
        }
        else
        {
            ShopDetailsText.text = "";
            foreach (Transform childTransform in ShopDetailsPanel.transform) childTransform.gameObject.SetActive(false);
            BuyPriceInt -= DogAddPrice;
            DogAddButton.GetComponent<Image>().color = new Color32(208, 255, 177, 255);
            DogAddFlag = false;
        }
    }

    //GiraffeAddボタンを押した時
    public void OnClick_GiraffeAddButton()
    {
        //SEの使用
        soundManager.SEManager("CharacterSelect_sound1");

        //trueであれば合計金額に追加
        if (GiraffeAddFlag == false)
        {
            //詳細表示
            ShopDetailsText.text = "キリンを誘う\n\nサポートキャラとしてキリンを追加\n※サポートキャラは2キャラまで";
            //DogAddのイメージ表示
            foreach (Transform childTransform in ShopDetailsPanel.transform) childTransform.gameObject.SetActive(false);
            GiraffeAddImage.SetActive(true);
            //値段の追加
            BuyPriceInt += GiraffeAddPrice;
            //ボタンの色変更
            GiraffeAddButton.GetComponent<Image>().color = new Color32(238, 255, 227, 255);
            //合計金額に追加している状態
            GiraffeAddFlag = true;
        }
        else
        {
            ShopDetailsText.text = "";
            foreach (Transform childTransform in ShopDetailsPanel.transform) childTransform.gameObject.SetActive(false);
            BuyPriceInt -= GiraffeAddPrice;
            GiraffeAddButton.GetComponent<Image>().color = new Color32(208, 255, 177, 255);
            GiraffeAddFlag = false;
        }
    }

    //ElephantAddボタンを押した時
    public void OnClick_ElephantAddButton()
    {
        //SEの使用
        soundManager.SEManager("CharacterSelect_sound1");

        //trueであれば合計金額に追加
        if (ElephantAddFlag == false)
        {
            //詳細表示
            ShopDetailsText.text = "ゾウを誘う\n\nサポートキャラとしてゾウを追加\n※サポートキャラは2キャラまで";
            //DogAddのイメージ表示
            foreach (Transform childTransform in ShopDetailsPanel.transform) childTransform.gameObject.SetActive(false);
            ElephantAddImage.SetActive(true);
            //値段の追加
            BuyPriceInt += ElephantAddPrice;
            //ボタンの色変更
            ElephantAddButton.GetComponent<Image>().color = new Color32(238, 255, 227, 255);
            //合計金額に追加している状態
            ElephantAddFlag = true;
        }
        else
        {
            ShopDetailsText.text = "";
            foreach (Transform childTransform in ShopDetailsPanel.transform) childTransform.gameObject.SetActive(false);
            BuyPriceInt -= ElephantAddPrice;
            ElephantAddButton.GetComponent<Image>().color = new Color32(208, 255, 177, 255);
            ElephantAddFlag = false;
        }
    }

    //TigerAddボタンを押した時
    public void OnClick_TigerAddButton()
    {
        //SEの使用
        soundManager.SEManager("CharacterSelect_sound1");

        //trueであれば合計金額に追加
        if (TigerAddFlag == false)
        {
            //詳細表示
            ShopDetailsText.text = "トラを誘う\n\nサポートキャラとしてトラを追加\n※サポートキャラは2キャラまで";
            //TigerAddのイメージ表示
            foreach (Transform childTransform in ShopDetailsPanel.transform) childTransform.gameObject.SetActive(false);
            TigerAddImage.SetActive(true);
            //値段の追加
            BuyPriceInt += TigerAddPrice;
            //ボタンの色変更
            TigerAddButton.GetComponent<Image>().color = new Color32(238, 255, 227, 255);
            //合計金額に追加している状態
            TigerAddFlag = true;
        }
        else
        {
            ShopDetailsText.text = "";
            foreach (Transform childTransform in ShopDetailsPanel.transform) childTransform.gameObject.SetActive(false);
            BuyPriceInt -= TigerAddPrice;
            TigerAddButton.GetComponent<Image>().color = new Color32(208, 255, 177, 255);
            TigerAddFlag = false;
        }
    }

    //CatAddボタンを押した時
    public void OnClick_CatAddButton()
    {
        //SEの使用
        soundManager.SEManager("CharacterSelect_sound1");

        //trueであれば合計金額に追加
        if (CatAddFlag == false)
        {
            //詳細表示
            ShopDetailsText.text = "ねこを誘う\n\nサポートキャラとしてねこを追加\n※サポートキャラは2キャラまで";
            //CatAddのイメージ表示
            foreach (Transform childTransform in ShopDetailsPanel.transform) childTransform.gameObject.SetActive(false);
            CatAddImage.SetActive(true);
            //値段の追加
            BuyPriceInt += CatAddPrice;
            //ボタンの色変更
            CatAddButton.GetComponent<Image>().color = new Color32(238, 255, 227, 255);
            //合計金額に追加している状態
            CatAddFlag = true;
        }
        else
        {
            ShopDetailsText.text = "";
            foreach (Transform childTransform in ShopDetailsPanel.transform) childTransform.gameObject.SetActive(false);
            BuyPriceInt -= CatAddPrice;
            CatAddButton.GetComponent<Image>().color = new Color32(208, 255, 177, 255);
            CatAddFlag = false;
        }
    }

    //RabbitAddボタンを押した時
    public void OnClick_RabbitAddButton()
    {
        //SEの使用
        soundManager.SEManager("CharacterSelect_sound1");

        //trueであれば合計金額に追加
        if (RabbitAddFlag == false)
        {
            //詳細表示
            ShopDetailsText.text = "ウサギを誘う\n\nサポートキャラとしてウサギを追加\n※サポートキャラは2キャラまで";
            //DogAddのイメージ表示
            foreach (Transform childTransform in ShopDetailsPanel.transform) childTransform.gameObject.SetActive(false);
            RabbitAddImage.SetActive(true);
            //値段の追加
            BuyPriceInt += RabbitAddPrice;
            //ボタンの色変更
            RabbitAddButton.GetComponent<Image>().color = new Color32(238, 255, 227, 255);
            //合計金額に追加している状態
            RabbitAddFlag = true;
        }
        else
        {
            ShopDetailsText.text = "";
            foreach (Transform childTransform in ShopDetailsPanel.transform) childTransform.gameObject.SetActive(false);
            BuyPriceInt -= RabbitAddPrice;
            RabbitAddButton.GetComponent<Image>().color = new Color32(208, 255, 177, 255);
            RabbitAddFlag = false;
        }
    }

    /// <summary>
    /// 次のシーンへ進むボタン
    /// </summary>
    //TeppenBattleSceneへすすむ
    public void OnClick_CloseButton()
    {
        //SEの使用
        soundManager.SEManager("Button_sound1");

        if (PlayerPrefs.GetInt("myCoin") >= BuyPriceInt)
        {
            //購入によるお金の減少
            PlayerPrefs.SetInt("myCoin", PlayerPrefs.GetInt("myCoin") - BuyPriceInt);
            //購入アイテム
            //DShoes
            if (DShoesFlag == true) PlayerPrefs.SetInt("DShoesFlag", 1);
            if (DShoes2Flag == true) PlayerPrefs.SetInt("DShoes2Flag", 1);
            if (DShoes3Flag == true) PlayerPrefs.SetInt("DShoes3Flag", 1);
            //JShoes
            if (JShoesFlag == true) PlayerPrefs.SetInt("JShoesFlag", 1);
            if (JShoes2Flag == true) PlayerPrefs.SetInt("JShoes2Flag", 1);
            if (JShoes3Flag == true) PlayerPrefs.SetInt("JShoes3Flag", 1);
            //AShoes
            if (AShoesFlag == true) PlayerPrefs.SetInt("AShoesFlag", 1);
            if (AShoes2Flag == true) PlayerPrefs.SetInt("AShoes2Flag", 1);
            if (AShoes3Flag == true) PlayerPrefs.SetInt("AShoes3Flag", 1);
            //SuperHand
            if (SuperHandFlag == true) PlayerPrefs.SetInt("SuperHandFlag", 1);
            //JWing
            if (JWingFlag == true) PlayerPrefs.SetInt("JWingFlag", 1);
            //RockSlow
            if (RockSlowFlag == true) RockSlowRealTotal += 5.0f;
            if (RockSlow2Flag == true) RockSlowRealTotal += 20.0f;
            if (RockSlow3Flag == true) RockSlowRealTotal += 50.0f;
            //AirplaneSlow
            if (AirplaneSlowFlag == true) AirplaneSlowRealTotal += 5.0f;
            if (AirplaneSlow2Flag == true) AirplaneSlowRealTotal += 20.0f;
            if (AirplaneSlow3Flag == true) AirplaneSlowRealTotal += 50.0f;
            //MinusTime
            if (MinusTimeFlag == true) MinusTimeRealTotal += 5.0f;
            if (MinusTime2Flag == true) MinusTimeRealTotal += 20.0f;
            if (MinusTime3Flag == true) MinusTimeRealTotal += 50.0f;
            //ミッション
            if (MissionAFlag == true) MissionASuccessFlag = true;
            if (MissionBFlag == true) MissionBSuccessFlag = true;
            //トランポリン
            if (TrampolineFlag == true) TrampolineRealFlag = true;
            //ステージ指定
            if (Stage1AssignmentFlag == true) stageAssignmentNumber = 1;
            else if (Stage2AssignmentFlag == true) stageAssignmentNumber = 2;
            else if (Stage3AssignmentFlag == true) stageAssignmentNumber = 3;
            else if (Stage4AssignmentFlag == true) stageAssignmentNumber = 4;
            else if (Stage5AssignmentFlag == true) stageAssignmentNumber = 5;
            else if (Stage6AssignmentFlag == true) stageAssignmentNumber = 6;
            //キャラクター変更
            if (DogAddFlag == true) PlayerPrefs.SetInt("TeppenDogCanUse", 1);
            if (GiraffeAddFlag == true) PlayerPrefs.SetInt("TeppenGiraffeCanUse", 1);
            if (ElephantAddFlag == true) PlayerPrefs.SetInt("TeppenElephantCanUse", 1);
            if (TigerAddFlag == true) PlayerPrefs.SetInt("TeppenTigerCanUse", 1);
            if (CatAddFlag == true) PlayerPrefs.SetInt("TeppenCatCanUse", 1);
            if (RabbitAddFlag == true) PlayerPrefs.SetInt("TeppenRabbitCanUse", 1);

            //画面回転の制御
            if (Screen.width > Screen.height) Screen.autorotateToPortrait = false;
            else
            {
                Screen.autorotateToLandscapeRight = false;
                Screen.autorotateToLandscapeLeft = false;
            }

            //キャラクターリスト取得
            characterList.Clear();
            CharacterList();

            //シーン移動
            //TeppenShopCanvas.SetActive(false);
            //TeppenBattleScene.SetActive(true);
            //SceneManager.LoadScene("TeppenBattleScene");
            teppenMenuUI.async_TeppenBattleScene.allowSceneActivation = true;
        }
        else
        {

        }
        
    }
}
