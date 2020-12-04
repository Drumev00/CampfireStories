import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ListSubCommentsComponent } from './list-sub-comments.component';

describe('ListSubCommentsComponent', () => {
  let component: ListSubCommentsComponent;
  let fixture: ComponentFixture<ListSubCommentsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ListSubCommentsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ListSubCommentsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
