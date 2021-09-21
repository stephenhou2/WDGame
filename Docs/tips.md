某些项目会默认在生成路径中包括框架和运行时。 若要更改这一点，请在解决方案资源管理器中右键单击项目节点，选择“编辑项目文件”并添加以下内容：

<PropertyGroup>
  <AppendTargetFrameworkToOutputPath>false</AppendTargetFrameworkToOutputPath>
  <AppendRuntimeIdentifierToOutputPath>false</AppendRuntimeIdentifierToOutputPath>
</PropertyGroup>

引用cs文件时，需要在<ItemGroup></temGroup> 标签下：

增加<Compile Include = "*\*.cs"> </Compile> 标签