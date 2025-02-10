import { TestBed } from '@angular/core/testing';
import { CanActivateFn } from '@angular/router';

import { accountHolderGuard } from './account-holder.guard';

describe('accountHolderGuard', () => {
  const executeGuard: CanActivateFn = (...guardParameters) => 
      TestBed.runInInjectionContext(() => accountHolderGuard(...guardParameters));

  beforeEach(() => {
    TestBed.configureTestingModule({});
  });

  it('should be created', () => {
    expect(executeGuard).toBeTruthy();
  });
});
