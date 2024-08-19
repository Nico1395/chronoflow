using ChronoFlow.Client.AccessManagement.Employees.Entities;
using ChronoFlow.Client.AccessManagement.Roles.Entities;
using ChronoFlow.Client.Common.MainData.Results;
using ChronoFlow.Client.Common.MainData.UseCases.MainDataList;
using ChronoFlow.Client.Common.Objects.ValueObjects;

namespace ChronoFlow.Client.AccessManagement.Employees.UseCases.MainDataList;

internal sealed class EmployeeMainDataListService : IMainDataListService<EmployeeViewModel>
{
    private List<EmployeeViewModel>? _employees;

    public Task<MainDataGetAllResult<EmployeeViewModel>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return Task.FromResult(new MainDataGetAllResult<EmployeeViewModel>(MainDataGetAllResultCode.Success, GetEmployees()));
    }

    public Task<MainDataDeleteResult<EmployeeViewModel>> DeleteAsync(EmployeeViewModel viewModel, CancellationToken cancellationToken = default)
    {
        GetEmployees().Remove(viewModel);
        return Task.FromResult(new MainDataDeleteResult<EmployeeViewModel>(MainDataDeleteResultCode.Success));
    }

    private List<EmployeeViewModel> GetEmployees()
    {
        return _employees ??= GenerateEmployees(50);
    }

    public static List<EmployeeViewModel> GenerateEmployees(int count)
    {
        var employees = new List<EmployeeViewModel>();

        for (int i = 0; i < count; i++)
        {
            DateTime createdDate = GetRandomCreatedDate();
            DateTime lastChangedDate = GetRandomLastChangedDate(createdDate);

            var employee = new EmployeeViewModel
            {
                PersonnelNumber = $"PN{i + 1000}",
                PasswordHash = Guid.NewGuid().ToString("N"),
                Name = new EmployeeNameViewModel
                {
                    FirstName = GetRandomFirstName(),
                    LastName = GetRandomLastName()
                },
                Emails = new List<EmployeeEmailViewModel>
                {
                    new EmployeeEmailViewModel
                    {
                        EmployeeId = Guid.NewGuid(),
                        Email = $"{GetRandomFirstName().ToLower()}.{GetRandomLastName().ToLower()}@example.com",
                        IsPrimary = true
                    }
                },
                PhoneNumbers = new List<EmployeePhoneNumberViewModel>
                {
                    new EmployeePhoneNumberViewModel
                    {
                        EmployeeId = Guid.NewGuid(),
                        PhoneNumber = GetRandomPhoneNumber()
                    }
                },
                Address = new AddressViewModel
                {
                    Street = GetRandomStreet(),
                    HouseNumber = GetRandomHouseNumber(),
                    City = GetRandomCity(),
                    PostalCode = GetRandomPostalCode(),
                    State = GetRandomState(),
                    Country = "USA"
                },
                Roles = new List<RoleViewModel>
                {
                    new RoleViewModel
                    {
                        Name = GetRandomRoleName(),
                        Description = "General Employee Role"
                    }
                },
                Birthday = GetRandomDateOfBirth(),
                Created = createdDate,
                LastChanged = lastChangedDate,
            };
            employees.Add(employee);
        }

        return employees;
    }

    // Random data generation methods
    public static string GetRandomFirstName()
    {
        string[] firstNames = { "John", "Jane", "Alex", "Emily", "Michael", "Sarah", "Chris", "Jessica", "David", "Laura" };
        return firstNames[new Random().Next(firstNames.Length)];
    }

    public static string GetRandomLastName()
    {
        string[] lastNames = { "Smith", "Johnson", "Brown", "Taylor", "Anderson", "Thomas", "Jackson", "White", "Harris", "Martin" };
        return lastNames[new Random().Next(lastNames.Length)];
    }

    public static string GetRandomPhoneNumber()
    {
        Random rand = new Random();
        return $"({rand.Next(200, 999)}) {rand.Next(200, 999)}-{rand.Next(1000, 9999)}";
    }

    public static string GetRandomStreet()
    {
        string[] streets = { "Main St", "Broadway", "Maple Ave", "Elm St", "Pine St", "Cedar St" };
        return streets[new Random().Next(streets.Length)];
    }

    public static string GetRandomHouseNumber()
    {
        return new Random().Next(100, 999).ToString();
    }

    public static string GetRandomCity()
    {
        string[] cities = { "New York", "Los Angeles", "Chicago", "Houston", "Phoenix", "Philadelphia", "San Antonio", "San Diego", "Dallas", "San Jose" };
        return cities[new Random().Next(cities.Length)];
    }

    public static string GetRandomPostalCode()
    {
        return new Random().Next(10000, 99999).ToString();
    }

    public static string GetRandomState()
    {
        string[] states = { "CA", "TX", "NY", "FL", "IL", "PA", "OH", "MI", "NC", "GA" };
        return states[new Random().Next(states.Length)];
    }

    public static string GetRandomRoleName()
    {
        string[] roles = { "Administrator", "Manager", "Developer", "Sales", "Support", "HR", "Finance", "Marketing", "Operations" };
        return roles[new Random().Next(roles.Length)];
    }

    public static DateTime GetRandomDateOfBirth()
    {
        Random rand = new Random();
        DateTime start = new DateTime(1970, 1, 1);
        int range = (DateTime.Today - start).Days;
        return start.AddDays(rand.Next(range));
    }

    public static DateTime GetRandomCreatedDate()
    {
        Random rand = new Random();
        // Generate a random created date between 5 years ago and today
        DateTime start = DateTime.Now.AddYears(-5);
        int range = (DateTime.Now - start).Days;
        return start.AddDays(rand.Next(range)).AddHours(rand.Next(0, 24)).AddMinutes(rand.Next(0, 60));
    }

    public static DateTime GetRandomLastChangedDate(DateTime createdDate)
    {
        Random rand = new Random();
        // Generate a random last changed date between the created date and today
        int range = (DateTime.Now - createdDate).Days;
        return createdDate.AddDays(rand.Next(range + 1)).AddHours(rand.Next(0, 24)).AddMinutes(rand.Next(0, 60));
    }
}
