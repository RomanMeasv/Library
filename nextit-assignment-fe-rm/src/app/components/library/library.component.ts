import { Component, OnInit } from '@angular/core';
import {
  FormGroup,
  FormControl,
  Validators,
  ReactiveFormsModule,
} from '@angular/forms';
import { Book } from '../../../core/domain/book/book';
import { BookDto } from '../../../core/dtos/book/bookDto';
import { BookService } from '../../services/book/book.service';
import {
  getMaxInputDate,
  convertBookDateToDateInput,
  convertDateInputToBookDate,
} from '../../util/date-util';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';

@Component({
  selector: 'app-library',
  standalone: true,
  imports: [CommonModule, HttpClientModule, ReactiveFormsModule],
  templateUrl: './library.component.html',
  styleUrl: './library.component.css',
  providers: [BookService],
})
export class LibraryComponent implements OnInit {
  // Variables
  books: Book[] = [];
  bookForm: FormGroup = new FormGroup({
    id: new FormControl<string>({ value: '', disabled: true }),
    name: new FormControl<string>('', [
      Validators.required,
      Validators.maxLength(15),
    ]),
    author: new FormControl<string>('', Validators.required),
    firstName: new FormControl<string>(''),
    lastName: new FormControl<string>(''),
    from: new FormControl<string>(''),
  });

  maxInputDate: string = '';
  isExistingSelected: boolean = false;

  constructor(private bookService: BookService) {
    // týmto obmedzujem dátum ktorý sa smie zadať do from input fieldu
    this.maxInputDate = getMaxInputDate();
  }

  ngOnInit(): void {
    this.getAllBooks();
  }

  // Public functions
  public selectBook(book: Book) {
    this.isExistingSelected = true;
    this.bookForm.setValue({
      id: book.id,
      name: book.name,
      author: book.author,
      firstName: book.borrowed.firstName,
      lastName: book.borrowed.lastName,
      from: convertBookDateToDateInput(book.borrowed.from),
    });
  }

  public onResetForm() {
    this.resetForm();
  }

  public onDeleteBook() {
    // IMPROVEMENT: Mohol som tu nejako passovať knihu/id
    let bookId = this.bookForm.get('id')?.value;

    // NOTICE: early return ak form nemá id
    // (teoreticky som mohol použiť aj isExistingSelected ale id je lepšie)
    if (!bookId) {
      return;
    }

    this.bookService.delete(bookId).subscribe(() => {
      this.books = this.books.filter((book) => book.id !== bookId);
      this.resetForm();
    });
  }

  // NOTICE: onSubmitForm rozhoduje či sa má vykonať update alebo create podľa toho či je selectnutá kniha
  public onSubmitForm() {
    if (this.isExistingSelected) {
      this.updateBook();
    } else {
      this.createBook();
    }
  }

  // Private functions
  private getAllBooks() {
    this.bookService.getAll().subscribe((books) => (this.books = books));
  }

  private createBook() {
    // IMPROVEMENT: mohol som použiť lepšiu validáciu
    if (this.bookForm.invalid) {
      return;
    }

    let dto = this.constructDtoFromForm();

    this.bookService
      .create(dto)
      .subscribe((createdBook) => this.books.push(createdBook));
  }

  private updateBook() {}

  // REFACTORING: helper funkcia na konštruovanie DTOčka
  private constructDtoFromForm(): BookDto {
    const dto: BookDto = {
      name: this.bookForm.get('name')?.value,
      author: this.bookForm.get('author')?.value,
      borrowed: {
        firstName: this.bookForm.get('firstName')?.value,
        lastName: this.bookForm.get('lastName')?.value,
        from: convertDateInputToBookDate(this.bookForm.get('from')?.value),
      },
    };

    return dto;
  }

  private resetForm() {
    this.isExistingSelected = false;
    this.bookForm.reset();
  }
}
