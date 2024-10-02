using UnityEngine;

public static class GlobalVaribles
{
    //Game Status
    private const bool defoultStatus = false;
    private static bool valueStatus;
    public static bool gameStatus
    {
        get
        {
            return valueStatus;
        }
        set
        {
            valueStatus = value;
        }
    }
    //Record
    private const string keyRecord = "record";
    private static int _valueRecord;
    public static int Energy
    {
        get
        {
            Init();
            return _valueRecord;
        }
        set
        {
            PlayerPrefs.SetInt(keyRecord, value);
            _valueRecord = value;
        }
    }
    public static void Init()
    {
        _valueRecord = PlayerPrefs.GetInt(keyRecord);
        valueStatus = defoultStatus;
    }
}
