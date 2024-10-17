namespace Contract_Monthly_Claim_System.Hashing
{
    public static class PasswordHasher
    {   
        /// <summary>
        /// Provides hashed value to password string using BCrypt package.
        /// </summary>
        /// <param name="password">password string</param>
        /// <returns>Hashed value</returns>
        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        /// <summary>
        /// Compares password string to hashed passwor value
        /// </summary>
        /// <param name="password">password string</param>
        /// <param name="hashedPassword">stored hash value</param>
        /// <returns>true/false</returns>
        public static bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }

    }
}
