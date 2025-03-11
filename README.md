# ApiProject

ApiProject - is example of RESTful API with using JWT and OIDC authentication.<br>
For theme of porject will be chosed shop of "Phone cases" (but emphasis was more on auth and not all backend functionality was used on frontend).


## Installation
1. Clone the repository
```powershell
git clone https://github.com/DarkElfes/ApiProject
```

2. Install the dependencies:

```powershell
winget install Microsoft.DotNet.SDK.9
```

## Pre-Launch `For Google OpenIdConnect` (Not necessary, main functionality will work)
For using Google Auth you need to create OAuth 2.0 Client ID in [Google Cloud Console](https://console.cloud.google.com/).<br>
Then you need to add your `ClientId` and `ClientSecret` to the appsettings.json file.

```json
"OIDC": {
    "Google": {
      "ClientId": "YOUR_CLIENT",
      "ClientSecret": "YOUR_SECRET",
      "RedirectUri": "https://localhost:7097/auth/google/oidc-callback"
    }
  }
```


## Launch

For using powershell scripts you need to allow scripts execution:

```powershell
Set-ExecutionPolicy -ExecutionPolicy Unrestricted -Scope CurrentUser
```

Then you can start the projects with the following command:

1. For run Frontent and Backend projects:
```powershell
.\backend-frontend.ps1
```

2. For run only backend project:
```powershell
.\backend-only.ps1
```
In web project console you find the link to the frontend or click here: [https://localhost:7097](https://localhost:7097)<br>
But for backend you need add `/scalar/v1` path to link or click here: [https://localhost:7236/scalar/v1](https://localhost:7236/scalar/v1) for using scalar docs.

## Features
- Clean architecture with using CQRS pattern (MediatR library)
>(But i find the best for me - [Vertical Slice Architecture](https://www.milanjovanovic.tech/blog/vertical-slice-architecture))
- JWT and OpenIDConnect (Only Google) authentication
- Scalar documentation with OpenApi
- Using Entity Framework Core with SQLite
- Fontend with Blazor WebAssembly (MudBlazor UI)

## Postman Documentation
Link to [Postman Documentation](https://documenter.getpostman.com/view/29519259/2sAYdiopYQ)

## Instrucation
[Video Instrucation](https://drive.google.com/file/d/1_qZg2wJAdhvggdHSsO8SEpuSWum2OQOl/view?usp=drive_link)
