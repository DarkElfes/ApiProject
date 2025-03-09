namespace ApiProject.Client.Web.Services;

public class VeilService
{
    public bool IsVisible { get; set; }

    public event Action? OnShow;
    public event Action? OnHide;

    public void Show()
    {
        IsVisible = true;
        OnShow?.Invoke();
    }   

    public void Hide()
    {
        IsVisible = false;
        OnHide?.Invoke();
    }
}
