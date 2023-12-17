import { Injectable } from '@angular/core';
import { Book } from '../../../core/domain/book/book';
import { Observable, catchError, of, tap } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BookDto } from '../../../core/dtos/book/bookDto';

@Injectable({
  providedIn: 'root',
})
export class BookService {
  private apiUrl = 'https://localhost:7014/Book';

  private httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
  };

  constructor(private http: HttpClient) {}

  // Public functions
  public getAll(): Observable<Book[]> {
    return this.http.get<Book[]>(this.apiUrl).pipe(
      tap((_) => this.log('fetching all books')),
      catchError(
        this.handleError<Book[]>(
          'an error occured while fetching all books',
          []
        )
      )
    );
  }

  public create(dto: BookDto): Observable<Book> {
    return this.http.post<Book>(this.apiUrl, dto, this.httpOptions).pipe(
      tap((book: Book) => this.log(`created new book with id ${book.id}`)),
      catchError(this.handleError<Book>('an error occured while creating book'))
    );
  }

  public delete(id: string): Observable<void> {
    const preparedUrl = `${this.apiUrl}/${id}`;
    return this.http.delete<void>(preparedUrl, this.httpOptions).pipe(
      tap((_) => this.log(`deleting book with id ${id}`)),
      catchError(this.handleError<void>('an error occured while deleting book'))
    );
  }

  // Private functions
  // Táto funkcia slúži ako všeobecný error handling tejto service
  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      console.error(error);
      this.log(`${operation} failed: ${error.message}`);
      return of(result as T);
    };
  }

  // Táto funkcia slúži ako jednoduchý logging systém s výpisom do konzole
  private log(message: string) {
    console.log(`Book Service: ${message}`);
  }
}
