import { Injectable } from '@angular/core';
import { initialize, LDClient, LDFlagSet } from 'launchdarkly-js-client-sdk';
import { BehaviorSubject, Observable, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class FeatureFlagService {
  private ldClient: LDClient;
  private flags: LDFlagSet;
  private flagChange: Subject<object> = new Subject<object>();
  private ldReady: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
  constructor() {
    // Replace 'YOUR_CLIENT_SIDE_ID' with your actual LaunchDarkly client-side ID.
    this.ldClient = initialize('67d90753ff79c709bcc7b23e', {
      key: 'frontend-user', // Replace 'user-key' with a unique user identifier.
      // Add any other user attributes that your flags use.
    });

    this.ldClient.on('ready', () => {
      this.flags = this.ldClient.allFlags();
      this.ldClient.on('change', (changes) => {
        this.flags = this.ldClient.allFlags();
        this.flagChange.next(changes);
      });
      this.ldReady.next(true);
    });
  }

  getFlag<T>(flagKey: string, defaultValue: T): T {
    if (this.flags) {
      return this.ldClient.variation(flagKey, defaultValue);
    }
    return defaultValue;
  }

  getFlagChanges(): Observable<object> {
    return this.flagChange.asObservable();
  }
  getLDReady(): Observable<boolean> {
    return this.ldReady.asObservable();
  }
}
