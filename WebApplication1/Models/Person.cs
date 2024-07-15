namespace WebApplication1.Models
{
    public class Person
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Age { set; get; }

        // alt enter ==> generate constractor

        public Person(string name, int age)
        {
            Name = name;
            Age = age;
            Id = Guid.NewGuid();
        }
    }
}
