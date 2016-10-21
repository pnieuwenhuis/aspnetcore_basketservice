cd src/BasketService
dotnet publish -c release
cd ../..
docker build .