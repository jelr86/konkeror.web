using konkeror.app.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace konkeror.web.Common
{
    public class ErrorResult : IHttpActionResult
    {
        public IEnumerable<ValidationMessage> _errors;
        public HttpRequestMessage _request;

        public ErrorResult(IEnumerable<ValidationMessage> errors, HttpRequestMessage request)
        {
            _errors = errors;
            _request = request;
        }
        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var response = new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest)
            {
                Content = new ObjectContent<IEnumerable<ValidationMessage>>(_errors, new JsonMediaTypeFormatter()),
                RequestMessage = _request
            };
            return Task.FromResult(response);
        }
    }
}