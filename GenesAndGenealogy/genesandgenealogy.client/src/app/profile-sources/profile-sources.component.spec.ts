import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProfileSourcesComponent } from './profile-sources.component';

describe('ProfileSourcesComponent', () => {
  let component: ProfileSourcesComponent;
  let fixture: ComponentFixture<ProfileSourcesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ProfileSourcesComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ProfileSourcesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
