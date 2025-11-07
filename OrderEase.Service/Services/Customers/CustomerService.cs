using Microsoft.EntityFrameworkCore;
using OrderEase.DataAccess.Repositories;
using OrderEase.Domain.Entitites;
using OrderEase.Service.Exceptions;
using OrderEase.Service.Services.Customers.Models;

namespace OrderEase.Service.Services.Customers
{
    public class CustomerService(IRepository<Customer> customerRepository) : ICustomerService
    {
        public async Task<long> CreateAsync(CustomerCreateModel model)
        {
            var createdCustomer = await customerRepository.InsertAsync(new Customer
            {
                Address = model.Address,
                CreatedDate = DateTime.UtcNow,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
            });

            return createdCustomer.Id;
        }

        public async Task UpdateAsync(long id, CustomerUpdateModel model)
        {
            var customerForUpdation = await customerRepository.SelectAsync(id)
                ?? throw new NotFoundException($"No customer was found with ID = {id}");

            customerForUpdation.FirstName = model.FirstName;
            customerForUpdation.LastName = model.LastName;
            customerForUpdation.PhoneNumber = model.PhoneNumber;
            customerForUpdation.Email = model.Email;
            customerForUpdation.Address = model.Address;

            await customerRepository.UpdateAsync(customerForUpdation);
        }

        public async Task DeleteAsync(long id)
        {
            var customerForDeletion = await customerRepository.SelectAsync(id)
                ?? throw new NotFoundException($"No customer was found with ID = {id}");

            await customerRepository.DeleteAsync(customerForDeletion);    
        }

        public async Task<CustomerViewModel> GetAsync(long id)
        {
            var customer = await customerRepository.SelectAsync(id)
                ?? throw new NotFoundException($"No customer was found with ID = {id}");

            return new CustomerViewModel
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                PhoneNumber = customer.PhoneNumber,
                Email = customer.Email,
                Address = customer.Address
            };
        }

        public async Task<List<CustomerViewModel>> GetAllAsync(string name = null, string phone = null, string email = null)
        {
            var query = customerRepository
                .SelectAllAsQueryable()
                .Where(c => !c.IsDeleted);

            if (!string.IsNullOrWhiteSpace(name))
            {
                name = name.ToLower();
                query = query.Where(c => 
                c.FirstName.ToLower().Contains(name) || 
                c.LastName.ToLower().Contains(name));
            }

            if (!string.IsNullOrWhiteSpace(phone))
            {
                query = query.Where(c => c.PhoneNumber.ToLower().Contains(phone.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(email))
            {
                query = query.Where(c => c.Email.ToLower().Contains(email.ToLower()));
            }

            return await query
                .Select(customer => new CustomerViewModel
                {
                    Id = customer.Id,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    PhoneNumber = customer.PhoneNumber,
                    Email = customer.Email,
                    Address = customer.Address
                }).ToListAsync();
        }
    }
}
