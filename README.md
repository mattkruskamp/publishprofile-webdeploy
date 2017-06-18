# publishprofile-webdeploy

[![Build status](https://ci.appveyor.com/api/projects/status/216x64sy29mkhx5r?svg=true)](https://ci.appveyor.com/project/Cyberkruz/publishprofile-webdeploy)

Quick example of getting a site up and running with .Net Core on Mac. This site converts the publishsettings from Azure Websites and converts it to yml for AppVeyor.

* [Demo site](http://publishprofile-webdeploy.mattkruskamp.com)
* [Article this code comes from](https://www.mattkruskamp.com/blog/2017/build-quick-net-core-api-and-site-on-mac-with-command-line/)
* [Test Publish Settings]()

## Pre-Reqs

In order to run this solution you need to have bower and .Net Core installed on your machine.

## Getting Started

Clone the repository.

```
git clone https://github.com/Cyberkruz/publishprofile-webdeploy.git
cd publishprofile-webdeploy
```

Restore NuGet packages

```
cd src/
dotnet restore
```

Restore the frontend packages

```
cd Cyberkruz.PublishProfile.Web
bower install
cd ..
```

Run the tests

```
dotnet test Cyberkruz.PublishProfile.Web.Tests/Cyberkruz.PublishProfile.Web.Tests.csproj
```

Build the project

```
dotnet build
```

Run the project

```
cd Cyberkruz.PublishProfile.Web
dotnet run
```
