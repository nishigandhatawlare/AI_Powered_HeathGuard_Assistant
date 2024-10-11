using Health_Guard_Assistant.Web.Models;

namespace Health_Guard_Assistant.Web.Services.IServices
{
    public interface IBaseService
    {
        Task<ResponseDto?> SendAsync(RequestDto requestDto, bool withBearer = true);

    }
}