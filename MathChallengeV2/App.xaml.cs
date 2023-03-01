using MathChallengeV2.Data;
using Microsoft.UI;
using Microsoft.UI.Windowing;

namespace MathChallengeV2;

public partial class App : Application
{
	public static GameRepository GameRepository { get; private set; }

	const int WindowWidth = 1200;
	const int WindowHeight = 850;

	public App(GameRepository gameRepository)
	{
		InitializeComponent();

		Microsoft.Maui.Handlers.WindowHandler.Mapper.AppendToMapping(nameof(IWindow), (handler, view) =>
		{
#if WINDOWS
			var mauiWindow = handler.VirtualView;
			var nativeWIndow = handler.PlatformView;
			nativeWIndow.Activate();
			IntPtr windowHandle = WinRT.Interop.WindowNative.GetWindowHandle(nativeWIndow);
			WindowId windowId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(windowHandle);
			AppWindow appWindow = Microsoft.UI.Windowing.AppWindow.GetFromWindowId(windowId);
			appWindow.Resize(new Windows.Graphics.SizeInt32(WindowWidth, WindowHeight));
#endif
		});

		MainPage = new AppShell();

		GameRepository = gameRepository;
	}
}
