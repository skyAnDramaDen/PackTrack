namespace AbcParcel.Data
{
    public class Tracking : BaseEntity<long>
    {
        public DateTime LastDateTimeTracked { get; set; }
        public string CurrentLocation { get; set; }
    }
}
