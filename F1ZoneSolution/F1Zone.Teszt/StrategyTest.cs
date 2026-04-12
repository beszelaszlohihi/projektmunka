using Blazored.LocalStorage;
using Bunit;
using Bunit.TestDoubles;
using F1Zone.BlazorApp.Components.Pages;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using RichardSzalay.MockHttp;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using Xunit;

// IDE ÍRD BE A SAJÁT NAMESPACE-EDET!
// using F1Zone.Pages; 
// using F1Zone.Models; // Ha a 'Circuits' osztály külön mappában van

namespace F1Zone.Tests
{
    public class StrategyTests : TestContext
    {
        private readonly MockHttpMessageHandler _mockHttp;
        private readonly Mock<ILocalStorageService> _mockLocalStorage;

        public StrategyTests()
        {
            _mockHttp = new MockHttpMessageHandler();
            var httpClient = _mockHttp.ToHttpClient();
            httpClient.BaseAddress = new Uri("http://localhost/");
            Services.AddSingleton(httpClient);

            _mockLocalStorage = new Mock<ILocalStorageService>();
            Services.AddSingleton(_mockLocalStorage.Object);

            // A bUnit beépített JSInterop kamuja (fontos a grafikon teszteléséhez!)
            JSInterop.Mode = JSRuntimeMode.Loose;
        }

        [Fact]
        public void Strategy_NincsBejelentkezve_VisszadobALoginra()
        {
            // ARRANGE: Azt hazudjuk a LocalStorage-nak, hogy nincs bejelentkezve (userId = 0)
            _mockLocalStorage.Setup(x => x.GetItemAsync<int>("userId", It.IsAny<CancellationToken>()))
                             .ReturnsAsync(0);

            var navMan = Services.GetRequiredService<NavigationManager>();

            // ACT
            var cut = Render<Strategy>();

            // ASSERT: Ellenőrizzük, hogy azonnal átirányított-e a /login oldalra
            Assert.Equal("http://localhost/login", navMan.Uri);
        }

        [Fact]
        public void Strategy_SikeresBetoltes_PalyakMegjelennekAListaban()
        {
            
            _mockLocalStorage.Setup(x => x.GetItemAsync<int>("userId", It.IsAny<CancellationToken>())).ReturnsAsync(1);

            // Mockoljuk az API-t: Visszaadunk 2 kamu pályát
            string fakeCircuitsJson = "[{\"circuit_id\": 1, \"circuit_name\": \"Hungaroring\", \"country\": \"Magyarország\"}, {\"circuit_id\": 2, \"circuit_name\": \"Monza\", \"country\": \"Olaszország\"}]";
            _mockHttp.When("*")
                     .Respond("application/json", fakeCircuitsJson);


            var cut = Render<Strategy>();

            // Várunk, amíg betölt a HTML-be a select (mert API-ról jön)
            cut.WaitForState(() => cut.FindAll("select option").Count > 0);

            // ASSERT: Megnézzük, hogy tényleg benne vannak-e a pályák a legördülőben
            var options = cut.FindAll("select option");
            Assert.Contains(options, opt => opt.TextContent.Contains("Hungaroring"));
            Assert.Contains(options, opt => opt.TextContent.Contains("Monza"));
        }

        [Fact]
        public async Task Strategy_SzamitasGomb_MeghivjaAGrafikonRajzoloJavaScriptet()
        {
            _mockLocalStorage.Setup(x => x.GetItemAsync<int>("userId", It.IsAny<CancellationToken>())).ReturnsAsync(1);

            // Itt is csillagot használunk a biztonság kedvéért
            _mockHttp.When("*").Respond("application/json", "[{\"circuit_id\": 1, \"circuit_name\": \"Hungaroring\", \"length_km\": 4.381, \"turns\": 14}]");

            var jsMock = JSInterop.SetupVoid("setupStrategyChart", _ => true);

            var cut = Render<Strategy>();
            cut.WaitForState(() => cut.FindAll("button").Count > 0);

            var calcButton = cut.Find("button.hero-btn");
            calcButton.Click();

            // VÁRUNK 250 milliszekundumot, hogy a te kódodban lévő 'await Task.Delay(150)' biztosan lefusson!
            await Task.Delay(250);

            jsMock.SetVoidResult();
            JSInterop.VerifyInvoke("setupStrategyChart", 1);
        }
    }
}