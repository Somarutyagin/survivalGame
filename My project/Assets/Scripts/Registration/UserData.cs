using System.Collections.Generic;

[System.Serializable]
public class UserData
{
    public string Login;
    public string Password;
    public string Email;
    public string PhoneNumber;
    public int Record;
}
[System.Serializable]
public class UserDataList
{
    public List <UserData> Users;
}
