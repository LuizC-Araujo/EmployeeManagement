namespace BaseLibrary.Entities
{
    public class ApplicationUser 
    {
        public int Id { get; set; }
        public string? Fullname { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }

        public void FullnameFill(string fullname)
        {
            Fullname = fullname;
        }
    }
}
