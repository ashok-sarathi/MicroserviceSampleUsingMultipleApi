using MasterPortal.Api.Helpers.Dtos;
using MasterPortal.Api.HttpClients;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace MasterPortal.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MastersController : ControllerBase
    {
        [HttpGet("health")]
        public IActionResult Health()
        {
            return Ok("Master Portal API is healthy.");
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetData([FromServices] HttpClient httpClient)
        {
            var dummyResponse = await httpClient.GetAsync("https://dummyjson.com/products");
            var resultDummy = await dummyResponse.Content.ReadFromJsonAsync<ProductsDto>();

            IList<HttpRequestMessage> urls = [];
            IList<string> urls2 = [];
            foreach (var item in resultDummy.Products)
            {
                urls.Add(new HttpRequestMessage(HttpMethod.Get, $"https://dummyjson.com/products/{item.Id}"));
                urls2.Add(($"https://dummyjson.com/products/{item.Id}"));
            }

            var tasks = urls2.Select(async x => await httpClient.GetAsync(x));

            await Task.WhenAll(tasks);

            return Ok();
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetUsers([FromServices] UsersHttpClient usersHttpClient)
        {
            var response = await usersHttpClient.SendAsync(HttpMethod.Get, "api/users");
            var users = await response.Content.ReadFromJsonAsync<List<UserDto>>();
            return Ok(users);
        }

        [HttpGet("notifications")]
        public async Task<IActionResult> GetNotifications([FromServices] NotificationsHttpClient notificationsHttpClient)
        {
            var response = await notificationsHttpClient.SendAsync(httpMethod: HttpMethod.Get, route: "api/notifications");
            var users = await response.Content.ReadFromJsonAsync<List<NotificationDto>>();
            return Ok(users);
        }
    }
}
