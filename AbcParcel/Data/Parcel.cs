using System.ComponentModel;

namespace AbcParcel.Data
{
    // this represennts a parcel entity
    public class Parcel : BaseEntity<long>
    {
        public string TrackingNumber { get; set; }
        public string Description { get; set; }
        public string OriginatingLocation { get; set; }
        public string CurrentLocation { get; set; }
        public string FinalDestination { get; set; }
        public ParcelStatus ParcelStatus { get; set; }
        public ICollection<Tracking> Trackings { get; set; }
    }

    //enumeration representing the possible status of a parcel
    public enum ParcelStatus
    {
        [Description("Picked up")]
        PickedUp,
        [Description("Enroute to Destination")]
        EnRouteToDestination,
        [Description("Misplaced")]
        Misplaced,
        [Description("MissingInTransit")]
        MissingInTransit,
        [Description("Delivered")]
        Delivered
    }
}
