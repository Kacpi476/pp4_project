using EShop.Domain.Models;
using EShop.Domain.Repositories;

namespace EShop.Application.Service;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync() => await _userRepository.GetAllUsersAsync();
    public async Task<User?> GetUserByIdAsync(int id) => await _userRepository.GetUserByIdAsync(id);
    public async Task<User?> GetUserByUsernameAsync(string username) => await _userRepository.GetUserByUsernameAsync(username);

    public async Task<User> AddUserAsync(User user)
    {
        if (string.IsNullOrWhiteSpace(user.Username))
            throw new Exception("Username is required");
        if (string.IsNullOrWhiteSpace(user.PasswordHash))
            throw new Exception("PasswordHash is required");
        return await _userRepository.AddUserAsync(user);
    }

    public async Task<User?> UpdateUserAsync(User user)
    {
        return await _userRepository.UpdateUserAsync(user);
    }

    public async Task<bool> DeleteUserAsync(int id)
    {
        return await _userRepository.DeleteUserAsync(id);
    }
}