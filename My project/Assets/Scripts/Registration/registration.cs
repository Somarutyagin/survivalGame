using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class registration : MonoBehaviour
{
    string exampleEmailToCheck = "example@example.com";
    string examplePhoneNumberToCheck = "+7 (123) 456-78-90";

    void Start()
    {
        bool isValidEmail = IsValidEmail(exampleEmailToCheck);

        if (isValidEmail)
        {
            Console.WriteLine($"{exampleEmailToCheck} €вл€етс€ корректным адресом электронной почты.");
        }
        else
        {
            Console.WriteLine($"{exampleEmailToCheck} не €вл€етс€ корректным адресом электронной почты.");
        }
        bool isValidPhoneNumber = IsValidPhoneNumber(examplePhoneNumberToCheck);

        if (isValidPhoneNumber)
        {
            Console.WriteLine($"{examplePhoneNumberToCheck} €вл€етс€ корректным номером телефона.");
        }
        else
        {
            Console.WriteLine($"{examplePhoneNumberToCheck} не €вл€етс€ корректным номером телефона.");
        }
    }
    static bool IsValidEmail(string email)
    {
        // –егул€рное выражение дл€ проверки формата электронной почты
        string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        return Regex.IsMatch(email, pattern);
    }
    static bool IsValidPhoneNumber(string phoneNumber)
    {
        // –егул€рное выражение дл€ проверки формата номера телефона
        string pattern = @"^\+?\d{1,3}\s?\(?\d{1,4}?\)?[\s-]?\d{1,4}[\s-]?\d{2,4}[\s-]?\d{2,4}$";
        return Regex.IsMatch(phoneNumber, pattern);
    }
}
