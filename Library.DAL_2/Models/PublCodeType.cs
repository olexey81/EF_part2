namespace Library.DAL.Models
{
    public partial class PublCodeType
    {
        public int PublCodeTypeID { get; set; }
        public string TypeName { get; set; } = string.Empty;
        public List<Book>? Books { get; set; }
    }
}