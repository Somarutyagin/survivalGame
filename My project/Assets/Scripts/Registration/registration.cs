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
using Task = System.Threading.Tasks.Task;

public class registration : MonoBehaviour
{
    private const string RegistrationDoneCheckKey = "RegistrationDone";

    private Color txtColorDefault;

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
        UserDataList UserDataList_ = LoadRegistrationInfo();

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
    }

    public void ConfirmRegistration()
    {
        bool isValidLogin = !string.IsNullOrEmpty(inputLogin.text);

        UserDataList UserDataList_ = LoadRegistrationInfo();
        if (UserDataList_.Users != null)
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
        }
    }
    private void SaveRegistrationInfo(bool isQuit)
    {
        int record = 0;
        if (isQuit)
            record = GameManager.Instance.record;
        else
            record = 0;

        UserDataList UserDataList_ = LoadRegistrationInfo();

        UserData userData = new UserData
        {
            Login = inputLogin.text,
            Password = inputPassword.text,
            PhoneNumber = inputPhoneNumber.text,
            Email = inputEmail.text,
            Record = record
        };
        if (UserDataList_.Users != null)
            UserDataList_.Users.Add(userData);
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
        string json = JsonUtility.ToJson(UserDataList_, true);

        File.WriteAllText("userData.json", json);
        /*
        Task save = new Task(() =>
        {
            string json = JsonUtility.ToJson(UserDataList_);

            File.WriteAllText("userData.json", json);
        });
        
        var prod = Task.Run(() => Prod(new double[10]));
        var prod1 = Task.Run(() => Prod(new double[10]));
        var prod2 = Task.Run(() => Prod(new double[10]));
        var prod3 = Task.Run(() => Prod(new double[10]));

        var allTask = Task.WhenAll(prod, prod1, prod2, prod3);

        allTask.ContinueWith(t => Debug.Log("¬ывод на экран"));

        save.Start();
        */
    }

    private UserDataList LoadRegistrationInfo()
    {
        string json = File.ReadAllText("userData.json");

        return JsonUtility.FromJson<UserDataList>(json);
    }

    private void OnApplicationQuit()
    {
        //SaveRegistrationInfo(true);
    }
}
