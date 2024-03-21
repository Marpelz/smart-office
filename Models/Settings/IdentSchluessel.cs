using ReactiveUI;

namespace SmartOffice.Models.Settings;

public class IdentSchluessel : ReactiveObject
{
    private string _key = "";
    private string _bezeichnung = "";


    public IdentSchluessel()
    {
    }

    public IdentSchluessel(string key, string bezeichnung)
    {
        _key = key;
        this._bezeichnung = bezeichnung;
    }

    public string KeyProp
    {
        get => _key;
        set => this.RaiseAndSetIfChanged(ref _key, value);
    }

    public string BezeichnungProp
    {
        get => _bezeichnung;
        set => this.RaiseAndSetIfChanged(ref _bezeichnung, value);
    }

    public string Key
    {
        get => _key;
        set => _key = value;
    }

    public string Bezeichnung
    {
        get => _bezeichnung;
        set => _bezeichnung = value;
    }
}