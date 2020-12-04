using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Geexbox.Extensions;
using Microsoft.AspNetCore.Http;

namespace Geexbox.Aliyun.Oss
{
    public class AliyunOssFileUploadMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AliyunOssClient _ossClient;

        public AliyunOssFileUploadMiddleware(RequestDelegate next, AliyunOssClient ossClient)
        {
            _next = next;
            _ossClient = ossClient;
        }

        public Task Invoke(HttpContext context)
        {
            if (context.Request?.HasFormContentType == false)
            {
                return _next.Invoke(context);
            }

            if (context.Request.Form?.Files?.Any() == true)
            {
                var resultList = new List<AliyunOssUploadResult>();
                var files = context.Request.Form.Files
                    .ToArray();

                foreach (var file in files)
                {
                    var result = _ossClient.UploadPostFile(file);
                    resultList.Add(result);
                }


                var names = files.Select(f => f.FileName);
                return context.Response.WriteAsync(new { files = resultList }.ToJson());
            }
            return _next.Invoke(context);
        }
    }
}
