using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

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
        string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        return Regex.IsMatch(email, pattern);
    }
    private bool IsValidPhoneNumber(string phoneNumber)
    {
        // –егул€рное выражение дл€ проверки формата номера телефона
        string pattern = @"^\+?\d{1,3}\s?\(?\d{1,4}?\)?[\s-]?\d{1,4}[\s-]?\d{2,4}[\s-]?\d{2,4}$";
        return Regex.IsMatch(phoneNumber, pattern);
    }
    private bool IsValidPassword(string password)
    {
        // –егул€рное выражение дл€ проверки формата парол€
        string pattern = @"^(?=.{4,10}$)(?!.*\s).*$";
        return Regex.IsMatch(password, pattern);
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
        bool isValidRepeatPassword = inputPassword.text.Equals(inputRepeatPassword.text);
        bool isValidRegistration = isValidEmail && isValidPhoneNumber && isValidPassword && isValidRepeatPassword && isValidLogin;

        if (!isValidLogin)
            txtLogin.color = Color.red;
        else
            txtLogin.color = txtColorDefault;
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
            SaveRegistrationInfo();

            PlayerPrefs.SetInt(RegistrationDoneCheckKey, 1);
            registrationDisplay.SetActive(false);
        }
    }
    private void SaveRegistrationInfo()
    {
        UserData userData = new UserData
        {
            Login = inputLogin.text,
            Password = inputPassword.text,
            PhoneNumber = inputPhoneNumber.text,
            Email = inputEmail.text
        };

        string json = JsonUtility.ToJson(userData);
        File.WriteAllText("userData.json", json);
    }
    private UserData LoadRegistrationInfo()
    {
        string json = File.ReadAllText("userData.json");

        return JsonUtility.FromJson<UserData>(json);
    }
}
