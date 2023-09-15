using System.Windows.Input;
using MicrowaveConverter.Models;
using MicrowaveConverter.Utils;

namespace MicrowaveConverter.ViewModels;

public class MainViewModel : NotifyPropertyChangedImpl
{
    // ==============
    // Properties
    // ==============

    // Only show the current step of the application to simplify the UX.
    public AppStep CurrentStep
    {
        get => _currentStep;
        set => SetField(ref _currentStep, value);
    }

    // Use raw strings for the input fields to always show a valid value and not messing up with validation.

    public string InputOriginalWattage
    {
        get => _inputOriginalWattage;
        set => SetField(ref _inputOriginalWattage, value);
    }

    public string InputOriginalTime
    {
        get => _inputOriginalTime;
        set => SetField(ref _inputOriginalTime, value);
    }
    
    public string InputTargetWattage
    {
        get => _inputTargetWattage;
        set => SetField(ref _inputTargetWattage, value);
    }

    // Use a raw string for the output field to show a specific string if the output cannot be computed because of an invalid input.

    public string OutputTargetTime
    {
        get => _outputTargetTime;
        set => SetField(ref _outputTargetTime, value);
    }

    // Use validation properties for marking valid or invalid input values.

    public bool IsValidInputOriginalWattage
    {
        get => _isValidInputOriginalWattage;
        set => SetField(ref _isValidInputOriginalWattage, value);
    }

    public bool IsValidInputOriginalTime
    {
        get => _isValidInputOriginalTime;
        set => SetField(ref _isValidInputOriginalTime, value);
    }

    public bool IsValidInputTargetWattage
    {
        get => _isValidInputTargetWattage;
        set => SetField(ref _isValidInputTargetWattage, value);
    }

    // Commands

    public ICommand UpdateAfterInputCommand { get; }
    public ICommand ContinueFromOriginalWattageCommand { get; }
    public ICommand ContinueFromOriginalTimeCommand { get; }
    public ICommand ContinueFromTargetWattageCommand { get; }
    public ICommand RestartCommand { get; }

    // ==============
    // Fields
    // ==============

    private const string TimeFormat = "m\\:ss";

    // Fields behind the properties.

    private AppStep _currentStep = AppStep.OriginalWattage;

    private string _inputOriginalWattage = "800";
    private string _inputOriginalTime = "2:00";

    private string _inputTargetWattage = "600";
    private string _outputTargetTime = "--:--";

    private bool _isValidInputOriginalWattage;
    private bool _isValidInputOriginalTime;
    private bool _isValidInputTargetWattage;

    // ==============
    // Initialization
    // ==============

    public MainViewModel()
    {
        // Initialize the commands.
        UpdateAfterInputCommand = new DelegateCommand(UpdateAfterInput);
        ContinueFromOriginalWattageCommand = new DelegateCommand(ContinueFromOriginalWattage);
        ContinueFromOriginalTimeCommand = new DelegateCommand(ContinueFromOriginalTime);
        ContinueFromTargetWattageCommand = new DelegateCommand(ContinueFromTargetWattage);
        RestartCommand = new DelegateCommand(Restart);

        // Run the update method once to apply the correct values after having them initialized.
        // Thus, the output target time and the validation properties will be computed correctly.
        UpdateAfterInput();
    }

    // ==============
    // Event handling
    // ==============

    private void UpdateAfterInput()
    {
        ValidateInputValues();
        ComputeTargetMicrowaveSettings();
    }

    private void ContinueFromOriginalWattage()
    {
        CurrentStep = AppStep.OriginalTime;
    }

    private void ContinueFromOriginalTime()
    {
        CurrentStep = AppStep.TargetWattage;
    }

    private void ContinueFromTargetWattage()
    {
        CurrentStep = AppStep.TargetTime;
    }

    private void Restart()
    {
        CurrentStep = AppStep.OriginalWattage;
    }

    // ==============
    // Convert the target microwave settings
    // ==============

    private void ComputeTargetMicrowaveSettings()
    {
        // Reset the target time value to mark it as not computed.
        // Thus, the user will not think they has a valid target time when making invalid inputs.
        OutputTargetTime = "--:--";

        // Compute the target time if the input values are valid.
        if (IsValidInputOriginalWattage && IsValidInputOriginalTime && IsValidInputTargetWattage)
        {
            // Convert the input values.
            int.TryParse(InputOriginalWattage, out int originalWattage);
            TimeSpan.TryParseExact(InputOriginalTime, TimeFormat, null, out TimeSpan originalTime);
            int.TryParse(InputTargetWattage, out int targetWattage);

            // Compute the target seconds which are needed to get the same result as with the original settings and the
            // target wattage.
            double targetTimeInSeconds = originalWattage * originalTime.TotalSeconds / targetWattage;

            // Save the target seconds as new time span.
            OutputTargetTime = TimeSpan.FromSeconds(targetTimeInSeconds).ToString(TimeFormat);
        }
    }

    private void ValidateInputValues()
    {
        IsValidInputOriginalWattage = int.TryParse(InputOriginalWattage, out _);
        IsValidInputOriginalTime = TimeSpan.TryParseExact(InputOriginalTime, TimeFormat, null, out _);
        IsValidInputTargetWattage = int.TryParse(InputTargetWattage, out _);
    }
}