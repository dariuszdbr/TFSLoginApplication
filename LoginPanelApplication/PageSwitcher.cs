using System.Windows.Controls;

namespace LoginPanelApplication
{
    static class PageSwitcher
    {
        public static MainWindow Window;
        /// <summary>
        ///  Navigate to the specified page
        /// </summary>
        /// <param name="page"></param>
        public static void Navigate(Page page)
        {
            Window.Content = page;
        }
    }
}
