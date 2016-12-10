using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace DigIn.API.Providers
{
    public class ResponseHandler : DelegatingHandler
    {
        //This class intercepts the responses and allow manipulation of responses. Register this class in the WebApiConfig to use it.
        async protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            HttpResponseMessage response = await base.SendAsync(request, cancellationToken);
            response.Headers.Add("X-Custom-Header", "This is my custom header.");
            return response;
        }
    }
}