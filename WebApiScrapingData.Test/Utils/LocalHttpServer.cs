using System.Net;
using System.Net.Sockets;
using System.Text;

namespace WebApiScrapingData.Test.Utils;

internal sealed class LocalHttpServer : IDisposable
{
    private readonly TcpListener _listener = new(IPAddress.Loopback, 0);
    private readonly Task _response;

    public LocalHttpServer(string body, int statusCode = 200)
    {
        _listener.Start();
        var port = ((IPEndPoint)_listener.LocalEndpoint).Port;
        Url = $"http://127.0.0.1:{port}/page";
        _response = RespondOnceAsync(body, statusCode);
    }

    public string Url { get; }

    private async Task RespondOnceAsync(string body, int statusCode)
    {
        using var client = await _listener.AcceptTcpClientAsync();
        await using var stream = client.GetStream();
        using var reader = new StreamReader(stream, Encoding.ASCII, leaveOpen: true);
        while (!string.IsNullOrEmpty(await reader.ReadLineAsync())) { }
        var content = Encoding.UTF8.GetBytes(body);
        var headers = Encoding.ASCII.GetBytes($"HTTP/1.1 {statusCode} Test\r\nContent-Type: text/html; charset=utf-8\r\nContent-Length: {content.Length}\r\nConnection: close\r\n\r\n");
        await stream.WriteAsync(headers);
        await stream.WriteAsync(content);
    }

    public void Dispose()
    {
        _listener.Stop();
        if (_response.IsCompletedSuccessfully)
            _response.GetAwaiter().GetResult();
    }
}
