namespace Library_DAL_2.Models
{
    public class Librarian : User
    {
        public Librarian()
        {
            Role = (int)UserRole.Librarian;
        }
    }
}