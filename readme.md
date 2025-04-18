# Introduction

diversa is a small collection of API endpoints.

# Download

Compiled downloads are not available.

# Compiling

To clone and run this application, you'll need [Git](https://git-scm.com) and [.NET](https://dotnet.microsoft.com/) installed on your computer. From your command line:

```
# Clone this repository
$ git clone https://github.com/btigi/diversa

# Go into the repository
$ cd src

# Build  the app
$ dotnet build
```

# Usage

A swagger page is available at the root of the project.

Endpoints accept basic authentication - the required username and password are specified in the appsettings.json file.

Endpoints return a plain text response by default, though will return JSON if application/json is sent in the accept header.

# Endpoints

- ChineseYear
- GetGuid
- Moonphase
- Morsecode
- Tamriel Date
- Zodiac

# Licencing

diversa is licenced under the MIT license. Full licence details are available in license.md
