using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

using HtmlAgilityPack;

using JustProxy.Extensions;

using Nancy;

using HttpStatusCode = Nancy.HttpStatusCode;

namespace JustProxy
{
    public class ProxyModule : NancyModule
    {
        private const int Key = 25;
        private readonly Dictionary<string, Func<Uri, string, string, object>> actions;
        private readonly Func<Uri, string, string, object> defaultAction;

        public ProxyModule()
        {
            actions = new Dictionary<string, Func<Uri, string, string, object>>
                      {
                          {"text/html", ProcessHtml}
                      };
            defaultAction = ProcessGeneric;

            Get["/route"] = x => { return ProcessUrl(Request.Query.x.ToString()); };
            Get["/"] = x => { return ""; };
            Get["*"] = x => { return ""; };
        }

        private static object ProcessGeneric(Uri uri, string contentType, string fileName)
        {
            var response = new Response
                           {
                               ContentType = contentType,
                               StatusCode = HttpStatusCode.OK,
                               Contents = s =>
                                          {
                                              using (FileStream file = File.OpenRead(fileName))
                                              {
                                                  file.CopyTo(s);
                                              }

                                              File.Delete(fileName);
                                          }
                           };

            return response;
        }

        private static object ProcessHtml(Uri uri, string contentType, string fileName)
        {
            string html;
            string returnString = "";

            using (var reader = new StreamReader(fileName))
            {
                html = reader.ReadToEnd();
            }

            File.Delete(fileName);

            if (html.IsNotNullOrEmpty())
            {
                var doc = new HtmlDocument();

                doc.LoadHtml(html);

                IEnumerable<HtmlAttribute> hrefes = (from n in doc.DocumentNode.Descendants()
                                                     where n.HasAttributes && n.Attributes["href"] != null
                                                     select n.Attributes["href"])
                                               .Union((from n in doc.DocumentNode.Descendants()
                                                       where n.HasAttributes && n.Attributes["src"] != null
                                                       select n.Attributes["src"])).ToArray()
                    ;

                foreach (HtmlAttribute att in hrefes.Where(att => !att.Value.StartsWith("mailto"))) {
                    att.Value = string.Format("/route?x={0}", Encode(new Uri(uri, att.Value).AbsoluteUri));
                }

                using (var writer = new StringWriter())
                {
                    doc.Save(writer);

                    returnString = writer.ToString();
                }
            }

            return new Response
                   {
                       StatusCode = HttpStatusCode.OK,
                       ContentType = "text/html",
                       Contents = s =>
                                  {
                                      using (var writer1 = new StreamWriter(s))
                                      {
                                          writer1.Write(returnString);
                                      }
                                  }
                   };
        }

        private object ProcessUrl(string url)
        {
            string contentType;
            string decodedUrl = Decode(url);

            if (!Uri.IsWellFormedUriString(decodedUrl, UriKind.Absolute))
                return "";

            var uri = new Uri(decodedUrl);
            WebRequest request = WebRequest.Create(uri);

            try
            {
                string fileName = Path.GetTempFileName();
                using (WebResponse response = request.GetResponse())
                {
                    contentType = response.ContentType;

                    using (Stream stream = response.GetResponseStream())
                    {
                        using (FileStream file = File.OpenWrite(fileName))
                        {
                            stream.CopyTo(file);
                        }
                    }
                }

                var key = (contentType.IndexOf(';') > 0) ? contentType.Substring(0, contentType.IndexOf(';')) : contentType;
                return actions.GetOrDefault(key, defaultAction)(uri, contentType, fileName);
            }
            catch (WebException webException)
            {
                if (webException.Status == WebExceptionStatus.ProtocolError)
                {
                    var response = ((HttpWebResponse) webException.Response);
                    return new Response
                           {
                               StatusCode = response.StatusCode.ToNancyStatusCode(),
                               ContentType = response.ContentType,
                               Contents = s =>
                                          {
                                              using (var writer = new StreamWriter(s))
                                              {
                                                  writer.Write(webException.Message);
                                              }
                                          }
                           };
                }

                return webException.Message;
            }
            catch (Exception)
            {
                return "invalid";
            }
        }

        private static string Encode(string value)
        {
            return HttpUtility.UrlEncode(Convert.ToBase64String((from c in value.ToCharArray()
                                                                 select Convert.ToByte((Convert.ToInt32(c) ^ Key))).ToArray()));
        }

        private static string Decode(string value)
        {
            return new string((from b in Convert.FromBase64String(HttpUtility.UrlDecode(value))
                               select (char) (Convert.ToInt32(b) ^ Key)).ToArray());
        }
    }
}