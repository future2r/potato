using Avalonia;
using System;
using System.Globalization;
using System.Linq;

namespace Potato.Gui;

sealed class Program
{
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    public static void Main(string[] args)
    {
        ApplyCultureFromArgs(args);

        BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
    }


    private static void ApplyCultureFromArgs(string[] args)
    {
        var langArg = args.FirstOrDefault(a => a.StartsWith("--lang=", StringComparison.OrdinalIgnoreCase));
        if (langArg != null)
        {
            var culture = new CultureInfo(langArg.Substring("--lang=".Length));
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;
        }
    }


    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace();
}
