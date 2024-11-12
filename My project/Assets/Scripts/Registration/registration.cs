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
            Console.WriteLine($"{exampleEmailToCheck} �������� ���������� ������� ����������� �����.");
        }
        else
        {
            Console.WriteLine($"{exampleEmailToCheck} �� �������� ���������� ������� ����������� �����.");
        }
        bool isValidPhoneNumber = IsValidPhoneNumber(examplePhoneNumberToCheck);

        if (isValidPhoneNumber)
        {
            Console.WriteLine($"{examplePhoneNumberToCheck} �������� ���������� ������� ��������.");
        }
        else
        {
            Console.WriteLine($"{examplePhoneNumberToCheck} �� �������� ���������� ������� ��������.");
        }
    }
    static bool IsValidEmail(string email)
    {
        // ���������� ��������� ��� �������� ������� ����������� �����
        string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        return Regex.IsMatch(email, pattern);
    }
    static bool IsValidPhoneNumber(string phoneNumber)
    {
        // ���������� ��������� ��� �������� ������� ������ ��������
        string pattern = @"^\+?\d{1,3}\s?\(?\d{1,4}?\)?[\s-]?\d{1,4}[\s-]?\d{2,4}[\s-]?\d{2,4}$";
        return Regex.IsMatch(phoneNumber, pattern);
    }
}
