namespace AbcParcel.Common
{
    // This static class contains helper methods for generating tracking numbers and extracting email domains.
    public static class Helpers
    {
        // Generates a tracking number using the provided location code and a random number.
        // Throws an ArgumentException if the location code is null, empty, or contains only whitespace.
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

        // Extracts the domain part from the given email address.
        // Throws an ArgumentException if the email address is in an invalid format.
        public static string GetEmailDomain(string emailAddress)
        {
            string[] parts = emailAddress.Split('@');

            if (parts.Length == 2)
            {
                return parts[1];  // Return the domain part
            }
            else
            {
                throw new ArgumentException("Invalid email address format");
            }
        }
    }
}
