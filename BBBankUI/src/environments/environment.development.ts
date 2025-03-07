export const environment = {
    apiBaseUrl: 'http://localhost:5111/api/',
    clientId: '3001c98e-c1c2-4bb6-ad1f-4fddf3039585', // Application (client) ID of this Angular app that was registered as client app in Azure AD App Registrations.
    authority: 'https://login.microsoftonline.com/0c087d99-9bb7-41d4-bd58-80846660b536', // The ID of the Tenant in which this client app was registered in Azure AD.
    redirectUri: 'http://localhost:4200', // Url where Azure AD will come back after Signing In Process completes. 
    postLogoutRedirectUri: 'http://localhost:4200/login', //Url where Azure AD will come back after Signing Out Process completes. 
    defaultScope: 'api://secure-bbbankapi/DefaultScope',
    entraIdUsersUrl: 'https://bbbank-entra-users-list.azurewebsites.net:443/api/GetEntraUsers/triggers/When_a_HTTP_request_is_received/invoke?api-version=2022-05-01&sp=%2Ftriggers%2FWhen_a_HTTP_request_is_received%2Frun&sv=1.0&sig=QT2Zwg074BblLJM0VxhJw_QOkGcnp4JowpgVqEzXM4c',
    gridDefaultPageSize: 10,
    profilePicUploadEndpoint: 'https://bbbankfunctions.azurewebsites.net/api/UploadImageToBlob?code=5oLCbAtZXUVCIrAada3x55Aox0qgAq9nyRhpmlkKSlIiAzFuhF91DA==&fileName=',
    azureStorageAccountName: 'bbbankstorage',
    azureStorageContainerName: 'profile-pics'
};
