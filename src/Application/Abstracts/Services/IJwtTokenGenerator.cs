namespace Application.Abstracts.Services;

    using Domain.Entities;


    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user);
    }


