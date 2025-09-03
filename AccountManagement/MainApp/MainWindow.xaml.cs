using MainApp.Models;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows;


namespace MainApp;

public partial class MainWindow : Window
{


    public MainWindow()
    {
        InitializeComponent();

    }


    public async Task GetUsersAsync()
    {
        using var client = new HttpClient();
        var data = await client.GetFromJsonAsync<ObservableCollection<User>>("https://localhost/api/users");
    }

}