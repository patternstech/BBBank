import { TestBed } from '@angular/core/testing';

import { EntraIdService } from './entra-id.service';

describe('EntraIdService', () => {
  let service: EntraIdService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(EntraIdService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
