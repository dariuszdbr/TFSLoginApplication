namespace LoginPanelApplication
{
    public static class LinqManager
    {
        public static UserDatabaseDataContext usersDataContext;
        public static User loggedInUser;
        public static Loginfo logInfo;
        


        static LinqManager()
        {
            usersDataContext = new UserDatabaseDataContext();
        }

    }
}
