public static class GlobalVaribles
{
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
    public static void Init()
    {
        valueStatus = defoultStatus;
    }
}
