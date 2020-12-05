import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditSubCommentComponent } from './edit-sub-comment.component';

describe('EditSubCommentComponent', () => {
  let component: EditSubCommentComponent;
  let fixture: ComponentFixture<EditSubCommentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EditSubCommentComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EditSubCommentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
