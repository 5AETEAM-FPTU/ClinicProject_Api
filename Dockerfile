# ============================================
# Stage 1: Build
# ============================================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution and all project files first (for layer caching)
COPY src/src.sln ./
COPY src/Core/Clinic.Domain/Clinic.Domain.csproj Core/Clinic.Domain/
COPY src/Core/Clinic.Application/Clinic.Application.csproj Core/Clinic.Application/
COPY src/External/Common/Clinic.Configuration/Clinic.Configuration.csproj External/Common/Clinic.Configuration/
COPY src/External/Presentation/Clinic.WebAPI/Clinic.WebAPI.csproj External/Presentation/Clinic.WebAPI/
COPY src/External/Infrastructure/Persistence/Relational/Clinic.MySQL/Clinic.MySQL.csproj External/Infrastructure/Persistence/Relational/Clinic.MySQL/
COPY src/External/Infrastructure/Persistence/Cache/Clinic.Redis/Clinic.Redis.csproj External/Infrastructure/Persistence/Cache/Clinic.Redis/
COPY src/External/Infrastructure/IdentityService/Clinic.JsonWebToken/Clinic.JsonWebToken.csproj External/Infrastructure/IdentityService/Clinic.JsonWebToken/
COPY src/External/Infrastructure/IdentityService/Clinic.OTP/Clinic.OTP.csproj External/Infrastructure/IdentityService/Clinic.OTP/
COPY src/External/Infrastructure/Notification/Mail/Clinic.MailKit/Clinic.MailKit.csproj External/Infrastructure/Notification/Mail/Clinic.MailKit/
COPY src/External/Infrastructure/Notification/SMS/Clinic.TwilioSMS/Clinic.TwilioSMS.csproj External/Infrastructure/Notification/SMS/Clinic.TwilioSMS/
COPY src/External/Infrastructure/FileObjectStorage/Image/Clinic.Firebase/Clinic.Firebase.csproj External/Infrastructure/FileObjectStorage/Image/Clinic.Firebase/
COPY src/External/Infrastructure/PaymentService/Clinic.VNPAY/Clinic.VNPAY.csproj External/Infrastructure/PaymentService/Clinic.VNPAY/
COPY src/External/Infrastructure/ChatMessage/Clinic.SignalR/Clinic.SignalR.csproj External/Infrastructure/ChatMessage/Clinic.SignalR/
COPY src/External/Infrastructure/Communication/Clinic.Stringee/Clinic.Stringee.csproj External/Infrastructure/Communication/Clinic.Stringee/
COPY src/External/Infrastructure/BackgroundJob/Clinic.AppBackgroundJob/Clinic.AppBackgroundJob.csproj External/Infrastructure/BackgroundJob/Clinic.AppBackgroundJob/

# Restore
RUN dotnet restore

# Copy everything and publish
COPY src/ ./
RUN dotnet publish External/Presentation/Clinic.WebAPI/Clinic.WebAPI.csproj \
    -c Release \
    -o /app/publish \
    --no-restore

# ============================================
# Stage 2: Runtime
# ============================================
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Install cultures for globalization
RUN apt-get update && apt-get install -y --no-install-recommends icu-devtools && rm -rf /var/lib/apt/lists/*

COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:5000
ENV ASPNETCORE_ENVIRONMENT=Production
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

EXPOSE 5000

ENTRYPOINT ["dotnet", "Clinic.WebAPI.dll"]
