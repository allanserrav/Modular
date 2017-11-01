Adicionar no csproj
<ItemGroup>
    <DotNetCliToolReference Include="Microsoft.EntityFrameworkCore.Tools.DotNet">
      <Version>1.0.0-*</Version>
    </DotNetCliToolReference>
</ItemGroup>

dotnet ef migrations add migration1
dotnet ef database update