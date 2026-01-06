using MasterPortal.Api.Helpers.Dtos;
using MasterPortal.Api.Helpers.Exceptions;
using MasterPortal.Api.Helpers.HttpClients;
using Microsoft.AspNetCore.Mvc;

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
            try
            {
                var dummyResponse = await httpClient.GetAsync("https://dummyjson.com/products");
                var resultDummy = await dummyResponse.Content.ReadFromJsonAsync<ProductsDto>();

                try
                {
                    IList<string> urls = [];
                    foreach (var item in resultDummy.Products)
                    {
                        urls.Add(($"https://dummyjson.com/products/{item.Id}"));
                    }

                    var tasks = urls.Select(x => httpClient.GetAsync(x));

                    await Task.WhenAll(tasks);

                    return Ok();
                }
                catch (Exception ex)
                {
                    throw new FailedDependencyException(
                            $"Exception from GET dummyjson.com/products/itemId:int, {ex.Message}"
                        );
                }
            }
            catch (FailedDependencyException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new FailedDependencyException(
                        $"Exception from GET dummyjson.com/products, {ex.Message}"
                    );
            }
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetUsers([FromServices] UsersHttpClient usersHttpClient)
        {
            try
            {
                var response = await usersHttpClient.SendAsync(HttpMethod.Get, "api/users");
                var users = await response.Content.ReadFromJsonAsync<List<UserDto>>();
                return Ok(users);
            }
            catch (Exception ex)
            {
                throw new FailedDependencyException(
                        $"Exception from User Service, {ex.Message}"
                    );
            }
        }

        [HttpGet("notifications")]
        public async Task<IActionResult> GetNotifications([FromServices] NotificationsHttpClient notificationsHttpClient)
        {
            try
            {
                var response = await notificationsHttpClient.SendAsync(httpMethod: HttpMethod.Get, route: "api/notifications");
                var users = await response.Content.ReadFromJsonAsync<List<NotificationDto>>();
                return Ok(users);
            }
            catch (Exception ex)
            {
                throw new FailedDependencyException(
                        $"Exception from Notification Service, {ex.Message}"
                    );
            }
        }
    }
}
