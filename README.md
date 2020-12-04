# Geexbox.Aliyun.Oss

[![NuGet RollingFile](https://img.shields.io/nuget/v/Geexbox.Aliyun.Oss.svg)](https://www.nuget.org/packages/Geexbox.Aliyun.Oss/)

## Geexbox.Aliyun.Oss

阿里云官方sdk的 asp.net core 集成方案

### Getting Started 

**First** Install the [Geexbox.Aliyun.Oss](https://nuget.org/packages/Geexbox.Aliyun.Oss) package from NuGet, either using powershell:

```powershell
Install-Package Geexbox.Aliyun.Oss
```

or using the .NET CLI:

```powershell
dotnet add package Geexbox.Aliyun.Oss
```

#### Usage

in `Startup.cs`
```csharp
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // ...
            services.AddAliyunOss(options =>
            {
                options.AccessKeyId = "your_AccessKeyId";
                options.AccessKeySecret = "your_AccessKeySecret";
                options.BulkName = "your_BulkName";
                options.ImageUrlPrefix = "your_ImageUrlPrefix";
            });
            // ...
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // ...
            app.UseAliyunOss();
            // ...
        }
```

the default upload link would be `your_site_baseUrl + "/fileupload"`(eg.: `http://localhost:8000/fileupload`)
