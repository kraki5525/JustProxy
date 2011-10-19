using System.Collections.Generic;

using Nancy;

namespace JustProxy.Extensions
{
    public static class HttpStatusCodeExtensions
    {
        private static readonly Dictionary<System.Net.HttpStatusCode, HttpStatusCode> Lookup = new Dictionary<System.Net.HttpStatusCode, HttpStatusCode>
                                                                                               {
                                                                                                   {System.Net.HttpStatusCode.Accepted, HttpStatusCode.Accepted},
                                                                                                   {System.Net.HttpStatusCode.BadGateway, HttpStatusCode.BadGateway},
                                                                                                   {System.Net.HttpStatusCode.BadRequest, HttpStatusCode.BadRequest},
                                                                                                   {System.Net.HttpStatusCode.Conflict, HttpStatusCode.Conflict},
                                                                                                   {System.Net.HttpStatusCode.Continue, HttpStatusCode.Continue},
                                                                                                   {System.Net.HttpStatusCode.Created, HttpStatusCode.Created},
                                                                                                   {System.Net.HttpStatusCode.ExpectationFailed, HttpStatusCode.ExpectationFailed},
                                                                                                   {System.Net.HttpStatusCode.Forbidden, HttpStatusCode.Forbidden},
                                                                                                   {System.Net.HttpStatusCode.GatewayTimeout, HttpStatusCode.GatewayTimeout},
                                                                                                   {System.Net.HttpStatusCode.Gone, HttpStatusCode.Gone},
                                                                                                   {System.Net.HttpStatusCode.HttpVersionNotSupported, HttpStatusCode.HttpVersionNotSupported},
                                                                                                   {System.Net.HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError},
                                                                                                   {System.Net.HttpStatusCode.LengthRequired, HttpStatusCode.LengthRequired},
                                                                                                   {System.Net.HttpStatusCode.MethodNotAllowed, HttpStatusCode.MethodNotAllowed},
                                                                                                   {System.Net.HttpStatusCode.Moved, HttpStatusCode.Moved},
                                                                                                   {System.Net.HttpStatusCode.MultipleChoices, HttpStatusCode.MultipleChoices},
                                                                                                   {System.Net.HttpStatusCode.NoContent, HttpStatusCode.NoContent},
                                                                                                   {System.Net.HttpStatusCode.NonAuthoritativeInformation, HttpStatusCode.NonAuthoritativeInformation},
                                                                                                   {System.Net.HttpStatusCode.NotAcceptable, HttpStatusCode.NotAcceptable},
                                                                                                   {System.Net.HttpStatusCode.NotFound, HttpStatusCode.NotFound},
                                                                                                   {System.Net.HttpStatusCode.NotImplemented, HttpStatusCode.NotImplemented},
                                                                                                   {System.Net.HttpStatusCode.NotModified, HttpStatusCode.NotModified},
                                                                                                   {System.Net.HttpStatusCode.OK, HttpStatusCode.OK},
                                                                                                   {System.Net.HttpStatusCode.PartialContent, HttpStatusCode.PartialContent},
                                                                                                   {System.Net.HttpStatusCode.PaymentRequired, HttpStatusCode.PaymentRequired},
                                                                                                   {System.Net.HttpStatusCode.PreconditionFailed, HttpStatusCode.PreconditionFailed},
                                                                                                   {System.Net.HttpStatusCode.ProxyAuthenticationRequired, HttpStatusCode.ProxyAuthenticationRequired},
                                                                                                   {System.Net.HttpStatusCode.Redirect, HttpStatusCode.Redirect},
                                                                                                   {System.Net.HttpStatusCode.RedirectMethod, HttpStatusCode.RedirectMethod},
                                                                                                   {System.Net.HttpStatusCode.RequestedRangeNotSatisfiable, HttpStatusCode.RequestedRangeNotSatisfiable},
                                                                                                   {System.Net.HttpStatusCode.RequestEntityTooLarge, HttpStatusCode.RequestEntityTooLarge},
                                                                                                   {System.Net.HttpStatusCode.RequestTimeout, HttpStatusCode.RequestTimeout},
                                                                                                   {System.Net.HttpStatusCode.RequestUriTooLong, HttpStatusCode.RequestUriTooLong},
                                                                                                   {System.Net.HttpStatusCode.ResetContent, HttpStatusCode.ResetContent},
                                                                                                   {System.Net.HttpStatusCode.ServiceUnavailable, HttpStatusCode.ServiceUnavailable},
                                                                                                   {System.Net.HttpStatusCode.SwitchingProtocols, HttpStatusCode.SwitchingProtocols},
                                                                                                   {System.Net.HttpStatusCode.TemporaryRedirect, HttpStatusCode.TemporaryRedirect},
                                                                                                   {System.Net.HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized},
                                                                                                   {System.Net.HttpStatusCode.UnsupportedMediaType, HttpStatusCode.UnsupportedMediaType},
                                                                                                   {System.Net.HttpStatusCode.Unused, HttpStatusCode.Unused},
                                                                                                   {System.Net.HttpStatusCode.UseProxy, HttpStatusCode.UseProxy}
                                                                                               };
        public static HttpStatusCode ToNancyStatusCode(this System.Net.HttpStatusCode statusCode)
        {
            return Lookup[statusCode];
        }
    }
}