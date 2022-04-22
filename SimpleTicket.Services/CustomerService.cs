using SimpleTicket.Data;
using SimpleTicket.Models.CustomerModels;
using SimpleTicket.Models.TicketModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTicket.Services
{
    // .----------------.  .----------------.  .----------------.  .----------------.  .----------------.  .----------------.  .----------------.  .----------------.  .----------------. 
    //| .--------------. || .--------------. || .--------------. || .--------------. || .--------------. || .--------------. || .--------------. || .--------------. || .--------------. |
    //| |   ______     | || |     ______   | || |    _______   | || |  _________   | || |  _______     | || | ____ ____    | || |     _____    | || |     ______   | || |  _________   | |
    //| |  |_ __   \   | || |   .' ___  |  | || |   /  ___  |  | || | |_   ___  |  | || | |_   __ \    | || ||_  _| |_  _| | || |    |_   _|   | || |   .' ___  |  | || | |_ ___  |  | |
    //| |    | |__) |  | || |  / .'   \_|  | || |  |  (__ \_|  | || |   | |_  \_|  | || |   | |__) |   | || |  \ \   / /   | || |      | |     | || |  / .'   \_|  | || |   | |_  \_|  | |
    //| |    |  ___/   | || |  | |         | || |   '.___`-.   | || |   |  _|  _   | || |   |  __ /    | || |   \ \ / /    | || |      | |     | || |  | |         | || |   |  _|  _   | |
    //| |   _| |_      | || |  \ `.___.'\  | || |  |`\____) |  | || |  _| |___/ |  | || |  _| |  \ \_  | || |    \ ' /     | || |     _| |_    | || |  \ `.___.'\  | || |  _| |___/ |  | |
    //| |  |_____|     | || |   `._____.'  | || |  |_______.'  | || | |_________|  | || | |____| |___| | || |     \_/      | || |    |_____|   | || |   `._____.'  | || | |_________|  | |
    //| |              | || |              | || |              | || |              | || |              | || |              | || |              | || |              | || |              | |
    //| '--------------' || '--------------' || '--------------' || '--------------' || '--------------' || '--------------' || '--------------' || '--------------' || '--------------' |
    // '----------------'  '----------------'  '----------------'  '----------------'  '----------------'  '----------------'  '----------------'  '----------------'  '----------------' 

  

    public class CustomerService
    {

        private readonly Guid _userID;

        public CustomerService(Guid userID)
        {
            _userID = userID;
        }

       
        public async Task<bool> CreateCustomerAsync(CustomerCreate model)
        {
            var entity = new Customer()
            {
                Name = model.Name,
                Status = CustomerStatus.Active
            };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Customers.Add(entity);
                return await ctx.SaveChangesAsync() == 1;
            }
        }

        
        public async Task<IEnumerable<CustomerListItem>> GetCustomersAsync()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = await
                            ctx
                            .Customers
                            .Where(e => e.Status == CustomerStatus.Active)
                            .OrderBy(e => e.Name) 
                            .Select(
                                e =>
                                    new CustomerListItem
                                    {
                                        ID = e.ID,
                                        Name = e.Name,
                                        TotalTicketCount = e.Tickets.Count(),
                                        OpenTicketCount = e.Tickets
                                                        .Where(f => f.Status == Status.Open)
                                                        .Select(
                                                                f =>
                                                                    new Models.TicketModels.TicketListItem
                                                                    {
                                                                        TicketID = f.TicketID,
                                                                        Status = f.Status
                                                                    }
                                                                ).ToList().Count()
                                    }
                                ).ToListAsync();
                return query;
            }
        }

        
        public async Task<CustomerDetail> GetCustomerByIDAsync(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = await
                    ctx
                    .Customers
                    .Where(e => e.ID == id)
                    .FirstOrDefaultAsync();
                return
                    new CustomerDetail
                    {
                        ID = entity.ID,
                        Name = entity.Name,
                        Tickets = entity
                                    .Tickets
                                    .Select
                                    (
                                        f =>
                                            new TicketListShortItem
                                            {
                                                TicketID = f.TicketID,
                                                CreatorID = f.CreatorID,
                                                Title = f.Title,
                                                DateCreated = f.DateCreated,
                                                DateUpdated = f.DateUpdated,
                                                Priority = f.Priority,
                                                Status = f.Status
                                            }
                                    ).ToList()
                    };
            }
        }

        
        public async Task<bool> UpdateCustomerAsync(CustomerEdit customer)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = await
                                ctx
                                .Customers
                                .Where(e => e.ID == customer.ID)
                                .FirstOrDefaultAsync();
                entity.Name = customer.Name;
                entity.Status = customer.Status;
                return await ctx.SaveChangesAsync() == 1;
            }
        }

        
        public async Task<bool> SoftDeleteCustomerAsync(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = await
                                ctx
                                .Customers
                                .Where(e => e.ID == id)
                                .FirstOrDefaultAsync();
                entity.Status = CustomerStatus.Deactivated;
                return await ctx.SaveChangesAsync() == 1;
            }
        }

        public async Task<bool> HardDeleteCustomerAsync(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = await
                    ctx
                        .Customers
                        .Where(e => e.ID == id)
                        .FirstOrDefaultAsync();
                ctx.Customers.Remove(entity);
                return await ctx.SaveChangesAsync() == 1;
            }
        }

    }
}


