import { TestBed } from '@angular/core/testing';
import { ResolveFn } from '@angular/router';

import { transferResolver } from './transfer.resolver';

describe('transferResolver', () => {
  const executeResolver: ResolveFn<boolean> = (...resolverParameters) => 
      TestBed.runInInjectionContext(() => transferResolver(...resolverParameters));

  beforeEach(() => {
    TestBed.configureTestingModule({});
  });

  it('should be created', () => {
    expect(executeResolver).toBeTruthy();
  });
});
