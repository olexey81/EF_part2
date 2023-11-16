namespace Library.DAL.Models
{
    public class DocumentsType
    {
        public int DocTypeID { get; set; }
        public string TypeName { get; set; } = string.Empty;
        public List<Reader>? Readers { get; set; } 
    }
}