using Blazored.LocalStorage;
using Bunit;
using Bunit.TestDoubles;
using F1Zone.BlazorApp.Components.Pages;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using RichardSzalay.MockHttp;
using System;
using System.Net.Http;
using Xunit;



namespace F1Zone.Tests
{
    public class LoginTests : TestContext
    {
        private readonly MockHttpMessageHandler _mockHttp;
        private readonly Mock<ILocalStorageService> _mockLocalStorage;

        public LoginTests()
        {
            _mockHttp = new MockHttpMessageHandler();
            var httpClient = _mockHttp.ToHttpClient();
            // A kódom relatív URL-t használ ("api/auth/login"), ehhez kell egy alap cím
            httpClient.BaseAddress = new Uri("http://localhost/");
            Services.AddSingleton(httpClient);

            _mockLocalStorage = new Mock<ILocalStorageService>();
            Services.AddSingleton(_mockLocalStorage.Object);
        }

        [Fact]
        public void Login_MezokKotelezoek_HTMLValidacio()
        {
            var cut = Render<Login>();

            // Ellenõrzöm, hogy az input mezõk megkapták-e a 'required' attribútumot, ami megakadályozza az üres form elküldését.
            var emailInput = cut.Find("input[type='email']");
            var passwordInput = cut.Find("input[type='password']");

            Assert.True(emailInput.HasAttribute("required"));
            Assert.True(passwordInput.HasAttribute("required"));
        }

        [Fact]
        public void Login_KezdolapGomb_JoHelyreMutat()
        {
            var cut = Render<Login>();

            // Megkeresem a logót, ami a kezdõlapra visz a te kódodban
            var logoLink = cut.Find("a.logo");

            // Ellenõrzöm, hogy a "/" címre visz-e
            Assert.Equal("/", logoLink.GetAttribute("href"));
            Assert.Contains("F1", logoLink.TextContent);
        }

        [Fact]
        public void Login_MezokKitoltese_KetiranyuAdatkotesSikeres()
        {
            var cut = Render<Login>();

            // Gépelés szimulálása a bind-value mezõkbe
            cut.Find("input[type='email']").Change("admin@f1zone.hu");
            cut.Find("input[type='password']").Change("Titkos123");

            // Ellenõrzés
            Assert.Equal("admin@f1zone.hu", cut.Find("input[type='email']").GetAttribute("value"));
            Assert.Equal("Titkos123", cut.Find("input[type='password']").GetAttribute("value"));
        }

        [Fact]
        public void Login_HibasAdatok_HibaUzenetetJelenitMeg()
        {
            // API mock: 400 Bad Request vagy 401 Unauthorized hiba szimulálása
            _mockHttp.When("http://localhost/api/auth/login")
                     .Respond(System.Net.HttpStatusCode.Unauthorized);

            var cut = Render<Login>();

            // Kitöltöm és elküldöm a formot
            cut.Find("input[type='email']").Change("rossz@f1zone.hu");
            cut.Find("input[type='password']").Change("rossz");
            cut.Find("button[type='submit']").Click();

            // Várok, amíg a te kódbeli <div class="error-msg"> megjelenik a HTML-ben
            cut.WaitForState(() => cut.FindAll(".error-msg").Count > 0);

            var hibaDoboz = cut.Find(".error-msg");
            Assert.Equal("Hibás email vagy jelszó!", hibaDoboz.TextContent);
        }

        [Fact]
        public void Login_SikeresBejelentkezes_MentiAzAdatokat_EsAtiranyit()
        {
            // API mock: 200 OK és egy UserDto-hoz hasonló JSON visszaadása
            string fakeResponse = "{\"Token\": \"kamutoken123\", \"Username\": \"AdminPisti\", \"Id\": 1, \"Role\": \"Admin\"}";
            _mockHttp.When("http://localhost/api/auth/login")
                     .Respond("application/json", fakeResponse);

            var navMan = Services.GetRequiredService<NavigationManager>();

            var cut = Render<Login>();

            // Kitöltöm és elküldöm a formot
            cut.Find("input[type='email']").Change("admin@f1zone.hu");
            cut.Find("input[type='password']").Change("joJelszo123");
            cut.Find("button[type='submit']").Click();

            // Várok az átirányításra a fõoldalra ("/")
            try
            {
                cut.WaitForState(() => navMan.Uri == "http://localhost/");
            }
            catch (Exception) { /* timeout ignorálása */ }

            // 1. Ellenõrûzöm, hogy jó helyre (a fõoldalra) irányított-e át
            Assert.Equal("http://localhost/", navMan.Uri);

            // 2. Ellenõrzöm, hogy a localStorage SetItemAsync lefutott-e a tokennel
            _mockLocalStorage.Verify(x => x.SetItemAsync("token", "kamutoken123", It.IsAny<CancellationToken>()), Times.Once);
            _mockLocalStorage.Verify(x => x.SetItemAsync("userRole", "Admin", It.IsAny<CancellationToken>()), Times.Once);
            _mockLocalStorage.Verify(x => x.SetItemAsync("isAuthenticated", true, It.IsAny<CancellationToken>()), Times.Once);
        }

        
        // DIREKT BUKÓ TESZT: Azt várom, hogy a gomb szövege "KUTYAFÜLE" legyen
        [Fact]
        public void Login_DirektHiba_RosszGombSzovegetKeres()
        {
            
            var cut = Render<Login>();

            //Megkeresem a bejelentkezés gombot
            var loginGomb = cut.Find("button[type='submit']");

            // DIREKT HIBA: Azt állítom, hogy a gombon a "KUTYAFÜLE" feliratnak kell lennie.
            
            Assert.Equal("KUTYAFÜLE", loginGomb.TextContent);
        }
    }
}