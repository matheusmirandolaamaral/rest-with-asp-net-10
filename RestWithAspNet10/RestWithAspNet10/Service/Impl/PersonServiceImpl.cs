using RestWithAspNet10.Model;

namespace RestWithAspNet10.Service.Impl
{
    public class PersonServiceImpl : IPersonService
    {

        public List<Person> FindAll()
        {
           List<Person> persons = new List<Person>();
            for(int i = 0; i < 8; i++)
            {
                persons.Add(MockPerson(i));
            }
            return persons;
        }

        public Person FindById(long id)
        {
            var person = MockPerson((int)id);
                return person;
        }
        public Person Create(Person person)
        {
            person.Id = new Random().Next(1,1000); // Simulate ID
            return person;
        }

        public Person Update(Person person)
        {
            return person;
        }
        public void Delete(long id)
        {
            // Simulate deletion logic
        }

        public Person MockPerson(int i)
        {
            var person = new Person
            {
                Id = new Random().Next(1, 1000),
                FirstName = "John",
                LastName = "Doe",
                Address = " 123 street",
                Gender = "Male"
            };
            return person;
        }

    }
}
