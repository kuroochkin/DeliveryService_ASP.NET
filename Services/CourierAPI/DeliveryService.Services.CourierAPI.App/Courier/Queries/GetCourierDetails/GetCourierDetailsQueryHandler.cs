namespace DeliveryService.Services.CourierAPI.App.Courier.Queries.GetCourierDetails;

using DeliveryService.Services.CourierAPI.App.Common.Errors;
using DeliveryService.Services.CourierAPI.App.Common.Interfaces.Persistence;
using DeliveryService.Services.CourierAPI.App.Courier.Vm;
using ErrorOr;
using MediatR;
using Newtonsoft.Json;

public class GetCourierDetailsQueryHandler
    : IRequestHandler<GetCourierDetailsQuery, ErrorOr<CourierDetailsVm>>
{

    private readonly IUnitOfWork _unitOfWork;
    public GetCourierDetailsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<CourierDetailsVm>> Handle(
        GetCourierDetailsQuery request,
        CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(request.CourierId, out var courierId))
        {
            return Errors.Courier.InvalidId;
        }

        var courier = await _unitOfWork.Couriers.FindById(courierId);
        if (courier is null)
        {
            return Errors.Courier.NotFound;
        }

        using (HttpClient httpClient = new HttpClient())
        {
            try
            {
                var url = $"http://host.docker.internal:5006/api/auth/get-by-id/{request.CourierId}";
                // Отправка GET-запроса
                HttpResponseMessage response = await httpClient.GetAsync(url);

                // Проверка успешности запроса
                response.EnsureSuccessStatusCode();

                // Чтение ответа
                string responseBody = await response.Content.ReadAsStringAsync();
                var user = JsonConvert.DeserializeObject<UserDetailsVm>(responseBody);


                var courierDetails = new CourierDetailsVm(
                    courier.Id.ToString(),
                    user.Email,
                    user.UserName,
                    courier.LastName,
                    courier.FirstName,
                    courier.BirthDay,
                    courier.CountOrder,
                    user.PhoneNumber);

                return courierDetails;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("Ошибка при выполнении HTTP-запроса: " + ex.Message);
                return Errors.Courier.InvalidId;
            }
        }
    }
}
