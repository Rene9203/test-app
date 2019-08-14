using TestApp.Core.Entities;

namespace TestApp.Application.Dto
{
    public class EmployerDto
    {
        public string Id { get; set; }

        public string FullName { get; set; }

        public static EmployerDto From(Employer employer)
        {
            return new EmployerDto()
            {
                Id = employer.Id,
                FullName = employer.FullName
            };
        }
    }
}
