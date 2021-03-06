﻿using System;
using System.Linq;

using Aliyun.OSS;

namespace Geexbox.Aliyun.Oss
{
    public class AliyunOssUploadResult
    {
        public AliyunOssUploadResult(string urlPrefix, string key, long fileSize, string mimeType)
        {
            Size = fileSize;
            Url = urlPrefix.TrimEnd('/') + "/" + Key;
            Key = key;
            MimeType = mimeType;
        }

        public long Size { get; }
        public string MimeType { get; }
        public string Url { get; }
        public string Key { get; }
    }
}