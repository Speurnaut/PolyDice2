using PolyDice2.Models;

namespace PolyDice2;

public partial class MainPage : ContentPage
{
	private Die Die;

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
        OutputLbl.Text = Die.Name;
		IconImg.Source = Die.Icon;

		if (Die.Sides == 2)
		{
			RollBtn.Text = "Flip";
		}
		else
		{
			RollBtn.Text = "Roll";
		}
	}

	private void FinishRoll(object sender)
	{
		MainThread.BeginInvokeOnMainThread(() =>
		{

			var timer = sender as IDispatcherTimer;
			timer.Stop();
			OutputLbl.Text = Die.Roll();
			ToggleRolling(false);
		});
	}

	private void ToggleRolling(bool isRolling)
	{
        RollBtn.IsEnabled = !isRolling;
        OutputLbl.IsVisible = !isRolling;
        RollAct.IsVisible = isRolling;
        RollAct.IsRunning = isRolling;
    }
}

