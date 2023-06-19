﻿using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Content.Res;

namespace PolyDice2;

[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity
{
    protected override void AttachBaseContext(Context @base)
    {
        Configuration configuration = new(@base.Resources.Configuration)
        {
            FontScale = 1.0f
        };
        ApplyOverrideConfiguration(configuration);
        base.AttachBaseContext(@base);
    }
}
