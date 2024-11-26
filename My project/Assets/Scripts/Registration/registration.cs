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

    public void ConfirmSignIn()
    {
        UserData userData = LoadRegistrationInfo();
        GameManager.Instance.record = userData.Record;

        bool isValidLogin = userData.Login.Equals(inputLoginSignIn.text);
        bool isValidPassword = userData.Password.Equals(inputPasswordSignIn.text);
        bool isValidSignIn = isValidLogin && isValidPassword;

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
        bool isValidEmail = IsValidEmail(inputEmail.text);
        bool isValidPhoneNumber = IsValidPhoneNumber(inputPhoneNumber.text);
        bool isValidPassword = IsValidPassword(inputPassword.text);
        bool isValidRepeatPassword = inputPassword.text == inputRepeatPassword.text;
        bool isValidRegistration = isValidEmail && isValidPhoneNumber && isValidPassword && isValidRepeatPassword && isValidLogin;

        txtLogin.color = isValidLogin ? txtColorDefault : Color.red;

        if (!isValidEmail)
            txtEmail.color = Color.red;
        else
            txtEmail.color = txtColorDefault;

        if (!isValidPhoneNumber)
            txtPhoneNumber.color = Color.red;
        else
            txtPhoneNumber.color = txtColorDefault;

        if (!isValidPassword)
            txtPassword.color = Color.red;
        else
            txtPassword.color = txtColorDefault;

        if (!isValidRepeatPassword)
            txtRepeatPassword.color = Color.red;
        else
            txtRepeatPassword.color = txtColorDefault;

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

        UserData userData = new UserData
        {
            Login = inputLogin.text,
            Password = inputPassword.text,
            PhoneNumber = inputPhoneNumber.text,
            Email = inputEmail.text,
            Record = record
        };

        System.Threading.Tasks.Task save = new System.Threading.Tasks.Task(() =>
        {
            string json = JsonUtility.ToJson(userData);

            File.WriteAllText("userData.json", json);
        });
        /*
        var prod = Task.Run(() => Prod(new double[10]));
        var prod1 = Task.Run(() => Prod(new double[10]));
        var prod2 = Task.Run(() => Prod(new double[10]));
        var prod3 = Task.Run(() => Prod(new double[10]));

        var allTask = Task.WhenAll(prod, prod1, prod2, prod3);

        allTask.ContinueWith(t => Debug.Log("¬ывод на экран"));*/

        save.Start();
    }

    private double Prod(double[] arr)
    {
        return arr[0];
    }

    private UserData LoadRegistrationInfo()
    {
        string json = File.ReadAllText("userData.json");

        return JsonUtility.FromJson<UserData>(json);
    }

    private void OnApplicationQuit()
    {
        SaveRegistrationInfo(true);
    }
}
