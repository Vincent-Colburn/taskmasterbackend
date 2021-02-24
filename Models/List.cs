namespace taskmasterbackend
{
    public class List
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string description { get; set; }

        public string CreatorId { get; set; }
    }


    public class ProfileTaskUserViewModel
    {
        public int ListId { get; set; }
    }
}