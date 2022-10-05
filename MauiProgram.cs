﻿using MauiMVVM.Service;
using MauiMVVM.ViewModel;

namespace MauiMVVM;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});
		builder.Services.AddTransient<MainPage>();
		builder.Services.AddTransient<DataItemListViewModel>();
		builder.Services.AddSingleton<DataItemService>();
		return builder.Build();
	}
}
