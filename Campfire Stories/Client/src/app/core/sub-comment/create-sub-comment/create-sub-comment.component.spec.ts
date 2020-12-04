import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateSubCommentComponent } from './create-sub-comment.component';

describe('CreateSubCommentComponent', () => {
  let component: CreateSubCommentComponent;
  let fixture: ComponentFixture<CreateSubCommentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CreateSubCommentComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateSubCommentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
