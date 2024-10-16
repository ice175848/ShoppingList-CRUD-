namespace Shopping
{
    public class Utility
    {
        public static void OpenForm(Form currentForm, Form newForm)
        {
            newForm.Show();
            currentForm.Close();
        }
    }
}