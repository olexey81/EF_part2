namespace Library_DAL_2.Models
{
    public partial class PublCodeType
    {
        public int PublCodeTypeID { get; set; }
        public string TypeName { get; set; }
        public List<Book> Books { get; set; }
    }
}