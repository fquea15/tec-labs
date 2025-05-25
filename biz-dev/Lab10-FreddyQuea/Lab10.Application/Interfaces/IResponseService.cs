using Lab10.Application.DTOs.Response;

namespace Lab10.Application.Interfaces;

public interface IResponseService
{
    Task<GetResponseDto> CreateResponseAsync(CreateResponseDto createResponseDto, Guid userId);
}