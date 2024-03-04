namespace AbcParcel.Common
{
    public static class Helpers
    {
        public static string GenerateTrackingNumber(string locationCode)
        {
            if (locationCode.Contains(" "))
                locationCode = locationCode.Replace(" ", "");
            if (string.IsNullOrWhiteSpace(locationCode))
            {
                throw new ArgumentException("Location code cannot be null or empty.", nameof(locationCode));
            }
            Random random = new Random();
            int randomNumber = random.Next(100000, 999999);

            string trackingNumber = $"{locationCode.ToUpper()}-{randomNumber}";

            return trackingNumber;
        }
        public static string GetEmailDomain(string emailAddress)
        {
            string[] parts = emailAddress.Split('@');

            if (parts.Length == 2)
            {
                return parts[0];
            }
            else
            {
                throw new ArgumentException("Invalid email address format");
            }
        }
    }
}
