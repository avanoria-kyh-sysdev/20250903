using System.Net.Http.Json;

async Task GetUsersAsync()
{
    using var client = new HttpClient();
    var data = await client.GetFromJsonAsync<IEnumerable<User>>("https://localhost/api/users");

    Lv_UserList.ItemsSource = data;
}