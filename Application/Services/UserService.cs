using Application.Services.Interfaces;
using AutoMapper;
using Domain.DTO;
using Domain.Model;
using Infrastructure.Repository.Interface;
using Microsoft.Extensions.Configuration;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IGenericRepository<User> _userRepository;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public UserService(
            IConfiguration configuration,
            IGenericRepository<User> userRepository,
            IMapper mapper)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<GenericResponse<UserDTO>> GetByEmailAsync(string email)
        {
            try
            {
                ArgumentException.ThrowIfNullOrEmpty(email);

                var user = await _userRepository
                    .FirstOrDefaultAsync(x => x.Email.ToLower().Equals(email.ToLower()));

                return user != null ?
                    new GenericResponse<UserDTO>(_mapper.Map<UserDTO>(user)) :
                    new GenericResponse<UserDTO>("User not found", false);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while retrieving the user by email", ex);
            }
        }

        public async Task<GenericResponse<bool>> VerifyPasswordAsync(UserDTO user, string password)
        {
            try
            {
                ArgumentException.ThrowIfNullOrEmpty(password);
                ArgumentNullException.ThrowIfNull(user);

                var ret = await _userRepository.AnyAsync(x =>
                      x.Email.ToLower().Equals(user.Email.ToLower()) &&
                      x.Password == password);

                return (ret) ?
                    new GenericResponse<bool>(ret) :
                    new GenericResponse<bool>("password is not valid", false);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while verifying the password", ex);
            }
        }

        public async Task<GenericResponse<IEnumerable<UserDTO>>> GetAllAsync()
        {
            try
            {
                var users = await _userRepository.GetAllAsync();
                return new GenericResponse<IEnumerable<UserDTO>>(users.Select(x => _mapper.Map<UserDTO>(x)));
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while retrieving all users", ex);
            }
        }

        public async Task<GenericResponse<UserDTO>> GetByIdAsync(Guid id)
        {
            try
            {
                var user = await _userRepository.FirstOrDefaultAsync(x => x.Id == id);

                return user is not null ?
                    new GenericResponse<UserDTO>(_mapper.Map<UserDTO>(user)) :
                    new GenericResponse<UserDTO>("User not found", false);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while retrieving the user by ID", ex);
            }
        }

        public async Task<GenericResponse<UserDTO>> AddAsync(UserAddDTO userDto)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(userDto);
                var user = _mapper.Map<User>(userDto);

                var exist = await _userRepository.FirstOrDefaultAsync(x => x.Email.ToLower().Equals(user.Email.ToLower()));
                if (exist != null) return new GenericResponse<UserDTO>("User already exists", false);

                user.Password = userDto.Password;

                var added = await _userRepository.AddAsync(user);
                return added != null ?
                    new GenericResponse<UserDTO>(_mapper.Map<UserDTO>(user)) :
                    new GenericResponse<UserDTO>("An error occurred while adding the user", false);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while adding the user", ex);
            }
        }

        public async Task<GenericResponse<UserDTO>> UpdateAsync(UserDTO userDto)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(userDto);

                var exist = await _userRepository.FirstOrDefaultAsync(x => x.Id == userDto.Id);
                if (exist is null) return new GenericResponse<UserDTO>("User not found", false);

                exist.Name ??= userDto.Name;
                exist.Email ??= userDto.Email;

                var updated = await _userRepository.UpdateAsync(exist);
                return updated != null ?
                    new GenericResponse<UserDTO>(_mapper.Map<UserDTO>(exist)) :
                    new GenericResponse<UserDTO>("An error occurred while updating the user", false);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while updating the user", ex);
            }
        }

        public async Task<GenericResponse<string>> DeleteAsync(Guid id)
        {
            try
            {
                var user = await _userRepository.FirstOrDefaultAsync(x => x.Id == id);
                if (user is null) return new GenericResponse<string>("User not found", false);

                return (await _userRepository.RemoveAsync(user)) ?
                    new GenericResponse<string>("User removed successfully") :
                    new GenericResponse<string>("An error occurred while removing the user", false);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while deleting the user", ex);
            }
        }
    }
}
