using StreamingApp.WPF.ViewModels.Base;

namespace StreamingApp.WPF.ViewModels;

internal class TestViewModel : ViewModelBase
{
    public string Message { get; set; }

    public TestViewModel()
    {
        Message = "Hello world!";
    }

    public TestViewModel(string message)
    {
        Message = message;
    }
}
