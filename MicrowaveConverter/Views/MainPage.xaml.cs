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

    // ==============
    // Set focus on Entry elements and mark their complete text
    // ==============

    private void MainPage_OnLoaded(object? sender, EventArgs e)
    {
        FocusAndSelectCompleteTextInsideEntry(EntryOriginalWattage);
    }

    /*
     * AppStep.OriginalWattage
     */

    private void ButtonOriginalWattage_OnClick(object? sender, EventArgs e)
    {
        FocusAndSelectCompleteTextInsideEntry(EntryOriginalTime);
    }

    private void EntryOriginalWattage_OnCompleted(object? sender, EventArgs e)
    {
        FocusAndSelectCompleteTextInsideEntry(EntryOriginalTime);
    }

    /*
     * AppStep.OriginalTime
     */

    private void ButtonOriginalTime_OnClick(object? sender, EventArgs e)
    {
        FocusAndSelectCompleteTextInsideEntry(EntryTargetWattage);
    }

    private void EntryOriginalTime_OnCompleted(object? sender, EventArgs e)
    {
        FocusAndSelectCompleteTextInsideEntry(EntryTargetWattage);
    }

    /*
     * AppStep.TargetTime
     */

    private void ButtonTargetTime_OnClick(object? sender, EventArgs e)
    {
        FocusAndSelectCompleteTextInsideEntry(EntryOriginalWattage);
    }

    private void FocusAndSelectCompleteTextInsideEntry(Entry entry)
    {
        // Wrap inside the dispatcher for making the commands working correctly in Android.
        // See: https://stackoverflow.com/questions/76078597/select-all-text-in-entry-when-focused
        Dispatcher.Dispatch(() =>
        {
            // Set the focus on the Entry for entering the original wattage.
            entry.Focus();

            // Select the complete text inside the Entry.
            entry.CursorPosition = 0;
            entry.SelectionLength = entry.Text.Length;
        });
    }
}