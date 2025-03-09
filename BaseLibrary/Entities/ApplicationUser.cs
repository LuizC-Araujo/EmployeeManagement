namespace BaseLibrary.Entities
{
    public class ApplicationUser 
    {
        public int Id { get; private set; }
        public string? Fullname { get; private set; }
        public string? Email { get; private set; }
        public string? Password { get; private set; }

        public void FullnameFill(string fullname)
        {
            Fullname = fullname;
        }

        public void EmailFill(string fullname)
        {
            Fullname = fullname;
        }

        public void PasswordFill(string fullname)
        {
            Fullname = fullname;
        }
    }
}
