# Bachelor's Thesis Sample App
[![Build Validation](https://github.com/ef4203/ba-sampleapp/actions/workflows/dotnet.yml/badge.svg)](https://github.com/ef4203/ba-sampleapp/actions/workflows/dotnet.yml)
![GitHub](https://img.shields.io/github/license/ef4203/ba-sampleapp)
![GitHub pull requests](https://img.shields.io/github/issues-pr/ef4203/ba-sampleapp)

This repository contains a sample container application for my bachelor's thesis, that will be deployed on an industrial Internet of Things (IoT) device.

## Getting started
```
docker-compose up --build -d
```

## Building from source
Download and install the [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0). You can build and run the applications with:

```
dotnet run --project src/EFK.SampleApp.AppHost/EFK.SampleApp.AppHost.csproj
```

## Contributing
All contributions are welcome, if you have ideas, improvements or suggestions feel free to open an issue or a pull request on GitHub.

## License
This project is licensed under the MIT license, see: [LICENSE](LICENSE). External dependencies may be subject to different licenses, see [Software Bill of Material](docs/SBOM.md).
