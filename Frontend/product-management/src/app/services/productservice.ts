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

  addProduct(product: Prodcts,imageFile: File | null) {
      const formData = new FormData();

  formData.append('name', product.name);
  formData.append('price', product.price.toString());
  formData.append('category', product.category);
  formData.append('brand', product.brand);
  formData.append('itemCode', product.itemCode);
  formData.append('quantity', product.quantity.toString());
  formData.append('description', product.description ?? '');

  if (imageFile) {

    formData.append('image', imageFile);

  }
     return this.http.post(this.apiurl, formData);
  }

  deleteproduct(id: number) {
    return this.http.delete(`${this.apiurl}/${id}`);
  }

 editproduct(data: FormData, id: string) {
  return this.http.put(`${this.apiurl}/${id}`, data);
}

  getbyid(id: string) {
    return this.http.get<Prodcts>(`${this.apiurl}/${id}`);
  }
}