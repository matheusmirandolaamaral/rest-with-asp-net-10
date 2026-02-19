using FluentAssertions;
using RestWithAspNet10.Data.Converter.Impl;
using RestWithAspNet10.Data.DTO.V2;
using RestWithAspNet10.Model;

namespace RestWithAspNet10.Tests
{
    public class PersonConverterTests
    {
        private readonly PersonConverter _converter;

        public PersonConverterTests()
        {
            _converter = new PersonConverter();
        }

        // PersonDTO to person conversion tests
        [Fact]
        public void Parse_ShouldConvertPersonDTOToPerson()
        {

            // Arrange: prepare the data, objects, and dependencies required for the test
            var dto = new PersonDTO
            {
                Id = 1,
                FirstName = "Yuri",
                LastName = "Alberto",
                Address = "Brasil",
                Gender = "Male",
                BirthDay = new DateTime(2000, 1, 1)
            };
            var expectedPerson = new Person
            {
                Id = 1,
                FirstName = "Yuri",
                LastName = "Alberto",
                Address = "Brasil",
                Gender = "Male"
            };
            // Act: execute the method or functionality under test
            var person = _converter.Parse(dto);


            // Assert: verify that the result matches the expected outcome
            person.Should().NotBeNull();
            person.Id.Should().Be(expectedPerson.Id);
            person.FirstName.Should().Be(expectedPerson.FirstName);
            person.LastName.Should().Be(expectedPerson.LastName);
            person.Address.Should().Be(expectedPerson.Address);
            person.Gender.Should().Be(expectedPerson.Gender);
            person.Should().BeEquivalentTo(expectedPerson);
        }
        [Fact]
        public void Parse_NullPersonDTOShouldReturnNull()
        {
            PersonDTO dto = null;
            var person = _converter.Parse(dto);
            person.Should().BeNull();
        }




        // Person to PersonDTO conversion tests
        [Fact]
        public void Parse_ShouldConvertPersonToPersonDTO()
        {

            // Arrange: prepare the data, objects, and dependencies required for the test
            var entity = new Person
            {
                Id = 1,
                FirstName = "Yuri",
                LastName = "Alberto",
                Address = "Brasil",
                Gender = "Male"
                // BirthDay = new DateTime(2000, 1, 1)
            };
            var expectedPerson = new PersonDTO
            {
                Id = 1,
                FirstName = "Yuri",
                LastName = "Alberto",
                Address = "Brasil",
                Gender = "Male"
            };
            // Act: execute the method or functionality under test
            var person = _converter.Parse(entity);


            // Assert: verify that the result matches the expected outcome
            person.Should().NotBeNull();
            person.Id.Should().Be(expectedPerson.Id);
            person.FirstName.Should().Be(expectedPerson.FirstName);
            person.LastName.Should().Be(expectedPerson.LastName);
            person.Address.Should().Be(expectedPerson.Address);
            person.Gender.Should().Be(expectedPerson.Gender);
            person.Should().BeEquivalentTo(expectedPerson, options => options.Excluding(person => person.BirthDay));
            person.BirthDay.Should().NotBeNull();
        }
        [Fact]
        public void Parse_NullPersonShouldReturnNull()
        {
            Person dto = null;
            var person = _converter.Parse(dto);
            person.Should().BeNull();
        }



        [Fact]
        public void ParseList_ShouldConvertPersonDTOListToPersonList()
        {
            //Arrange
            var dtoList = new List<PersonDTO>
            {
                new PersonDTO
                {
                    Id = 1,
                    FirstName = "Yuri",
                    LastName = "Alberto",
                    Address = "Brasil",
                    Gender = "Male",
                    BirthDay = new DateTime(2000, 1, 1)
                },

                new PersonDTO
                {
                    Id = 2,
                    FirstName = "Lebron",
                    LastName = "James",
                    Address = "USA",
                    Gender = "Male",
                    BirthDay = new DateTime(1989, 1, 1)
                }
            };
            //Act
            var personList = _converter.ParseList(dtoList);

            //Assert
            personList.Should().NotBeNull();
            personList.Should().HaveCount(2);
            personList[0].Should().BeEquivalentTo(new Person
            {
                Id = 1,
                FirstName = "Yuri",
                LastName = "Alberto",
                Address = "Brasil",
                Gender = "Male",
                // BirthDay = new DateTime(2000, 1, 1)
            });
            personList[1].Should().BeEquivalentTo(new Person
            {

                Id = 2,
                FirstName = "Lebron",
                LastName = "James",
                Address = "USA",
                Gender = "Male",
               // BirthDay = new DateTime(1989, 1, 1)
            });
        }

        [Fact]
        public void Parse_NullListPersonDTOShouldReturnNull()
        {
            List<PersonDTO> dto = null;
            var listPerson = _converter.ParseList(dto);
            listPerson.Should().BeNull();
        }
    }
}
