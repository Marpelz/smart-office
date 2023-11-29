using ReactiveUI;

namespace SmartOffice.Models.UserModels;

public class UserDataGrid : ReactiveObject
{
    private string userid = "";
    private string username = "";
    private string userpassword = "";
    private string haspaypal = "";
    private string paypalemail = "";

    public string UserId
    {
        get => userid;
        set => this.RaiseAndSetIfChanged(ref userid, value);
    }
    
    public string Username
    {
        get => username;
        set => this.RaiseAndSetIfChanged(ref username, value);
    }
    
    public string Userpassword
    {
        get => userpassword;
        set => this.RaiseAndSetIfChanged(ref userpassword, value);
    }
    
    public string Haspaypal
    {
        get => haspaypal;
        set => this.RaiseAndSetIfChanged(ref haspaypal, value);
    }
    
    public string Paypalemail
    {
        get => paypalemail;
        set => this.RaiseAndSetIfChanged(ref paypalemail, value);
    }
}