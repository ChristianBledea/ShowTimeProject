using Microsoft.AspNetCore.Identity;
using ShowTime.BusinessLogic.Abstractions;
using ShowTime.BusinessLogic.Dtos;
using ShowTime.DataAccess.Models;
using ShowTime.DataAccess.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowTime.BusinessLogic.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;

        public UserService(IUserRepository userRepository, IPasswordHasher<User> passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<LoginResponseDto> LoginAsync(LoginDto loginDto)
        {
            var user = await _userRepository.GetByEmail(loginDto.Email);

            if (user == null)
            {
                Console.WriteLine("User is null");
                throw new Exception("Invalid credentials");
            }
            else
            {
                Console.WriteLine($"User found: {user.Email}");
            }

            if (user.Password == null)
            {
                Console.WriteLine("User password is null");
                throw new Exception("Invalid credentials");
            }
            else
            {
                Console.WriteLine($"Stored hash: {user.Password}");
            }

            Console.WriteLine($"Entered password: {loginDto.Password}");

            var passwordValid = _passwordHasher.VerifyHashedPassword(user, user.Password, loginDto.Password) == PasswordVerificationResult.Success;

            if (!passwordValid)
            {
                Console.WriteLine("Password invalid");
                throw new Exception("Invalid credentials");
            }

            if (user.Role == null)
            {
                Console.WriteLine("User role is null");
                throw new Exception("User role missing");
            }
            else
            {
                Console.WriteLine($"User role found: {user.Role.Name}");
            }

            return new LoginResponseDto
            {
                Email = user.Email,
                Role = user.Role.Name,
                Id= user.Id
            };
        }


        public async Task RegisterAsync(RegisterDto registerDto)
        {
            var existingUser = await _userRepository.GetByEmail(registerDto.Email);
            if (existingUser != null)
            {
                throw new Exception("Email already exists");
            }

            var newUser = new User
            {
                Email = registerDto.Email,
                RoleId = 1
            };

            newUser.Password = _passwordHasher.HashPassword(newUser, registerDto.Password);
            await _userRepository.Add(newUser);
        }
    }
}
