import { BrowserCacheLocation, Configuration, InteractionType, IPublicClientApplication, LogLevel, PublicClientApplication } from "@azure/msal-browser";
import { environment } from "../environments/environment.development";
import { MsalGuardConfiguration, MsalInterceptorConfiguration } from "@azure/msal-angular";
import { isPlatformBrowser } from "@angular/common";

export const msalConfig: Configuration = {
    auth: {
        clientId: environment.clientId, // Application (client) ID of this Angular app that was registered as client app in Azure AD App Registrations.
        authority: environment.authority, // The ID in this URI is the Id of the Tenant in which this client app was registered in Azure AD.
        redirectUri: environment.redirectUri, // Url where Azure AD will come back after Signing In Process completes. 
        postLogoutRedirectUri: environment.postLogoutRedirectUri, //Url where Azure AD will come back after Signing Out Process completes. 
    },
    cache: {
        cacheLocation: BrowserCacheLocation.LocalStorage, // Configures cache location. "sessionStorage" is more secure, but "localStorage" gives you SSO between tabs.,
        storeAuthStateInCookie: false,
    },
    system: {
        loggerOptions: { // MSAL provides loggerOptions where we can configure logging to receive logs of all steps performed during sign in process. 
            loggerCallback(logLevel: LogLevel, message: string) {
                console.log(message);
            },
            logLevel: LogLevel.Verbose, // Different levels of logs are available . 'Verbose' will have most detailed information. 
            piiLoggingEnabled: false,
        }
    }
};

export const protectedResources = {
    bbbankApi: {
        // Scopes defines what client application (this Angular app) can do in the Api.
        // in our application all resources of api are exposed through one default scope. and App Roles are there to define what a particular user can do. 
        // Which will be handled on API side using [Authorize(Roles="xxx")] attribute.
        // Since all resources in the API can be accessed by one default scope we are getting token against the baseUrl. 
        endpoint: environment.apiBaseUrl,
        scopes: [environment.defaultScope],
        // We could have introduced multiple scopes for different types of resources om api and could have handled them using [RequireScope('xxx')] attribute on api side.
    },
};
export const loginRequest = {
    // This scope is exposed by API App and added as permission by Client app (this app) in the Azure AD Portal. 
    // When client app (this App) will try to login with this scope, it will receive App Roles information as well in the token since App roles are also configured in API App
    scopes: [environment.defaultScope] as any[],
};

export function MSALInstanceFactory(): PublicClientApplication {
      return new PublicClientApplication(msalConfig);
  }


/**
* MSAL Angular will automatically retrieve tokens for resources
* added to protectedResourceMap. For more info, visit:
* https://github.com/AzureAD/microsoft-authentication-library-for-js/blob/dev/lib/msal-angular/docs/v2-docs/initialization.md#get-tokens-for-web-api-calls
*/

// This Interceptor setting will add access token to all http calls that are mentioned in  protectedResourceMap. 
// Since we are mentioning baseUrl, So all the http calls starting with baseUrl will have access_token injected in the header that will have role and scope info in it
export function MSALInterceptorConfigFactory(): MsalInterceptorConfiguration {
    const protectedResourceMap = new Map<string, Array<string>>();
   // protectedResourceMap.set('https://graph.microsoft.com/v1.0/me', ['User.Read', 'User.ReadBasic.All']);
    protectedResourceMap.set(protectedResources.bbbankApi.endpoint, protectedResources.bbbankApi.scopes);

    return {
        interactionType: InteractionType.Redirect,
        protectedResourceMap,
    };
}

/**
* Set your default interaction type for MSALGuard here. If you have any
* additional scopes you want the user to consent upon login, add them here as well.
*/
// Setting up MSAL on any route will automatically check if the user is logged in and will take the user to login flow if its not logged in and then will let route to proceed. 
export function MSALGuardConfigFactory(): MsalGuardConfiguration {
    return {
        interactionType: InteractionType.Redirect,
        authRequest: loginRequest,
    };
}