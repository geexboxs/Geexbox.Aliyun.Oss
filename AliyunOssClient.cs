﻿using System;
using System.IO;
using System.Linq;

using Aliyun.OSS;

using Geexbox.Extensions;

using Microsoft.AspNetCore.Http;

namespace Geexbox.Aliyun.Oss
{
    public class AliyunOssClient
    {
        private string _imageBulkName;
        private OssClient _ossClient;
        private AliyunOssOptions _options;

        public AliyunOssClient(AliyunOssOptions options)
        {
            this._imageBulkName = options.BulkName;
            this._ossClient = new OssClient(options.RemoteEndPoint, options.AccessKeyId, options.AccessKeySecret);
            this._options = options;
        }

        public AliyunOssUploadResult UploadBase64(string dataUrl, string fileName)
        {
            var base64WithExt = dataUrl.DataUrlToBase64WithExt();
            var md5 = dataUrl.ComputeMd5();
            //var md5FileName = $"{md5}.{base64WithExt.ext}";
            var fileContent = Convert.FromBase64String(base64WithExt.base64);
            _ = this._ossClient.PutObject(this._imageBulkName, md5, new MemoryStream(fileContent), new ObjectMetadata()
            {
                ContentType = base64WithExt.ext,
                UserMetadata =
                {
                    {"fileName",fileName}
                }
            });
            return new AliyunOssUploadResult(_options.ImageUrlPrefix, md5, fileContent.Length, base64WithExt.ext, fileName);
        }

        public AliyunOssUploadResult UploadPostFile(IFormFile formFile)
        {
            var stream = formFile.OpenReadStream();
            var md5 = stream.ComputeMd5();
            var fileMime = formFile.ContentType;
            //var fileExt = formFile.FileName.Split('.').LastOrDefault() ?? "";
            _ = this._ossClient.PutObject(this._imageBulkName, md5, stream, new ObjectMetadata()
            {
                ContentType = fileMime,
                UserMetadata =
                {
                    {"fileName",formFile.FileName}
                }
            });
            return new AliyunOssUploadResult(_options.ImageUrlPrefix, md5, stream.Length, fileMime, formFile.FileName);
        }

        public OssObject GetFile(string key)
        {
            var file = this._ossClient.GetObject(_options.BulkName, key);
            return file;
        }

        public DeleteObjectResult DeleteFile(string key)
        {
            var file = this._ossClient.DeleteObject(_options.BulkName, key);
            return file;
        }
    }
}