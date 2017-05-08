using System.Windows.Controls;

namespace LoginPanelApplication
{
    static class PageSwitcher
    {
        public static MainWindow Window;

        public static void Navigate(Page page)
        {
            Window.Content = page;
        }
    }
}
