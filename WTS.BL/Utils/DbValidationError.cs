namespace WTS.BL.Utils
{
    public class DbValidationError
    {
        public DbValidationError(string errorMessage, string propertyName = "")
        {
            ErrorMessage = errorMessage;
            PropertyName = propertyName;
        }

        public string ErrorMessage { get; set; }
        public string PropertyName { get; set; }
    }
}
