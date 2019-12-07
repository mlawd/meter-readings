# Meter Reading App

You will need a `appsettings.json` file in MeterReader.Api with the following:

```json
{
  "ConnectionStrings": {
    "Default": "Server=localhost,1433;Database=meter-reader;User Id=##USERNAME##; Password=##PASSWORD##;"
  }
}
```

Replacing `##USERNAME##` and `##PASSWORD##` with the correct details for your database.

Run the migrations to seed the Accounts.

Start the server via cli/Visual Studio

Start the web front end with:

```bash
cd web
npm i
npm run serve
```