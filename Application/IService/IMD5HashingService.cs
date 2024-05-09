using Microsoft.AspNetCore.Http;

namespace Application.IService
{
    public interface IMD5HashingService
    {
        public string getMD5Hash(string rawValue);
    }
}
