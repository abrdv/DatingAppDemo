import { User } from "./user";

export class UserParams {
  pageNumber = 1;
  pageSize = 5;
  gender: string;
  minAge = 18;
  maxAge = 99;
  orderBy = 'lastActive';
  constructor (user: User | null) {
    this.gender = user?.gender === 'female' ? 'mail' : 'female';
  }
}
