namespace Library_DAL_2.Models
{
    public class DocumentsType
    {
        public int DocTypeID { get; set; }
        public string TypeName { get; set; } 
        public List<Reader> Readers { get; set; } 
    }
}