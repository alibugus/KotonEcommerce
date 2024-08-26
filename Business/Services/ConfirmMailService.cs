using Data.Models;
using Data.Repositories;

namespace Data.Services
{
    public class ConfirmMailService
    {
        private readonly UserRepository _userRepository;

        public ConfirmMailService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        
        public async Task<bool> ConfirmUserAsync(ConfirmUser confirmUser)
        {
            var user = await _userRepository.FindByEmailAsync(confirmUser.Email);
            if (user != null && user.ConfirmCode == confirmUser.ConfirmCode)
            {
                user.EmailConfirmed = true;
                await _userRepository.UpdateAsync(user);
                return true;
            }
            return false;
        }
    }
}
