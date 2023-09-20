using CommunityToolkit.Maui.Core.Platform;
using MicrowaveConverter.ViewModels;

namespace MicrowaveConverter.Views;

public partial class MainPage : ContentPage
{
    // ==============
    // Initialization
    // ==============

    public MainPage(MainViewModel mainViewModel)
    {
        InitializeComponent();
        BindingContext = mainViewModel;
    }

    // ==============
    // Hide keyboard when unfocussing an Entry
    // ==============

    private void Entry_OnUnfocused(object? sender, FocusEventArgs e)
    {
        if (sender is Entry entry)
        {
            entry.HideKeyboardAsync(CancellationToken.None);
        }
    }
}