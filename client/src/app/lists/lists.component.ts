import { Component, inject, OnDestroy } from '@angular/core';
import { LikesService } from '../_services/likes.service';
import { OnInit } from '@angular/core';
import { ButtonsModule } from 'ngx-bootstrap/buttons';
import { FormsModule } from '@angular/forms';
import { MemberCardComponent } from '../members/member-card/member-card.component';
import { PaginationModule } from 'ngx-bootstrap/pagination';

@Component({
  selector: 'app-lists',
  standalone: true,
  imports: [ButtonsModule, FormsModule, MemberCardComponent, PaginationModule],
  templateUrl: './lists.component.html',
  styleUrl: './lists.component.css'
})
export class ListsComponent implements OnInit, OnDestroy {

likeService = inject(LikesService); // Replace with actual LikesService type
predicate = 'liked'; // Default predicate, can be changed based on user interaction
pageNarber = 1;
pageSize = 5;

ngOnInit(): void {
    this.loadLikes();
  }

getTitle(): string {
  switch (this.predicate) {
    case 'liked': return 'Members you Likes';
    case 'likedBy': return 'Members who liked you';
    default: return 'Mutual Likes';
  }}

  loadLikes() {
    this.likeService.getLikes(this.predicate, this.pageNarber, this.pageSize);
  }

  pageChanged(event: any) {
    if (event.page !== this.pageNarber) {
    this.pageNarber = event.page;
    this.loadLikes();
    }
  }
  ngOnDestroy(): void {
    this.likeService.paginatedResult.set(null); // Clear paginated result on destroy
    this.likeService.likeId.set([]); // Clear like IDs on destroy
  }
}
