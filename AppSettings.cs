namespace SmartOffice;

public static class AppSettings
{
    private static string _username;

    public static string Username
    {
        get { return _username; }
        set { _username = value; }
    }
}