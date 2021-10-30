namespace Demo.BLL.Models
{
    public class StandardPersonData 
    {
        public string Id { get; set; }
        public string Address { get; set; }
        public string Zip { get; set; }
        public string Name { get; set; }
        public bool IsDuplicate { get; set; }
        public string[] Errors { get; set; }
    }
}
