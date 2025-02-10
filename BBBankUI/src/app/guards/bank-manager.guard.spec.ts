import { TestBed } from '@angular/core/testing';
import { CanActivateFn } from '@angular/router';

import { bankManagerGuard } from './bank-manager.guard';

describe('bankManagerGuard', () => {
  const executeGuard: CanActivateFn = (...guardParameters) => 
      TestBed.runInInjectionContext(() => bankManagerGuard(...guardParameters));

  beforeEach(() => {
    TestBed.configureTestingModule({});
  });

  it('should be created', () => {
    expect(executeGuard).toBeTruthy();
  });
});
