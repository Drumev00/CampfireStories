import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ForeignStoriesComponent } from './foreign-stories.component';

describe('ForeignStoriesComponent', () => {
  let component: ForeignStoriesComponent;
  let fixture: ComponentFixture<ForeignStoriesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ForeignStoriesComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ForeignStoriesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
