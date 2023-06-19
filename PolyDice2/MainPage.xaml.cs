using PolyDice2.Models;

namespace PolyDice2;

public partial class MainPage : ContentPage
{
	private Die Die;
	private int Count = 1;
	private int Modifier = 0;

	public MainPage()
	{
		Die = new Die(20);
        InitializeComponent();
    }

	private void OnRollClicked(object sender, EventArgs e)
	{
		ToggleRolling(true);
		var rollTimer = Application.Current.Dispatcher.CreateTimer();
		rollTimer.Interval = TimeSpan.FromSeconds(1);
		rollTimer.Tick += (s, e) => FinishRoll(s);
		rollTimer.Start();
	}

    private void OnLeftClicked(object sender, EventArgs e)
    {
		Die.Previous();
		UpdateUI();
    }

    private void OnRightClicked(object sender, EventArgs e)
    {
		Die.Next();
        UpdateUI();
    }

	private void UpdateUI()
	{
		NameLbl.Text = Die.Name;
        OutputLbl.Text = "";
        BreakdownLbl.Text = "";
		IconImg.Source = Die.Icon;

        if (Die.Sides == 2)
		{
			RollBtn.Text = "Flip";
		}
		else
		{
			RollBtn.Text = "Roll";
		}

        AdvancedOptionsGrd.IsVisible = !Die.ForceSimpleMode;
        CountLessBtn.IsEnabled = Count > 1 && !Die.ForceSimpleMode;
        CountMoreBtn.IsEnabled = !Die.ForceSimpleMode;
        ModifierLessBtn.IsEnabled = !Die.ForceSimpleMode;
        ModifierMoreBtn.IsEnabled = !Die.ForceSimpleMode;
    }

    private void CountLess(object sender, EventArgs e)
    {
        if (Count > 1)
        {
            Count--;
        }

        CountLessBtn.IsEnabled = Count > 1;
        CountLbl.Text = Count.ToString();
    }

    private void CountMore(object sender, EventArgs e)
    {
        Count++;
        CountLessBtn.IsEnabled = Count > 1;
        CountLbl.Text = Count.ToString();
    }

    private void ModifierLess(object sender, EventArgs e)
    {
        Modifier--;
        ModifierLbl.Text = Modifier >= 0 ? $"+{Modifier}" : $"{Modifier}";
    }

    private void ModifierMore(object sender, EventArgs e)
    {
        Modifier++;
        ModifierLbl.Text = Modifier >= 0 ? $"+{Modifier}" : $"{Modifier}";
    }

    private void FinishRoll(object sender)
	{
		MainThread.BeginInvokeOnMainThread(() =>
		{
			var timer = sender as IDispatcherTimer;
			timer.Stop();
            (string total, string breakdown) result = Die.Roll(Count, Modifier);
            OutputLbl.Text = result.total;
            BreakdownLbl.Text = result.breakdown;
			ToggleRolling(false);
		});
	}

	private void ToggleRolling(bool isRolling)
	{
        LeftBtn.IsEnabled = !isRolling;
        RightBtn.IsEnabled = !isRolling;
        RollBtn.IsEnabled = !isRolling;
        OutputLbl.IsVisible = !isRolling;
        BreakdownLbl.IsVisible = !isRolling && !Die.ForceSimpleMode;
        CountLessBtn.IsEnabled = !isRolling && Count > 1 && !Die.ForceSimpleMode;
        CountMoreBtn.IsEnabled = !isRolling && !Die.ForceSimpleMode;
        ModifierLessBtn.IsEnabled = !isRolling && !Die.ForceSimpleMode;
        ModifierMoreBtn.IsEnabled = !isRolling && !Die.ForceSimpleMode;
        RollAct.IsVisible = isRolling;
        RollAct.IsRunning = isRolling;
    }
}