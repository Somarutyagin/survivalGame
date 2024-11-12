using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;
#if UNITY_IOS
using UnityEngine.iOS;
#endif
#if UNITY_ANDROID
using Google.Play.Review;
#endif

public class GameManager1 : MonoBehaviour
{
    private const string lastTab = "lastTab";

    [SerializeField] private GameObject stateHand;
    /*
    [Header("Debug")]
    [SerializeField] private bool Debug = false;
    [SerializeField] private bool DeleteSaveOnAwake = false;
    [SerializeField] private bool upAgeOnButtonAgePress = false;
    [SerializeField] private Button testAgeButton;
    [Space]
    */
#if UNITY_ANDROID
    // Create instance of ReviewManager
    private ReviewManager _reviewManager;
    private PlayReviewInfo _playReviewInfo;
#endif

    public GameObject Ficon;

    public GameObject tasksDisplay;
    public GameObject talentsDisplay;

    public AudioMixer musicMix;
    public AudioMixer soundMix;
    public AudioSource cryBabySound, happyBabySound, cryBabySitSound, renovationSound;



    public GameObject statePanel;
    public GameObject stateExit;
    public GameObject buffList;



    public GameObject eventPanel;
    public GameObject eventbtn1;

    public GameObject gameOverDisplay;
    //public GameObject restartBtn;

    public GameObject NotEnoughMoneyDisplay;
    public GameObject NotEnoughMoneyExitBtn;
    public GameObject BuyMoreBtn;

    public GameObject nameDisplay;
    public GameObject changeNameDisplay;
    public Text name;
    public InputField inputField;
    public InputField inputField1;

    public GameObject mainCanvas;
    public GameObject mainCanvasSup;
    public GameObject casinoCanvas;

    public GameObject blackout;
    public GameObject blackout1;
    public GameObject blackout2;
    public GameObject blackout3;

    public GameObject settingsDisplay;
    //public GameObject termsDisplay;
    //public GameObject privasyDisplay;
    public GameObject musicOff;
    public GameObject musicOn;
    public GameObject soundOff;
    public GameObject soundOn;
    public GameObject notifOff;
    public GameObject notifOn;

    public GameObject hintBunnyLeisure;
    public GameObject hintCommode;
    public GameObject hintNewWallsLemonTree;
    public GameObject hintNewWallsWardrobe;
    public GameObject hintNewWallsLamp;
    public GameObject hintNewWallsToyBasket;
    public GameObject hintNewWallsToyBasket1;
    public GameObject hintNewWallsFrame;
    public GameObject hintLandPlot_;
    public GameObject hintNewWallsShelf;
    public GameObject hintShelfBook1;
    public GameObject hintShelfBook2;
    public GameObject hintShelfBook3;
    public GameObject hintShelfBunny;
    public GameObject hintBasket;
    public GameObject hintWardrobeSocks;
    public GameObject hintWardrobeT_shirt;
    public GameObject hintArmchair;
    public GameObject hintRugBlocks;
    public GameObject hintRugPyramid;
    public GameObject hintSpoon1;
    public GameObject hintSpoon2;
    public GameObject hintSpoon3;
    public GameObject hintSpoon4;
    public GameObject hintPlate1;
    public GameObject hintPlate2;
    public GameObject hintPlate3;
    public GameObject hintBlender1;
    public GameObject hintBlender2;

    [SerializeField] private List<GameObject> hint3StageList = new List<GameObject>();
    private List<GameObject> hint2StageList = new List<GameObject>();
    private List<GameObject> hint1StageList = new List<GameObject>();

    private string blenderBuy = "blenderBuy",
        plateBuy = "plateBuy",
        spoonBuy = "spoonBuy";

    //public GameObject babyAnim;

    private void Awake()
    {
    }

    private void Update()
    {
    }



    private void Start()
    {
        //если включен звук
        if (PlayerPrefs.GetInt("sound") != 1)
        {
            soundMix.SetFloat("volumeSound", 0f);
            soundOn.SetActive(true);
            soundOff.SetActive(false);
        }
        //если выключен звук
        else
        {
            soundMix.SetFloat("volumeSound", -80f);
            soundOff.SetActive(true);
            soundOn.SetActive(false);
        }

        //если включена музыка
        if (PlayerPrefs.GetInt("music") != 1)
        {
            musicMix.SetFloat("volumeMusic", 0f);
            musicOn.SetActive(true);
            musicOff.SetActive(false);
        }
        //если выключена музыка
        else
        {
            musicMix.SetFloat("volumeMusic", -80f);
            musicOff.SetActive(true);
            musicOn.SetActive(false);
        }

        name.text = PlayerPrefs.GetString("name");
        if (name.text == "")
        {
            nameDisplay.SetActive(true);
            blackout2.SetActive(true);
        }
        else
            nameDisplay.SetActive(false);

        inputField.onEndEdit.AddListener(SubmitName);
        inputField1.onEndEdit.AddListener(SubmitName);
    }

    public void SendMail()
    {
        Application.OpenURL("mailto:babyhostmaster@gmail.com");
    }


    private void SubmitName(string arg0)
    {
        Color newCol = new Color(1f, 1f, 1f, 1f);
        ColorBlock cb = inputField.colors;
        cb.normalColor = newCol;
        inputField.colors = cb;

        cb = inputField1.colors;
        cb.normalColor = newCol;
        inputField1.colors = cb;
        print(cb.normalColor);
    }

    public void renameEnter()
    {
        blackout2.SetActive(true);
        blackout3.SetActive(true);

        if (name.text == "")
        {
            nameDisplay.SetActive(true);
        }
        else
        {
            changeNameDisplay.SetActive(true);
        }

        //babyAnim.SetActive(false);
    }

    public void renameExit()
    {
        inputField.text = "";

        Color newCol = new Color(0.674509804f, 0.447058824f, 0.301960784f, 1f);
        ColorBlock cb = inputField.colors;
        cb.normalColor = newCol;
        inputField.colors = cb;

        cb = inputField1.colors;
        cb.normalColor = newCol;
        inputField1.colors = cb;
        print(cb.normalColor);
    }

    public void NameSave()
    {
        if (inputField1.text != "")
        {
            PlayerPrefs.SetString("name", inputField1.text);
            name.text = PlayerPrefs.GetString("name");
            inputField1.text = "";

            nameDisplay.SetActive(false);
            blackout.SetActive(false);
            blackout1.SetActive(false);
            blackout2.SetActive(false);
            blackout3.SetActive(false);

            if (PlayerPrefs.GetInt("Stage") == 1)
            {
                tasksDisplay.SetActive(true);
            }

            Color newCol = new Color(0.674509804f, 0.447058824f, 0.301960784f, 1f);
            ColorBlock cb = inputField.colors;
            cb.normalColor = newCol;
            inputField.colors = cb;

            cb = inputField1.colors;
            cb.normalColor = newCol;
            inputField1.colors = cb;

            //babyAnim.SetActive(true);
        }
    }
    public void changeName()
    {
        if (inputField.text != "")
        {
            if (PlayerPrefs.GetInt("Stage", 1) == 1)
            {
                
                    changeNameDisplay.SetActive(false);
            }
            inputField.text = "";

            Color newCol = new Color(0.674509804f, 0.447058824f, 0.301960784f, 1f);
            ColorBlock cb = inputField.colors;
            cb.normalColor = newCol;
            inputField.colors = cb;

            cb = inputField1.colors;
            cb.normalColor = newCol;
            inputField1.colors = cb;
            print(cb.normalColor);
        }
    }

}
