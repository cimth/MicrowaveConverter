using System.Globalization;

namespace MicrowaveConverter;

public partial class App : Application
{
    // ==============
    // Initialization
    // ==============

    public App()
    {
        InitializeComponent();

        // Force English as language for developing purposes.
        //SetLocale(new CultureInfo("en"));

        MainPage = new AppShell();
    }

    // ==============
    // Force language for developing purposes
    // ==============

    public void SetLocale(CultureInfo ci)
    {
        Thread.CurrentThread.CurrentCulture = ci;
        Thread.CurrentThread.CurrentUICulture = ci;
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
        window.Height = 700;

        return window;
    }
}