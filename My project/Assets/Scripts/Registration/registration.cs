using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static UnityEngine.UIElements.UxmlAttributeDescription;
using Task = System.Threading.Tasks.Task;

public class registration : MonoBehaviour
{
    private const string RegistrationDoneCheckKey = "RegistrationDone";

    private Color txtColorDefault;

    [SerializeField] private spawnManager spawnManager;

    [SerializeField] private Transform enemyPool;
    [SerializeField] private Text hpTxt;
    [SerializeField] private Text scoreTxt;


    [SerializeField] private leaderBoard leaderBoard_;
    [SerializeField] private GameObject registrationDisplay;
    [SerializeField] private GameObject signInDisplay;

    [SerializeField] private Text txtLogin;
    [SerializeField] private Text txtPassword;
    [SerializeField] private Text txtRepeatPassword;
    [SerializeField] private Text txtEmail;
    [SerializeField] private Text txtPhoneNumber;

    [SerializeField] private InputField inputLogin;
    [SerializeField] private InputField inputPassword;
    [SerializeField] private InputField inputRepeatPassword;
    [SerializeField] private InputField inputEmail;
    [SerializeField] private InputField inputPhoneNumber;

    [SerializeField] private Text txtLoginSignIn;
    [SerializeField] private Text txtPasswordSignIn;

    [SerializeField] private InputField inputLoginSignIn;
    [SerializeField] private InputField inputPasswordSignIn;

    private UserDataList UserDataList_;
    public string activeUser;
    private bool isFirstStart = true;

    private bool IsValidEmail(string email)
    {
        // –егул€рное выражение дл€ проверки формата электронной почты
        // string pattern =;
        return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
    }
    private bool IsValidPhoneNumber(string phoneNumber)
    {
        // –егул€рное выражение дл€ проверки формата номера телефона
        //string pattern = ;
        return Regex.IsMatch(phoneNumber, @"^\+?\d{1,3}\s?\(?\d{1,4}?\)?[\s-]?\d{1,4}[\s-]?\d{2,4}[\s-]?\d{2,4}$");
    }
    private bool IsValidPassword(string password)
    {
        // –егул€рное выражение дл€ проверки формата парол€
        //string pattern = ;
        return Regex.IsMatch(password, @"^(?=.{4,10}$)(?!.*\s).*$");
    }

    private void Start()
    {
        if (PlayerPrefs.GetInt(RegistrationDoneCheckKey, 0) != 1)
        {
            registrationDisplay.SetActive(true);
            signInDisplay.SetActive(false);
        }
        else
        {
            registrationDisplay.SetActive(false);
            signInDisplay.SetActive(true);
        }

        txtColorDefault = txtLogin.color;
    }

    public void BtnSignIn()
    {
        signInDisplay.SetActive(true);
        registrationDisplay.SetActive(false);
    }
    public void BtnSignUp()
    {
        registrationDisplay.SetActive(true);
        signInDisplay.SetActive(false);
    }

    public void ConfirmSignIn()
    {
        bool isValidSignIn = false;
        UserDataList_ = LoadRegistrationInfo();

        for (int i = 0; i < UserDataList_.Users.Count; i++)
        {
            if (UserDataList_.Users[i].Login == inputLoginSignIn.text && UserDataList_.Users[i].Password == inputPasswordSignIn.text)
            {
                isValidSignIn = true;
                GameManager.Instance.record = UserDataList_.Users[i].Record;
            }
        }

        if (!isValidSignIn)
        {
            txtLoginSignIn.color = Color.red;
            txtPasswordSignIn.color = Color.red;
        }
        else
        {
            signInDisplay.SetActive(false);
        }
        AuthEnd(inputLoginSignIn.text);

        if (UserDataList_ != null && UserDataList_.Users != null)
            leaderBoardListsUpdate();
    }

    public void ConfirmRegistration()
    {
        bool isValidLogin = !string.IsNullOrEmpty(inputLogin.text);

        UserDataList UserDataList_ = LoadRegistrationInfo();
        if (UserDataList_ != null && UserDataList_.Users != null)
        {
            for (int i = 0; i < UserDataList_.Users.Count; i++)
            {
                if (UserDataList_.Users[i].Login == inputLogin.text)
                    isValidLogin = false;
            }
        }
        bool isValidEmail = IsValidEmail(inputEmail.text);
        bool isValidPhoneNumber = IsValidPhoneNumber(inputPhoneNumber.text);
        bool isValidPassword = IsValidPassword(inputPassword.text);
        bool isValidRepeatPassword = inputPassword.text == inputRepeatPassword.text;
        bool isValidRegistration = isValidEmail && isValidPhoneNumber && isValidPassword && isValidRepeatPassword && isValidLogin;

        txtLogin.color = isValidLogin ? txtColorDefault : Color.red;
        txtEmail.color = isValidEmail ? txtColorDefault : Color.red;
        txtPhoneNumber.color = isValidPhoneNumber ? txtColorDefault : Color.red;
        txtPassword.color = isValidPassword ? txtColorDefault : Color.red;
        txtRepeatPassword.color = isValidRepeatPassword ? txtColorDefault : Color.red;

        if (isValidRegistration)
        {
            SaveRegistrationInfo(false);

            PlayerPrefs.SetInt(RegistrationDoneCheckKey, 1);
            registrationDisplay.SetActive(false);
            AuthEnd(inputLogin.text);
        }
    }

