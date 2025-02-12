import { ApplicationConfig, importProvidersFrom, PLATFORM_ID, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes'; // Your routes
import { provideClientHydration, withEventReplay } from '@angular/platform-browser';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { provideHttpClient, withInterceptorsFromDi, HTTP_INTERCEPTORS, withFetch } from '@angular/common/http';
import {
  MsalModule,
  MsalGuard,
  MsalInterceptor,
  MSAL_INSTANCE,
  MSAL_GUARD_CONFIG,
  MSAL_INTERCEPTOR_CONFIG,
  MsalService,
  MsalBroadcastService
} from '@azure/msal-angular';
import { InteractionType } from '@azure/msal-browser'; // If needed
import {
  MSALInstanceFactory,
  MSALInterceptorConfigFactory,
  MSALGuardConfigFactory,
  loginRequest // Make sure you import this
} from './auth-config'; // Path to your auth config file
import { isPlatformBrowser } from '@angular/common';

export const appConfig: ApplicationConfig = {
  providers: [
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(routes),
    provideClientHydration(withEventReplay()),
    provideAnimationsAsync(),
    provideHttpClient(withFetch(), withInterceptorsFromDi()),
    importProvidersFrom(MsalModule),
    {
      provide: MSAL_INSTANCE,
      useFactory: MSALInstanceFactory,
      deps: [PLATFORM_ID]
    },
    {
      provide: MSAL_INTERCEPTOR_CONFIG,
      useFactory: MSALInterceptorConfigFactory
    },
    {
      provide: MSAL_GUARD_CONFIG,
      useFactory: MSALGuardConfigFactory
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: MsalInterceptor,
      multi: true
    },
    MsalService,
    MsalGuard,
    MsalBroadcastService
  ]
};