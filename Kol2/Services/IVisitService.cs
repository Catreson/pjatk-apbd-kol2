using Kol2.DTOs;

namespace Kol2.Services;

public interface IVisitService
{
    Task<VisitDto> GetVisitAsync(int visitId);
    Task CreateAsync(VisitCreateDto dto);
}