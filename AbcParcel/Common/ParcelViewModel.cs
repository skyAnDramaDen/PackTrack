using AbcParcel.Data;  // Import the data namespace
using System.Text.Json.Serialization;  // Import the JsonSerialization namespace

namespace AbcParcel.Common
{
    // Represents a view model for displaying parcel details.
    public class ParcelViewModel
    {
        public long Id { get; set; }

        public string TrackingNumber { get; set; }

        public string Description { get; set; }

        public ParcelStatus ParcelStatus { get; set; }

        public string OriginatingLocation { get; set; }

        public string FinalDestination { get; set; }
    }

    // Represents a model for creating a new parcel.
    public class CreateParcel
    {
        public string Description { get; set; }
        public string OriginatingLocation { get; set; }
        public string FinalDestination { get; set; }
    }

    // Represents a model for updating an existing parcel.
    public class UpdateParcel
    {
        public string Description { get; set; }
        public ParcelStatus ParcelStatus { get; set; }
        public string FinalDestination { get; set; }
        [JsonIgnore]
        public int Id { get; set; }
    }

    // Represents a view model for updating an existing parcel.
    public class UpdateParcelView
    {        
        public string Description { get; set; }
        public ParcelStatus ParcelStatus { get; set; }
        public string FinalDestination { get; set; }
        [JsonIgnore]
        public long Id { get; set; }
    }
}