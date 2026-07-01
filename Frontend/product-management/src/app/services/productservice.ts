import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Prodcts } from './productinterface';

@Injectable({
  providedIn: 'root',
})
export class ProductService {

  apiurl = "https://localhost:7070/api/prod";

  constructor(private http: HttpClient) {}

  getproducts(page: number, pageSize: number , sortColumn: string,
  sortAscending: boolean, search: string, category: string) {
    return this.http.get<any>(`${this.apiurl}?page=${page}&pageSize=${pageSize}&sortColumn=${sortColumn}&sortAscending=${sortAscending}&search=${search}&category=${category}`);
  }

  addProduct(product: Prodcts) {
    return this.http.post<Prodcts>(this.apiurl, product);
  }

  deleteproduct(id: number) {
    return this.http.delete(`${this.apiurl}/${id}`);
  }

  editproduct(data: any, id: string) {
    return this.http.put(`${this.apiurl}/${id}`, data);
  }

  getbyid(id: string) {
    return this.http.get<Prodcts>(`${this.apiurl}/${id}`);
  }
}