
namespace HoneyBunny.Models
{
    public class Category
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public string Description { get; set; }

        public Category() { }
        public Category(string name, string fullName, string description, int parentId)
        {
            Name = name;
            FullName = fullName;
            Description = description;
            ParentId = parentId;
        }
    }
}