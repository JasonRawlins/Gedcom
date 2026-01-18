import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SourceInformationComponent } from './source-information.component';

describe('SourceComponent', () => {
  let component: SourceInformationComponent;
  let fixture: ComponentFixture<SourceInformationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [SourceInformationComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SourceInformationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
