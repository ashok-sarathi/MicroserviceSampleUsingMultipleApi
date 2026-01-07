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
        public async Task<IActionResult> GetData([FromServices] DummyJsonClient httpClient)
        {
            try
            {
                using var dummyResponse = await httpClient.SendAsync(HttpMethod.Get, "products");
                dummyResponse.EnsureSuccessStatusCode();

                var resultDummy =
                    await dummyResponse.Content.ReadFromJsonAsync<ProductsDto>()
                    ?? new ProductsDto([]);

                var urls = resultDummy.Products
                    .Select(p => $"products/{p.Id}")
                    .ToList();

                urls.Add("products/999999"); // Non-existing product to simulate failure

                var tasks = urls.Select(async url =>
                {
                    using var response = await httpClient.SendAsync(HttpMethod.Get, url);

                    return new
                    {
                        response.StatusCode,
                        response.RequestMessage?.RequestUri,
                        IsSuccess = response.IsSuccessStatusCode
                    };
                });

                var results = await Task.WhenAll(tasks);

                if (results.Any(r => !r.IsSuccess))
                {
                    return StatusCode(StatusCodes.Status207MultiStatus, results);
                }

                return Ok(results);
            }
            catch (Exception ex)
            {
                throw new FailedDependencyException(
                    $"Exception from dummyjson.com/products flow, {ex.Message}"
                );
            }
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetUsers([FromServices] UsersHttpClient usersHttpClient)
        {
            try
            {
                var response = await usersHttpClient.SendAsync(HttpMethod.Get, "api/users");
                if (!response.IsSuccessStatusCode)
                {
                    throw new FailedDependencyException(await response.Content.ReadAsStringAsync());
                }
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
                if (!response.IsSuccessStatusCode)
                {
                    throw new FailedDependencyException(await response.Content.ReadAsStringAsync());
                }
                var notifications = await response.Content.ReadFromJsonAsync<List<NotificationDto>>();
                return Ok(notifications);
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
