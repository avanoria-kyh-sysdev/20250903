using Grpc.Core;

namespace WebApi.Services;

public class UserService(UserManager userManager) : UserGrpcService.UserGrpcServiceBase
{
    private readonly UserManager _userManager = userManager;

    public override async Task<GetUsersResponse> GetUsers(GetUsersRequest request, ServerCallContext context)
    {
        var response = new GetUsersResponse
        {
            Success = true,
            StatusCode = 200,
            Error = "",
        };

        var users = await _userManager.GetUsersAsync();
        response.Users.AddRange(users);
        return response;

    }

}
