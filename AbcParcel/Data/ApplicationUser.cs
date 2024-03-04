using Microsoft.AspNetCore.Identity;
using System.ComponentModel;

namespace AbcParcel.Data
{
    public class Applicationuser : IdentityUser
    {
        public bool IsActive { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime? AddressVerificationDate { get; set; }
        public DateTime? ApprovedOn { get; set; }
        public string? ApprovedBy { get; set; }
        public DateTime? LastTransactedOn { get; set; }
        public string? HouseNumber { get; set; }
        public string? Street { get; set; }
        public string? LandMark { get; set; }
        public string? AddressLine1 { get; set; }
        public UserType UserType { get; set; }
    }

    public enum UserType
    {
        [Description("Admin")]
        Admin,
        [Description("Customer")]
        Customer
    }
}
