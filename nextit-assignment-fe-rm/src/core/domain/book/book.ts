import { Borrowed } from '../borrowed/borrowed';

export type Book = {
  id: string;
  name: string;
  author: string;
  borrowed: Borrowed;
};
