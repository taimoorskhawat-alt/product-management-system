import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Prodcts } from './productinterface';

@Injectable({
  providedIn: 'root',
})
export class Product {

  apiurl = "https://localhost:7070/api/prod";

  constructor(private http: HttpClient) {}

  getproducts() {
    return this.http.get<Prodcts[]>(this.apiurl);
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