# GloboDietWeb

## about

- proof-of-concept for nutrition interviews
- possible replacement for currently used desktop applications
- persistent storage for demo data
- role management for logins

## impressions

### website

<img alt="website" src="docs/img/2023-02-04-15-35-13.png" width="700" border=1/>

### desktop process

<img alt="skizzen" src="docs/img/2023-02-03-22-16-25.png" width="700" border=1 />

## built with

### components

- framework used: ![C#](https://img.shields.io/badge/c%23-%23239120.svg?style=flat&logo=c-sharp&logoColor=white) ![.Net](https://img.shields.io/badge/.NET-5C2D91?style=flat&logo=.net&logoColor=white)
- data storage in [![Azure](https://badgen.net/badge/icon/azure?icon=azure&label)](https://azure.microsoft.com)
- [![made-for-VSCode](https://img.shields.io/badge/Made%20for-VSCode-1f425f.svg)](https://code.visualstudio.com/)
- tested on ![Google Chrome](https://img.shields.io/badge/Google%20Chrome-4285F4?style=flat&logo=GoogleChrome&logoColor=white)

### used packages

```xml
  <ItemGroup>
    <PackageReference Include="DextersLabor" Version="0.9.2" />
    <PackageReference Include="Microsoft.AspNetCore.Identity.EntityFrameworkCore" Version="7.0.2" />
    <PackageReference Include="Microsoft.AspNetCore.Mvc.NewtonsoftJson" Version="7.0.2" />
    <PackageReference Include="Microsoft.EntityFrameworkCore" Version="7.0.2" />
    <PackageReference Include="Microsoft.EntityFrameworkCore.InMemory" Version="7.0.2" />
    <PackageReference Include="Microsoft.EntityFrameworkCore.Proxies" Version="7.0.2" />
    <PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" Version="7.0.2" />
    <PackageReference Include="Microsoft.EntityFrameworkCore.Tools" Version="7.0.2">
      <PrivateAssets>all</PrivateAssets>
      <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
    </PackageReference>
    <PackageReference Include="Microsoft.Extensions.Identity.Stores" Version="7.0.2" />
    <PackageReference Include="Microsoft.VisualStudio.Web.CodeGeneration.Design" Version="7.0.3" />
    <PackageReference Include="NLog" Version="5.1.1" />
  </ItemGroup>
```
