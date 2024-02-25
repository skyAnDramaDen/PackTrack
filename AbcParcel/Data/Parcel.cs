﻿namespace AbcParcel.Data
{
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
    public enum ParcelStatus
    {

    }
}