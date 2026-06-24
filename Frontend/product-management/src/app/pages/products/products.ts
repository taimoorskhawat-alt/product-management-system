import { CommonModule } from '@angular/common';
import { Component, signal } from '@angular/core';
import {  ProductService } from '../../services/productservice';
import { Prodcts } from '../../services/productinterface';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../services/authservice';
import { ToastrService } from 'ngx-toastr';
import { FormsModule } from '@angular/forms';
@Component({
  selector: 'app-products',
  imports: [CommonModule,RouterLink,FormsModule],
  templateUrl: './products.html',
  styleUrl: './products.css',
})
export class Products {
    productdata = signal<Prodcts[]>([]);
selectedProductId: number | null = null;
selectedCategory = '';
currentPage = 1;
pageSize = 5;

searchText:string='';
  newProduct = {
    name: '',
    category: '',
    price: '',
    itemCode:'',
    brand:'',
    quantity:'',
  };


  constructor(private productService:ProductService,private route:Router,private auth:AuthService,private toastr:ToastrService) {}
  get role() {
  return this.auth.getRole();
}
ngOnInit(): void {
  this.loadProducts();
}
 

  loadProducts() {
    this.productService.getproducts().subscribe({
       next: (res) => {

    this.productdata.set(res);
  }
    });
  }
 deleteproducts(id: number) {
  this.selectedProductId = id;

  const modal = new (window as any).bootstrap.Modal(
    document.getElementById('deleteModal')
  );

  modal.show();
}
confirmDelete() {

  if (this.selectedProductId == null) return;

  this.productService.deleteproduct(this.selectedProductId)
    .subscribe({
      next: () => {

        this.productdata.update(products =>
          products.filter(p => p.id !== this.selectedProductId)
        );

        this.toastr.success('Product deleted successfully', 'Success');

        const modalEl = document.getElementById('deleteModal');
        const modal = (window as any).bootstrap.Modal.getInstance(modalEl);
        modal.hide();

        this.selectedProductId = null;
      },

      error: (err) => {
        console.error(err);
        this.toastr.error('Failed to delete product', 'Error');
      }
    });
}
  editproducts(id:number){
    this.route.navigate(['/edit-product',id]);
  }
get filteredProducts() {

  return this.productdata().filter(product => {

    const matchesSearch =
      product.name
        .toLowerCase()
        .includes(this.searchText.toLowerCase());

    const matchesCategory =
      this.selectedCategory === '' ||
      product.category === this.selectedCategory;

    return matchesSearch && matchesCategory;

  });

}
get paginatedProducts() {

  const startIndex = (this.currentPage - 1) * this.pageSize;

  const endIndex = startIndex + this.pageSize;

  return this.filteredProducts.slice(startIndex, endIndex);

}
get totalPages() {

  return Math.ceil(
    this.filteredProducts.length / this.pageSize
  );

}
get categories(): string[] {

  return [...new Set(
    this.productdata()
      .map(product => product.category)
      .filter(category => category)
  )];

}
nextPage() {

  if (this.currentPage < this.totalPages) {

    this.currentPage++;

  }

}

previousPage() {

  if (this.currentPage > 1) {

    this.currentPage--;

  }

}
viewProduct(id: number) {
  this.route.navigate(['/product-details', id]);
}
}
