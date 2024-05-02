using System.Net.Http.Json;

namespace CatFacts;

public partial class MainPage : ContentPage {
    private readonly HttpClient _client;

    public MainPage() {
        Connectivity.ConnectivityChanged += RequireNetwork;
        RequireNetwork(null, null);

        _client = new HttpClient();

        InitializeComponent();
    }

    private async void RequireNetwork(object? sender, ConnectivityChangedEventArgs? e) {
        if (Connectivity.Current.NetworkAccess != NetworkAccess.None) return;

        var answer = await DisplayAlert("A network connection is required...",
                                        "A network connection is required to use this app, either connect to a network or the app close.",
                                        "Understood", "Exit");

        if(answer) RequireNetwork(null, null);
        else Application.Current?.Quit();
    }

    private async void GetFactButton_OnPressed(object? sender, EventArgs e) {
        var response = await _client.GetFromJsonAsync("https://catfact.ninja/fact", typeof(CatFact));

        if (response is CatFact catFact)
            CatFactLabel.Text = catFact.Fact;
    }
}