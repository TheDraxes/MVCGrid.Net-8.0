using MVCGrid.Models;
using MVCGrid.Net_Core_Example.Models;
using MVCGrid.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCGrid.Net_Core_Example.Grids
{
    public static class GridTest
    {
        public static MVCGridBuilder<Person> Test()
        {

            ColumnDefaults colDefauls = new ColumnDefaults()
            {
                EnableSorting = true
            };

            return new MVCGridBuilder<Person>(colDefauls)
                .WithAuthorizationType(AuthorizationType.AllowAnonymous)
                .WithSorting(sorting: true, defaultSortColumn: "Id", defaultSortDirection: SortDirection.Dsc)
                .WithPaging(paging: true, itemsPerPage: 10, allowChangePageSize: true, maxItemsPerPage: 100)
                .WithAdditionalQueryOptionNames("search")
                .AddColumns(cols =>
                {
                    cols.Add("FirstName").WithHeaderText("First Name")
                        .WithVisibility(true, true)
                        .WithValueExpression(p => p.FirstName);
                    cols.Add("LastName").WithHeaderText("Last Name")
                        .WithVisibility(true, true)
                        .WithValueExpression(p => p.LastName);
                    cols.Add("FullName").WithHeaderText("Full Name")
                        .WithValueTemplate("{Model.FirstName} {Model.LastName}")
                        .WithVisibility(visible: false, allowChangeVisibility: true)
                        .WithSorting(false);
                    cols.Add("StartDate").WithHeaderText("Start Date")
                        .WithVisibility(visible: true, allowChangeVisibility: true)
                        .WithValueExpression(p => p.StartDate == null ? p.StartDate.ToShortDateString() : "");
                    cols.Add("Status")
                        .WithSortColumnData("Active")
                        .WithVisibility(visible: true, allowChangeVisibility: true)
                        .WithHeaderText("Status")
                        .WithValueExpression(p => p.Active ? "Active" : "Inactive")
                        .WithCellCssClassExpression(p => p.Active ? "success" : "danger");
                    cols.Add("Gender").WithValueExpression((p, c) => p.Gender)
                        .WithAllowChangeVisibility(true);
                    cols.Add("Email")
                        .WithVisibility(visible: false, allowChangeVisibility: true)
                        .WithValueExpression(p => p.Email);
                })
                //.WithAdditionalSetting(MVCGrid.Rendering.BootstrapRenderingEngine.SettingNameTableClass, "notreal") // Example of changing table css class
                .WithRetrieveDataMethod((context) =>
                {
                    Person person = new Person()
                    {
                        Id = 0,
                        FirstName = "Alpha",
                        LastName = "Test",
                        Active = true,
                        Email = "test@gmail.com",
                        Employee = true,
                        Gender = "Male",
                        StartDate = DateTime.Now,
                    };
                    List<Person> persons = new List<Person>();
                    for (int x = 0; 15 > x; x++)
                    {
                        persons.Add(person);
                    }

                    return new QueryResult<Person>()
                    {
                        Items = persons,
                        TotalRecords = persons.Count
                    };
                });
            
        }
    }
}
