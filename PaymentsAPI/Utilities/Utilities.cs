namespace PaymentsAPI.Utilities
{
    public static class Utilities
    {
        public static string HashPassword(string password)
        {
           
            string salt = BCrypt.Net.BCrypt.GenerateSalt(4); //cost of 4
            string hashedPassword = !string.IsNullOrEmpty(password) ? BCrypt.Net.BCrypt.HashPassword(password,salt) : string.Empty;
            return hashedPassword;
        }
    }
}
