import { TestBed, inject } from '@angular/core/testing';

import { AuthenticationGuardsService } from './authentication-guards.service';

describe('AuthenticationGuardsService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [AuthenticationGuardsService]
    });
  });

  it('should be created', inject([AuthenticationGuardsService], (service: AuthenticationGuardsService) => {
    expect(service).toBeTruthy();
  }));
});
