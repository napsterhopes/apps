Unzip and run 

Angular polyfills are already built in this and can be built separately from the Front end project , in case of changes.

Instructions for Deployment to IIS (same for IIS on an Azure VM) 

✅Install dotnet-hosting module and URL Rewrite Module
✅Right-click on Sites-node in the left section and add a new website.For physical path value,select the same folder where copied the published output earlier.
✅Configure Applications Pool : Set .NET CLR Version to "No managed Code"
✅Give IIS access permission to folder : Add IIS_IUSRS as admin for the folder.


