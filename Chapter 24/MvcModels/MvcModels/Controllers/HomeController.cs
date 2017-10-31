using System.Collections.Generic;
using System.Web.Mvc;
using MvcModels.Models;
using MvcModels.Infrastructure;

namespace MvcModels.Controllers {
    public class HomeController : Controller {
        public static Dictionary<int, Person> Persons = new Dictionary<int, Person>(){
            {
                1,
                new Person {PersonId = 1, FirstName = "Adam", LastName = "Freeman",
                Role = Role.Admin}
            },
            {
                2,
                new Person {PersonId = 2, FirstName = "Jacqui", LastName = "Griffyth",
                Role = Role.User}
            },
            {
                3,
                new Person {PersonId = 3, FirstName = "John", LastName = "Smith",
                Role = Role.User}
            },
            {
                4,
                new Person {PersonId = 4, FirstName = "Anne", LastName = "Jones",
                    Role = Role.Guest}
            },
        };

        public ActionResult Index(int id = 1) {
            Person dataItem = Persons[id];
            return View(dataItem);
        }

        public ActionResult CreatePerson() {
            return View(new Person());
        }

        [HttpPost]
        public ActionResult CreatePerson(Person model) {
            return View("Index", model);
        }

        public ActionResult DisplaySummary(
            [Bind(Prefix = "HomeAddress", Exclude = "Country")]AddressSummary summary) {
            return View(summary);
        }

        public ActionResult UpdateAddress(int id)
        {
            Person dataItem = Persons[id];
            return View(new AddressSummary(dataItem.HomeAddress ?? new Address()));
        }

        [HttpPost]
        public ActionResult UpdateAddress(int id, [ModelBinder(typeof(DefaultModelBinder))]AddressSummary address)
        {
            Person dataItem = Persons[id];
            if (dataItem.HomeAddress == null)
            {
                dataItem.HomeAddress = new Address();
            }
            dataItem.HomeAddress.City = address.City;

            dataItem.HomeAddress.Country = address.Country;
            return RedirectToAction("Index", new { id = id });
        }

        public ActionResult ViewPerson([ModelBinder(typeof(PersonDatabaseBinder))]Person person)
        {
            return View("Index", person);
        }

        public ActionResult Names(IList<string> names) {
            names = names ?? new List<string>();
            return View(names);
        }

        public ActionResult Address() {
            IList<AddressSummary> addresses = new List<AddressSummary>();
            UpdateModel(addresses);
            return View(addresses);
        }
    }
}
