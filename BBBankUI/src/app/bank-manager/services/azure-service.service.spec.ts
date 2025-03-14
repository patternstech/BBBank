import { TestBed } from '@angular/core/testing';
import { AzureAccessService } from './azure-service.service';



describe('AzureAccessService', () => {
  let service: AzureAccessService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AzureAccessService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
