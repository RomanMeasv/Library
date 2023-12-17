import { BorrowedDto } from '../borrowed/borrowedDto';

export type BookDto = {
  name: string;
  author: string;
  borrowed: BorrowedDto;
};
