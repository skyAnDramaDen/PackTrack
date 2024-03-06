namespace AbcParcel.Data
{
    //this represents tracking information for a parcel
    public class Tracking : BaseEntity<long>
    {
        public DateTime LastDateTimeTracked { get; set; }
        public string CurrentLocation { get; set; }

        // Additional properties may be added in the future to enhance tracking functionality.
        // some include:
        // - GPS coordinates
        // - Estimated arrival or delivery time
        // - Delivery status
        // These properties are currently unused but defined for potential future prospects.
    }
}
