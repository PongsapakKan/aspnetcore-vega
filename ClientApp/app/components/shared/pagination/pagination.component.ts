import { Component, Input, Output, EventEmitter, OnChanges } from '@angular/core';

@Component({
  selector: 'app-pagination',
  templateUrl: './pagination.component.html',
  styleUrls: ['./pagination.component.css']
})
export class PaginationComponent implements OnChanges {
  @Input('total-items') totalItems: number;
  @Input('page-size') pageSize: number;
  @Output('page-change') pageChange = new EventEmitter<number>();
  pages: any[];
  currentPage: number = 1;
  
  constructor() { }

  ngOnChanges() {
    this.currentPage = 1;

    const pagesCount = Math.ceil(this.totalItems / this.pageSize);
    this.pages = []

    for (let i = 1; i <= pagesCount; i++)
      this.pages.push(i);
  }

  onSelectPage(selectedPage) {
    this.currentPage = selectedPage;
    this.pageChange.emit(this.currentPage);
  }
  
  onPrevPage() {
    if (this.currentPage === 1)
      return;

    this.pageChange.emit(--this.currentPage);
  }

  onNextPage() {
    if (this.currentPage === this.pages.length)
      return;
    this.pageChange.emit(++this.currentPage);
  }
}
