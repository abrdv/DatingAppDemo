import { Component, computed, inject, input } from '@angular/core';
import { Member } from '../../_models/member';
import { RouterLink } from '@angular/router';
import { LikesService } from '../../_services/likes.service';

@Component({
  selector: 'app-member-card',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './member-card.component.html',
  styleUrl: './member-card.component.css'
})
export class MemberCardComponent {
  private likeService = inject(LikesService); // Replace with actual LikesService type
  member = input.required<Member>();
hasLiked = computed(() =>
  this.likeService.likeId().includes(this.member().id))
toggleLike() {
  this.likeService.toggleLike(this.member().id).subscribe({
    next: () => {
      if (this.hasLiked()) {
        this.likeService.likeId.update(ids => ids.filter(id => id !== this.member().id));
      } else{
        this.likeService.likeId.update(ids => [...ids, this.member().id]);
      }
    },
    error: (error) => {
      console.error('Error toggling like:', error);
      // Optionally handle error, e.g., show an error message
    }
  });
}
}
