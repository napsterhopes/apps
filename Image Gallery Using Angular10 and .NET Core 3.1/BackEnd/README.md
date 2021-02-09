**Download BackendCore Project (ASP.NET Core) project from the below URL and run in local,angular code is integrated already in the project:**

https://github.com/napsterhopes/apps/tree/main/Image%20Gallery%20Using%20Angular10%20and%20.NET%20Core%203.1/BackEnd
Angular polyfills are already built in this and can be built separately from the Front end project , in case of changes.

Use "ng build" command to generate build files of your angular app; and copy all those files from "dist" folder of angular app into your asp.net core project (into wwwroot folder).

Instructions for Deployment to IIS (same for IIS on an Azure VM) 

✅Install dotnet-hosting module and URL Rewrite Module
✅Right-click on Sites-node in the left section and add a new website.For physical path value,select the same folder where copied the published output earlier.
✅Configure Applications Pool : Set .NET CLR Version to "No managed Code"
✅Give IIS access permission to folder : Add IIS_IUSRS as admin for the folder.

**Screenshots are uploaded displaying various functionalities:**


