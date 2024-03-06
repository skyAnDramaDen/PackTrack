namespace AbcParcel.Data
{
    public class BaseEntity<T>
    {
        // this represents a base entity with common properties for all entities.
        // it is used to provide common properties such as Id, creation date, and update date
        // for all entities in the application, reducing code duplication and ensuring consistency.

        public T Id { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
