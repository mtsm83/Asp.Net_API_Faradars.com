using Faradars.Application.DTOs.Auth;
using Faradars.Application.DTOs.Users.Information.UserService;
using Faradars.Application.Interfaces.General;
using Faradars.Application.Interfaces.Services.Auth;
using Faradars.Application.Interfaces.Services.Validation;
using Faradars.Application.Mappers.Users.Information;
using Faradars.Application.ValidationRules.Auth;
using Faradars.Domain.Entities.Users.Information;
using Faradars.Domain.Entities.Users.Role;
using Faradars.Shared.Result;
using Microsoft.EntityFrameworkCore;

namespace Faradars.Application.Services.Auth;

public class AuthService(
    IRepository<User> userRepository,
    IRepository<VerificationCode> codeRepository,
    IRepository<UserRole> userRoleRepository,
    IRepository<Role> roleRepository,
    IUserContextService userContextService,
    IFluentValidatorService validator,
    IJwtTokenManager jwtTokenManager) : IAuthService, IScopedDependency
{

    public async Task<Result<UserDto>> RegisterByPhoneAsync(RegisterByPhoneDto dto, CancellationToken ct)
    {
        var validationResult = await validator.ValidateAsync(dto, ct);
        if (validationResult.IsFailure)
            return Result.Failure<UserDto>(validationResult.Error);

        var existingUser = await userRepository.TableNoTracking
            .FirstOrDefaultAsync(x => x.Phone == dto.Phone, ct);
        if (existingUser != null)
            return Result.Failure<UserDto>(Error.UserAlreadyExists);

        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);

        var user = new User
        {
            FirstName = dto.FirstName,
            Phone = dto.Phone,
            Password = hashedPassword,
            IsPhoneVerified = false,
            IsEmailVerified = false,
        };
        await userRepository.AddAsync(user, ct);

        var studentRole = await roleRepository.TableNoTracking
            .FirstOrDefaultAsync(r => r.Name == "Student", ct);

        if (studentRole == null)
            return Result.Failure<UserDto>(Error.NotFound);

        var userRole = new UserRole
        {
            UserId = user.Id,
            RoleId = studentRole.Id
        };
        await userRoleRepository.AddAsync(userRole, ct);

        var accessToken = jwtTokenManager.GenerateAccessToken(user, studentRole);
        var dtoResult = user.MapToUserDto();
        dtoResult.AccessTokenDto = accessToken;
        return Result.Success(dtoResult);
    }

    public async Task<Result<UserDto>> LoginByPhoneAsync(LoginByPhoneDto dto, CancellationToken ct)
    {
        var validationResult = await validator.ValidateAsync(dto, ct);
        if (validationResult.IsFailure)
            return Result.Failure<UserDto>(validationResult.Error);
        
        var user = await userRepository.TableNoTracking.FirstOrDefaultAsync(u => 
            u.Phone == dto.Phone, ct);
        if (user == null)
            return Result.Failure<UserDto>(Error.UserNotFound);
        
        bool isPasswordValid = BCrypt.Net.BCrypt.Verify(dto.Password, user.Password);
        if (!isPasswordValid)
            return Result.Failure<UserDto>(Error.WrongPassword);
        
        var accessToken = jwtTokenManager.GenerateAccessToken(user, new Role());
        var userDto = user.MapToUserDto();
        userDto.AccessTokenDto = accessToken;
        return Result.Success(userDto);
    }

    public async Task<Result<UserDto>> RegisterByEmailAsync(RegisterByEmailDto dto, CancellationToken ct)
    {
        var validationResult = await validator.ValidateAsync(dto, ct);
        if (validationResult.IsFailure)
            return Result.Failure<UserDto>(validationResult.Error);

        var existingUser = await userRepository.TableNoTracking
            .FirstOrDefaultAsync(x => x.Email == dto.Email, ct);
        if (existingUser != null)
            return Result.Failure<UserDto>(Error.UserAlreadyExists);

        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);

        var user = new User
        {
            FirstName = dto.FirstName,
            Email = dto.Email,
            Password = hashedPassword,
            IsPhoneVerified = false,
            IsEmailVerified = false,
        };
        await userRepository.AddAsync(user, ct);

        var studentRole = await roleRepository.TableNoTracking
            .FirstOrDefaultAsync(r => r.Name == "Student", ct);

        if (studentRole == null)
            return Result.Failure<UserDto>(Error.NotFound);

        var userRole = new UserRole
        {
            UserId = user.Id,
            RoleId = studentRole.Id
        };
        await userRoleRepository.AddAsync(userRole, ct);

        var accessToken = jwtTokenManager.GenerateAccessToken(user, studentRole);
        var dtoResult = user.MapToUserDto();
        dtoResult.AccessTokenDto = accessToken;
        return Result.Success(dtoResult);
    }

    public async Task<Result<UserDto>> LoginByEmailAsync(LoginByEmailDto dto, CancellationToken ct)
    {
        var validationResult = await validator.ValidateAsync(dto, ct);
        if (validationResult.IsFailure)
            return Result.Failure<UserDto>(validationResult.Error);
        
        var user = await userRepository.TableNoTracking.FirstOrDefaultAsync(u => 
            u.Email == dto.Email, ct);
        if (user == null)
            return Result.Failure<UserDto>(Error.UserNotFound);
        
        bool isPasswordValid = BCrypt.Net.BCrypt.Verify(dto.Password, user.Password);
        if (!isPasswordValid)
            return Result.Failure<UserDto>(Error.WrongPassword);
        
        var accessToken = jwtTokenManager.GenerateAccessToken(user, new Role());
        var userDto = user.MapToUserDto();
        userDto.AccessTokenDto = accessToken;
        return Result.Success(userDto);
    }

    public async Task<Result<Unit>> SendEmailVerificationCodeAsync(string email, CancellationToken ct)
    {
        // var validationResult = await validator.ValidateAsync(email, ct);
        // if (validationResult.IsFailure)
        //     return Result.Failure<Unit>(validationResult.Error);
        
        var user = await userRepository.TableNoTracking
            .FirstOrDefaultAsync(x => x.Email == email, ct);
        if (user == null)
            return Result.Failure<Unit>(Error.UserNotFound);

        var generatedCode = "123456";
        var newCode = new VerificationCode
        {
            Code = generatedCode,
            CreatedBy = userContextService.CurrentUser.UserId,
        };
        await codeRepository.AddAsync(newCode, ct);

        // todo: send to email + auto code generation
        return Result.Success(Unit.Value);
    }

    public async Task<Result<UserDto>> VerifyEmailAsync(VerifyEmailDto dto, CancellationToken ct)
    {
        var validationResult = await validator.ValidateAsync(dto, ct);
        if (validationResult.IsFailure)
            return Result.Failure<UserDto>(validationResult.Error);
        
        var user = await userRepository.TableNoTracking
            .FirstOrDefaultAsync(x => x.Email == dto.Email, ct);
        if (user == null)
            return Result.Failure<UserDto>(Error.UserNotFound);

        if (user.IsEmailVerified)
            return Result.Failure<UserDto>(Error.EmailAlreadyVerified);

        var code = await codeRepository.TableNoTracking
            .FirstOrDefaultAsync(x => x.Code == dto.Code, ct);
        if (code == null)
            return Result.Failure<UserDto>(Error.CodeNotValid);

        user.IsEmailVerified = true;
        code.UsageTime = DateTime.UtcNow;
        code.UsedBy = userContextService.CurrentUser.UserId;
        code.VerifiedResourceBy = dto.Email;
        await codeRepository.UpdateAsync(code, ct);
        await userRepository.UpdateAsync(user, ct);

        var dtoResult = user.MapToUserDto();
        return Result.Success(dtoResult);
    }

    public async Task<Result<Unit>> SendPhoneVerificationCodeAsync(string phone, CancellationToken ct)
    {
        // var validationResult = await validator.ValidateAsync(phone, ct);
        // if (validationResult.IsFailure)
        //     return Result.Failure<Unit>(validationResult.Error);
        
        var user = await userRepository.TableNoTracking
            .FirstOrDefaultAsync(x => x.Phone == phone, ct);
        if (user == null)
            return Result.Failure<Unit>(Error.UserNotFound);

        var generatedCode = "123456";
        var newCode = new VerificationCode
        {
            Code = generatedCode,
            CreatedBy = userContextService.CurrentUser.UserId,
        };
        await codeRepository.AddAsync(newCode, ct);

        // todo: send SMS + auto code generation
        return Result.Success(Unit.Value);
    }

    public async Task<Result<UserDto>> VerifyPhoneAsync(VerifyPhoneDto dto, CancellationToken ct)
    {
        var validationResult = await validator.ValidateAsync(dto, ct);
        if (validationResult.IsFailure)
            return Result.Failure<UserDto>(validationResult.Error);
        
        var user = await userRepository.TableNoTracking
            .FirstOrDefaultAsync(x => x.Phone == dto.Phone, ct);
        if (user == null)
            return Result.Failure<UserDto>(Error.UserNotFound);

        if (user.IsEmailVerified)
            return Result.Failure<UserDto>(Error.PhoneAlreadyVerified);

        var code = await codeRepository.TableNoTracking
            .FirstOrDefaultAsync(x => x.Code == dto.Code, ct);
        if (code == null)
            return Result.Failure<UserDto>(Error.CodeNotValid);

        user.IsPhoneVerified = true;
        code.UsageTime = DateTime.UtcNow;
        code.UsedBy = userContextService.CurrentUser.UserId;
        code.VerifiedResourceBy = dto.Phone;
        await codeRepository.UpdateAsync(code, ct);
        await userRepository.UpdateAsync(user, ct);

        var dtoResult = user.MapToUserDto();
        return Result.Success(dtoResult);
    }
}