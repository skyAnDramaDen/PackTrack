using AbcParcel.Data;

namespace AbcParcel.Common
{
    public class ParcelViewModel
    {
        public long Id { get; set; }
        public string TrackingNumber { get; set; }
        public string Description { get; set; }
        public ParcelStatus ParcelStatus { get; set; }
        public string CreatedBy { get; set; }
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
    }
}
