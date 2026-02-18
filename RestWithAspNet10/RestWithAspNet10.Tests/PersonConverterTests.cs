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
    }
}
