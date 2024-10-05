using UnityEngine;

public static class GlobalVaribles
{
    //map border
    public static readonly float border = 99.0f;
    //Game Status
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
    //Score
    private static int _valueScore;
    public static int score
    {
        get
        {
            Init();
            return _valueScore;
        }
        set
        {
            _valueScore = value;
            if (score > record)
                record = score;
        }
    }
    //Record
    private const string keyRecord = "record";
    private static int _valueRecord;
    public static int record
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
    }
}
