using MVCGrid.Models;
using MVCGrid.Net_Core_Example.Models;
using MVCGrid.NetCore;
using MVCGrid.NetCore.SignalR.Extensions;
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
                .AddColumns(cols =>
                {
                    cols.Add("Id").WithValueExpression((p, c) => p.Id.ToString())
                        .WithAllowChangeVisibility(true);
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
                .WithRetrieveDataMethod((context) =>
                {
                    QueryOptions queryOptions = context.QueryOptions;
                    int pageIndex = queryOptions.PageIndex.Value;
                    int pageSize = queryOptions.ItemsPerPage.Value;
                    int totalCount = 120;
                    List<Person> persons = new List<Person>();
                    for (int x = 0; totalCount > x; x++)
                    {
                        Person person = new Person()
                        {
                            Id = x,
                            FirstName = "Alpha",
                            LastName = "Shabazz",
                            Active = true,
                            Email = "test@gmail.com",
                            Employee = true,
                            Gender = "Male",
                            StartDate = DateTime.Now,
                        };
                        persons.Add(person);
                    }
                    persons = persons.Skip(pageIndex * pageSize).Take(pageSize).ToList();

                    return new QueryResult<Person>()
                    {
                        Items = persons,
                        TotalRecords = totalCount
                    };
                });
            
        }

        public static SignalRMVCGridBuilder Test2()
        {
            ColumnDefaults colDefauls = new ColumnDefaults()
            {
                EnableSorting = true
            };

            return new SignalRMVCGridBuilder("TestGrid2", SignalRGridType.Individual, colDefauls)
                .WithAuthorizationType(AuthorizationType.AllowAnonymous)
                .WithSorting(sorting: true, defaultSortColumn: "Id", defaultSortDirection: SortDirection.Dsc)
                .WithPaging(paging: true, itemsPerPage: 10, allowChangePageSize: true, maxItemsPerPage: 100)
                .AddColumns(cols =>
                {
                    cols.Add("Id").WithValueExpression((p, c) => p.Id.ToString())
                        .WithAllowChangeVisibility(true);
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
                .ToSignalRGrid();
        }
    }
}
