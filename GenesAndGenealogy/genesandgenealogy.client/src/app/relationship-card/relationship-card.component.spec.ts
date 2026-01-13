import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RelationshipCardComponent } from './relationship-card.component';

describe('RelationshipCardComponent', () => {
  let component: RelationshipCardComponent;
  let fixture: ComponentFixture<RelationshipCardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [RelationshipCardComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RelationshipCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
