using er.huawei.replicador.func.Application.Model;
using er.huawei.replicador.func.Application.Model.Dto;
using er.huawei.replicador.func.Data.Repository.Interfaces;
using Newtonsoft.Json;
using System.Text;

namespace er.huawei.replicador.func.Data.Repository.Adapters;

public class HuaweiAdapter : IHuaweiRepository
{
    private readonly HttpClient _httpClient;
    private readonly string huaweiApi;

    public HuaweiAdapter(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient();
        huaweiApi = "https://er-integrator-huawei.azurewebsites.net/api/v1/integrators/huawei/";
    }

    public async Task<ResponseModel<string>> GetRealTimeDeviceInfoAsync(FiveMinutesRequest request)
    {
        var api = huaweiApi;
        var apiUrl = $"{api}realTimeInfo";

        // Enviar solicitud a la API de dispositivo
        var requestBody = JsonConvert.SerializeObject(request); // Usar JsonConvert en lugar de JsonSerializer
        var requestContent = new StringContent(requestBody, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync(apiUrl, requestContent);

        if (!response.IsSuccessStatusCode)
        {
            return new ResponseModel<string> { Success = false, ErrorCode = (int)response.StatusCode, ErrorMessage = response.ReasonPhrase };
        }

        // Procesar la respuesta de la API de dispositivo
        var responseContent = await response.Content.ReadAsStringAsync();
        var responseData = JsonConvert.DeserializeObject<ResponseModel<string>>(responseContent); // Corrección en la deserialización

        return responseData;
    }
}
