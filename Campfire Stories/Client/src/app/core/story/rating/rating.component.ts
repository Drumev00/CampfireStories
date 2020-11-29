import { Component, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-rating',
  templateUrl: './rating.component.html',
  styleUrls: ['./rating.component.css']
})
export class RatingComponent {
  selected = 0;
  hovered = 0;
  readonly = false;

  @Output() ratingValue = new EventEmitter<number>();

  get rating() {
    return this.selected = this.hovered;
  }

  giveRating() {
    this.ratingValue.emit(this.rating);
  }
}
