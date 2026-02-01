import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MultimediaItemComponent } from './multimedia-item.component';

describe('MultimediaItemComponent', () => {
  let component: MultimediaItemComponent;
  let fixture: ComponentFixture<MultimediaItemComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [MultimediaItemComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MultimediaItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
