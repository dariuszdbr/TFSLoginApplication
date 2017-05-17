namespace LoginPanelApplication
{
    public static class LinqManager
    {
        public static UserDatabaseDataContext usersDataContext;
        public static User loggedInUser;

        static LinqManager()
        {
            usersDataContext = new UserDatabaseDataContext();
        }

    }
}
