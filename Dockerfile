FROM microsoft/dotnet:latest
COPY src/BasketService/bin/release/netcoreapp1.0/publish/ /root/
EXPOSE 5000/tcp
ENTRYPOINT dotnet /root/BasketService.dll