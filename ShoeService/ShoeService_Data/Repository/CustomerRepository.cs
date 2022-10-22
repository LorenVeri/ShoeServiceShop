using AutoMapper;
using ShoeService_Common.Helpers;
using ShoeService_Data.IRepository;
using ShoeService_Model.Dtos;
using ShoeService_Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeService_Data.Repository
{
    public class CustomerRepository : RepositoryBase<Customer>, ICustomerRepository
    {
        private readonly IMapper _mapper;
        public CustomerRepository(ShoeServiceDbContext dataContext, IMapper mapper) : base(dataContext)
        {
            _mapper = mapper;
        }

        public bool GetByCustomerName(string CustomerName)
        {
            throw new NotImplementedException();
        }

        public bool GetByEmail(string Email)
        {
            throw new NotImplementedException();
        }

        public Customer Login(LoginDto loginDto)
        {
            Customer customer = _dbContext.Customers.SingleOrDefault(x => x.CustomerEmail == loginDto.Email && x.PasswordHash == ConvertMD5.CreateMD5(loginDto.Password) && x.IsDeleted == false);
            return customer;
        }

        public Customer Register(RegisterDto registerDto)
        {
            var customer = _mapper.Map<Customer>(registerDto);
            customer.PasswordHash = ConvertMD5.CreateMD5(registerDto.Password);
            customer.CustomerName = ConvertString.convertToUnSign($"{registerDto.FirstName}{registerDto.LastName}").ToLower();
            customer.MemberShipId = 6;
            _dbContext.Customers.Add(customer);
            return customer;
        }
    }
}
