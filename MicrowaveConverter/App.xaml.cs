namespace MicrowaveConverter;

public partial class App : Application
{
    // ==============
    // Initialization
    // ==============

    public App()
    {
        InitializeComponent();

        MainPage = new AppShell();
    }

    // ==============
    // Override CreateWindow()
    // ==============

    // Override the CreateWindow() method to set a custom window size.
    // See: https://stackoverflow.com/questions/72399551/maui-net-set-window-size

    protected override Window CreateWindow(IActivationState? activationState)
    {
        var window = base.CreateWindow(activationState);

        window.Width = 400;
        window.Height = 600;

        return window;
    }
}