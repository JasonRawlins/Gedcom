import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AppFactsComponent } from './app-facts.component';

describe('AppFactsComponent', () => {
  let component: AppFactsComponent;
  let fixture: ComponentFixture<AppFactsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AppFactsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AppFactsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
