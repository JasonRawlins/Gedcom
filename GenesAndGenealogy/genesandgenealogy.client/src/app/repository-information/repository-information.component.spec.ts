import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RepositoryInformationComponent } from './repository-information.component';

describe('RepositoryInformationComponent', () => {
  let component: RepositoryInformationComponent;
  let fixture: ComponentFixture<RepositoryInformationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [RepositoryInformationComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RepositoryInformationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
