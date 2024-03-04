using AbcParcel.Common.Contract;
using AbcParcel.Common;
using AbcParcel.Data;
using AbcParcel.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AbcParcel.Services.UserServices
{
    public interface IUserService
    {
        Task<Applicationuser> GetUserByEmail(string email);
        Task<Applicationuser> GetUserById(string userId);
        Task<Response<Applicationuser>> GetUserByUserName(string userName);
        Task<SignInResult> Login(string userName, string password, bool rememberMe);
        Task Logout();
        Task<Response<SignInResult>> RegisterAdmin(RegisterAdminViewModel user);
        Task<IdentityResult> RegisterUser(Applicationuser user, string password);
    }
    public class UserService : IUserService
    {
        private readonly UserManager<Applicationuser> _userManager;
        private readonly SignInManager<Applicationuser> _signInManager;
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        public UserService(UserManager<Applicationuser> userManager, SignInManager<Applicationuser> signInManager,
            ApplicationDbContext dbContext, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<SignInResult> Login(string userName, string password, bool rememberMe)
        {
            var result = await _signInManager.PasswordSignInAsync(userName, password, rememberMe, lockoutOnFailure: false);
            return result;
        }
        public async Task<IdentityResult> RegisterUser(Applicationuser user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            var entity = await _dbContext.Users.FirstOrDefaultAsync(c => c.UserName == user.UserName);
            if (entity != null)
            {
                entity.UserType = UserType.Customer;
                var updateResult = _dbContext.Users.Update(entity);
                await _dbContext.SaveChangesAsync();
            }
            return result;
        }
        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }
        public async Task<Applicationuser> GetUserById(string userId)
        {
            return await _userManager.FindByIdAsync(userId);
        }

        public async Task<Applicationuser> GetUserByEmail(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }
        public async Task<Response<Applicationuser>> GetUserByUserName(string userName)
        {
            var response = Response<Applicationuser>.Failed(string.Empty);
            try
            {
                var entity = await _dbContext.Users.FirstOrDefaultAsync(c => c.UserName == userName);
                response = Response<Applicationuser>.Success(entity);
            }
            catch (Exception ex)
            {
                response = Response<Applicationuser>.Failed(ex.Message);
            }
            return await Task.FromResult(response);
        }
        public async Task<Response<SignInResult>> RegisterAdmin(RegisterAdminViewModel user)
        {
            var response = Response<SignInResult>.Failed(string.Empty);
            try
            {
                var entity = await _userManager.FindByEmailAsync(user.Email);
                if (entity != null)
                    return Response<SignInResult>.Failed("User Already Exists");

                var mappedEntities = _mapper.Map(user, entity);
                var userName = Helpers.GetEmailDomain(user.Email);
                mappedEntities.UserName = userName;

                if (user.Password != user.ConfirmPassword)
                    return Response<SignInResult>.Failed("Passwords do not match");

                var adminUser = await _userManager.CreateAsync(mappedEntities, user.Password);
                if (adminUser.Succeeded)
                {
                    var findAdmin = await _userManager.FindByEmailAsync(user.Email);
                    findAdmin.UserType = UserType.Admin;
                    _dbContext.Applicationusers.Update(findAdmin);
                    await _dbContext.SaveChangesAsync();
                    bool rememberMe = true;
                    var signInAdmin = await _signInManager.PasswordSignInAsync(findAdmin.UserName, user.Password, rememberMe, lockoutOnFailure: false);
                    if (signInAdmin.Succeeded)
                    {
                        response = Response<SignInResult>.Success(signInAdmin);
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return await Task.FromResult(response);
        }
    }
}
