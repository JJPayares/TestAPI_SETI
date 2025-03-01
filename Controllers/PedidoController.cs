using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System;

[ApiController]
[Route("api/pedidos")]
public class PedidoController : ControllerBase
{
    private readonly HttpClient _httpClient;

    public PedidoController(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    [HttpPost("enviar")]
    public async Task<IActionResult> EnviarPedido([FromBody] PedidoRequest request)
    {
        try
        {
            var xmlRequest = ConvertToXml(request);
            var content = new StringContent(xmlRequest, Encoding.UTF8, "application/xml");
            var response = await _httpClient.PostAsync("https://run.mocky.io/v3/19217075-6d4e-4818-98bc-416d1feb7b84", content);

            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, new { error = "Error en la comunicación con el servicio SOAP", xml = xmlRequest });
            }

            var xmlResponse = await response.Content.ReadAsStringAsync();
            var jsonResponse = ConvertToJson(xmlResponse);
            return Ok(jsonResponse);
        }
        catch (TaskCanceledException)
        {
            return StatusCode(504, new { error = "Tiempo de espera agotado al comunicarse con el servicio SOAP" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Error interno del servidor", detalle = ex.Message });
        }
    }

    private string ConvertToXml(PedidoRequest request)
    {
        XNamespace soapenv = "http://schemas.xmlsoap.org/soap/envelope/";
        XNamespace env = "http://WSDLs/EnvioPedidos/EnvioPedidosAcme";

        var xml = new XElement(soapenv + "Envelope",
            new XAttribute(XNamespace.Xmlns + "soapenv", soapenv.NamespaceName),
            new XAttribute(XNamespace.Xmlns + "env", env.NamespaceName),
            new XElement(soapenv + "Body",
                new XElement(env + "EnvioPedidoAcme",
                    new XElement("EnvioPedidoRequest",
                        new XElement("pedido", request.EnviarPedido.NumPedido),
                        new XElement("Cantidad", request.EnviarPedido.CantidadPedido),
                        new XElement("EAN", request.EnviarPedido.CodigoEAN),
                        new XElement("Producto", request.EnviarPedido.NombreProducto),
                        new XElement("Cedula", request.EnviarPedido.NumDocumento),
                        new XElement("Direccion", request.EnviarPedido.Direccion)
                    )
                )
            )
        );
        return xml.ToString();
    }

    private object ConvertToJson(string xmlResponse)
    {
        try
        {
            var xml = XDocument.Parse(xmlResponse);
            XNamespace env = "http://WSDLs/EnvioPedidos/EnvioPedidosAcme";
            var responseElement = xml.Descendants(env + "EnvioPedidoResponse").FirstOrDefault();

            if (responseElement != null)
            {
                return new
                {
                    enviarPedidoRespuesta = new
                    {
                        codigoEnvio = responseElement.Element("Codigo")?.Value,
                        estado = responseElement.Element("Mensaje")?.Value
                    }
                };
            }

            return new { error = "Formato XML de respuesta no válido" };
        }
        catch (Exception)
        {
            return new { error = "Error al procesar la respuesta XML" };
        }
    }
}

public class PedidoRequest
{
    public EnviarPedido EnviarPedido { get; set; }
}

public class EnviarPedido
{
    public string NumPedido { get; set; }
    public string CantidadPedido { get; set; }
    public string CodigoEAN { get; set; }
    public string NombreProducto { get; set; }
    public string NumDocumento { get; set; }
    public string Direccion { get; set; }
}
