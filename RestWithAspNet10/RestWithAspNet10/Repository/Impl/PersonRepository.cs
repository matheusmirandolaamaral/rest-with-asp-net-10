using RestWithAspNet10.Model;
using RestWithAspNet10.Model.Context;

namespace RestWithAspNet10.Repository.Impl
{
    public class PersonRepository(MSSQLContext context) : GenericRepository<Person>(context), IPersonRepository
    {
        public Person? Disable(long id)
        {
            var person = _context.Persons.Find(id);
            if(person == null) return null;
            person.Enabled = false;
            _context.SaveChanges();
            return person;
        }
    }
}
