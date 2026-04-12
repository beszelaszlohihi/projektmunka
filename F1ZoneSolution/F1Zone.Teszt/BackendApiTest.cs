using F1Zone.API.Controllers;
using F1Zone.API.INTERFACE;
using F1ZoneLibrary.DATA;
using F1ZoneLibrary.Dto;
using F1ZoneLibrary.MODEL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace F1Zone.Tests
{
    public class BackendApiTests
    {
        
        [Fact]
        public async Task GetDriversForAdmin_VisszaadjaAPilotakat_Es200OKStátusztAd()
        {
            
            var mockService = new Mock<IGenericF1ZoneService<Drivers>>();

            // Szimulálok egy listát, amit a szerviz visszaad
            mockService.Setup(s => s.GetAll())
                       .ReturnsAsync(new List<Drivers> {
                           new Drivers { driver_id = 1, driver_name = "Lewis Hamilton", teamname = "Mercedes" }
                       });

            // Létrehozom a Controllert. A DbContext helyére null-t teszek            
            var controller = new DriversController(mockService.Object, null);

            
            var actionResult = await controller.GetDriversForAdmin();

            
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var drivers = Assert.IsAssignableFrom<IEnumerable<DriverDto>>(okResult.Value);
            Assert.Single(drivers);
        }

        
        [Fact]
        public async Task UpdateDriver_NemLetezoIdEseten_404NotFoundHibatDob()
        {
            
            var mockService = new Mock<IGenericF1ZoneService<Drivers>>();

            // Azt mondom, hogy a 999-es ID-ra a szerviz nem talál semmit (null jön vissza)
            mockService.Setup(s => s.GetById(999)).ReturnsAsync((Drivers)null);

            var controller = new DriversController(mockService.Object, null);
            var fakeUpdateDto = new DriverDto { driver_name = "Teszt" };

            var actionResult = await controller.UpdateDriver(999, fakeUpdateDto);

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(actionResult);
            Assert.Equal("A pilóta nem található.", notFoundResult.Value);
        }

        
        // DIREKT BUKÓ TESZT (Backend): Sikeres frissítésnél szándékosan hibát várunk
        [Fact]
        public async Task UpdateDriver_DirektHiba_RosszValasztVarunkSikeresMentesnel()
        {
            //Azt mondom a szerviznek, hogy a pilóta létezik
            var mockService = new Mock<IGenericF1ZoneService<Drivers>>();

            mockService.Setup(s => s.GetById(1))
                       .ReturnsAsync(new Drivers { driver_id = 1, driver_name = "Eredeti Nev" });

            // Szimulálom, hogy az Update metódus is lefut hiba nélkül
            mockService.Setup(s => s.Update(It.IsAny<Drivers>())).Returns(Task.CompletedTask);

            // A DbContext helye ismét null, mert itt is csak a service-t használom
            var controller = new DriversController(mockService.Object, null);
            var updateDto = new DriverDto { driver_name = "Uj Nev" };

            //Lefuttatom a frissítést az 1-es ID-val
            var actionResult = await controller.UpdateDriver(1, updateDto);

            //itt a tesztben viszont szándékosan azt állítjuk, hogy a Controllernek 'BadRequest'-et (400) kellett volna dobnia!
            Assert.IsType<BadRequestObjectResult>(actionResult);
        }

    }
}