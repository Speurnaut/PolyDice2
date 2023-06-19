using PolyDice2.Models;

namespace PolyDice2;

public partial class MainPage : ContentPage
{
	private int _count = 1;
    private Die _die;
    private int _modifier = 0;
    private bool _isRolling = false;

    public int Count {
        get { return _count; }
        set {
            _count = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(EnableCountLessBtn));
        }
    }

    public Die CurrentDie
    {
        get { return _die; }
        set {
            _die = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(ShowAdvancedOptions));
        }
    }

    public int Modifier
    {
        get { return _modifier; }
        set {
            _modifier = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(FormattedModifier));
        }
    }

    public string FormattedModifier
    {
        get { return Modifier >= 0 ? $"+{Modifier}" : Modifier.ToString(); }
    }

    public bool IsRolling
    {
        get { return _isRolling; }
        set { 
            _isRolling = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(EnableControls));
            OnPropertyChanged(nameof(EnableCountLessBtn));
        }
    }

    public bool EnableControls
    {
        get { return !IsRolling; }
    }

    public bool ShowAdvancedOptions
    {
        get { return !CurrentDie.ForceSimpleMode; }
    }

    public bool EnableCountLessBtn
    {
        get { return Count > 1 && EnableControls; }
    }

    public MainPage()
	{
        CurrentDie = new Die(20);
        InitializeComponent();
        BindingContext = this;
    }

	private void OnRollClicked(object sender, EventArgs e)
	{
        IsRolling = true;
		var rollTimer = Application.Current.Dispatcher.CreateTimer();
		rollTimer.Interval = TimeSpan.FromSeconds(1);
		rollTimer.Tick += (s, e) => FinishRoll(s);
		rollTimer.Start();
	}

    private void OnLeftClicked(object sender, EventArgs e)
    {
        CurrentDie = new Die(CurrentDie.Previous());
		UpdateUI();
    }

    private void OnRightClicked(object sender, EventArgs e)
    {
        CurrentDie = new Die(CurrentDie.Next());
        UpdateUI();
    }

	private void UpdateUI()
	{
        OutputLbl.Text = "";
        BreakdownLbl.Text = "";

        if (CurrentDie.Sides == 2)
		{
			RollBtn.Text = "Flip";
		}
		else
		{
			RollBtn.Text = "Roll";
		}
    }

    private void CountLess(object sender, EventArgs e)
    {
        if (Count > 1)
        {
            Count--;
        }
    }

    private void CountMore(object sender, EventArgs e)
    {
        Count++;
    }

    private void ModifierLess(object sender, EventArgs e)
    {
        Modifier--;
    }

    private void ModifierMore(object sender, EventArgs e)
    {
        Modifier++;
    }

    private void FinishRoll(object sender)
	{
		MainThread.BeginInvokeOnMainThread(() =>
		{
			var timer = sender as IDispatcherTimer;
			timer.Stop();
            (string total, string breakdown) result = CurrentDie.Roll(Count, Modifier);
            OutputLbl.Text = result.total;
            BreakdownLbl.Text = result.breakdown;
			IsRolling = false;
		});
	}
}