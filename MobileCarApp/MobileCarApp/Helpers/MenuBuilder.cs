using MobileCarApp.Controls;

namespace MobileCarApp.Helpers;

public static class MenuBuilder
{
    public async static Task BuildMenu()
    {
        Shell.Current.Items.Clear();
        Shell.Current.FlyoutHeader = new FlyOutHeader();

        var role = App.UserInfo.Role;

        if(role.Equals("Administrator"))
        {
            var flyOutItem = new FlyoutItem()
            {
                Title = "Car Management",
                Route = nameof(MainPage),
                FlyoutDisplayOptions = FlyoutDisplayOptions.AsMultipleItems,
                Items =
                {
                    new ShellContent
                    {
                        Icon = "dotnet_bot.png",
                        Title = "Admin Page 1",
                        ContentTemplate = new DataTemplate(typeof(MainPage))
                    },
                    new ShellContent
                    {
                        Icon = "dotnet_bot.png",
                        Title = "Admin Page 2",
                        ContentTemplate = new DataTemplate(typeof(MainPage))
                    }
                }
            };

            if (!Shell.Current.Items.Contains(flyOutItem))
            {
                Shell.Current.Items.Add(flyOutItem);
            }
        }

        if (role.Equals("User"))
        {

        }
    }
}
