namespace WebApi.Services;

public class UserManager
{
    private static readonly List<User> _users =
    [
        new() { Id = "1", FirstName = "Hans", LastName = "Mattin-Lassei", Email = "hans@avanoria.se" },
        new() { Id = "2", FirstName = "Anna", LastName = "Andersson", Email = "anna@avanoria.se" }
    ];

    public async Task<IEnumerable<User>> GetUsersAsync()
    {
        return _users;
    }
}
