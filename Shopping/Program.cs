namespace Shopping
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration. 
            ApplicationConfiguration.Initialize();
            while (true)
            {
                try
                {
                    Application.Run(new Form1());
                }
                catch (IndexOutOfRangeException ex)
                {
                    MessageBox.Show("無內容物");
                }
            }
        }
    }
}