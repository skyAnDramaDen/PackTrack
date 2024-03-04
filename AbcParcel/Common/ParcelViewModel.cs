using AbcParcel.Data;
using System.Text.Json.Serialization;

namespace AbcParcel.Common
{
    public class ParcelViewModel
    {
        public long Id { get; set; }
        public string TrackingNumber { get; set; }
        public string Description { get; set; }
        public ParcelStatus ParcelStatus { get; set; }
        public string OriginatingLocation { get; set; }
        public string FinalDestination { get; set; }
    }
    public class CreateParcel
    {
        public string Description { get; set; }
        public string OriginatingLocation { get; set; }
        public string FinalDestination { get; set; }
    }

    public class UpdateParcel
    {
        public string Description { get; set; }
        public ParcelStatus ParcelStatus { get; set; }
        public string FinalDestination { get; set; }
        [JsonIgnore]
        public int Id { get; set; }
    }
    public class UpdateParcelView
    {
        public string Description { get; set; }
        public ParcelStatus ParcelStatus { get; set; }
        public string FinalDestination { get; set; }
        [JsonIgnore]
        public long Id { get; set; }
    }
}