    private void AuthEnd(string activeLogin)
    {
        activeUser = activeLogin;
    }

    private void leaderBoardListsUpdate()
    {
        leaderBoard_.players.Clear();
        leaderBoard_.records.Clear();

        for (int i = 0; i < UserDataList_.Users.Count; i++)
        {
            leaderBoard_.players.Add(UserDataList_.Users[i].Login);
            leaderBoard_.records.Add(UserDataList_.Users[i].Record);
        }

        leaderBoard_.LeaderBoardUpdate();
    }

    public void SaveRegistrationInfo(bool isQuit)
    {
        int record = 0;
        if (isQuit)
            record = GameManager.Instance.record;
        else
            record = 0;

        UserDataList_ = LoadRegistrationInfo();
        List<UserData> UsersTemp = new List<UserData> { };
        UserDataList userDataListTemp = new UserDataList
        {
            Users = UsersTemp
        };

        UserData userData = new UserData
        {
            Login = inputLogin.text,
            Password = inputPassword.text,
            PhoneNumber = inputPhoneNumber.text,
            Email = inputEmail.text,
            Record = record,

            enemyCount = 0,
            hpCount = 0,
            score = 0
        };
        if (isQuit)
        {
            for (int i = 0; i < UserDataList_.Users.Count; i++)
            {
                if (UserDataList_.Users[i].Login == activeUser)
                {
                    userData = new UserData
                    {
                        Login = UserDataList_.Users[i].Login,
                        Password = UserDataList_.Users[i].Password,
                        PhoneNumber = UserDataList_.Users[i].PhoneNumber,
                        Email = UserDataList_.Users[i].Email,
                        Record = record,

                        enemyCount = enemyPool.childCount,
                        hpCount = int.Parse(hpTxt.text),
                        score = int.Parse(scoreTxt.text)
                    };
                }
                else
                {
                    userDataListTemp.Users.Add(UserDataList_.Users[i]);
                }
            }
        }
        if (UserDataList_ != null && UserDataList_.Users != null && !isQuit)
            UserDataList_.Users.Add(userData);
        else if (UserDataList_ != null && UserDataList_.Users != null && isQuit)
        {
            UserDataList_ = userDataListTemp;
            UserDataList_.Users.Add(userData);
        }
        else
        {
            UserDataList_ = new UserDataList
            {
                Users = new List<UserData>
                {
                    userData
                }
            };
        }
        Task save = new Task(() =>
        {
            string json = JsonUtility.ToJson(UserDataList_);

            File.WriteAllText("userData.json", json);
        });
        
        /*
        var prod = Task.Run(() => Prod(new double[10]));
        var prod1 = Task.Run(() => Prod(new double[10]));
        var prod2 = Task.Run(() => Prod(new double[10]));
        var prod3 = Task.Run(() => Prod(new double[10]));

        var allTask = Task.WhenAll(prod, prod1, prod2, prod3);

        allTask.ContinueWith(t => Debug.Log("¬ывод на экран"));
        */

        save.Start();
        leaderBoardListsUpdate();
    }

    public void loadGameInfo()
    {
        int enemyCount_ = 0;
        int hpCount_ = 0;
        int score_ = 0;
        if (UserDataList_ != null && UserDataList_.Users != null)
        {
            for (int i = 0; i < UserDataList_.Users.Count; i++)
            {
                if (UserDataList_.Users[i].Login == activeUser)
                {
                    enemyCount_ = UserDataList_.Users[i].enemyCount;
                    hpCount_ = UserDataList_.Users[i].hpCount;
                    score_ = UserDataList_.Users[i].score;
                }
            }
        }
        if (isFirstStart)
        {
            for (int i = 0; i < enemyCount_; i++)
            {
                spawnManager.SpawnEnemy();
            }
            hpTxt.transform.parent.parent.GetComponent<playerConfig>().hp = hpCount_;
            GameManager.Instance.score = score_;
        }
        GameManager.Instance.activeRecord = GameManager.Instance.record;

        isFirstStart = false;
    }


    private UserDataList LoadRegistrationInfo()
    {
        string json = File.ReadAllText("userData.json");

        return JsonUtility.FromJson<UserDataList>(json);
    }

    private void OnApplicationQuit()
    {
        SaveRegistrationInfo(true);
    }
}
