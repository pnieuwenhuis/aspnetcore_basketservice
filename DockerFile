FROM microsoft/dotnet:latest
COPY src/BasketService/bin/Release/netcoreapp1.0/publish/ /root/
EXPOSE 5000/tcp
ENTRYPOINT dotnet /root/BasketService.dll