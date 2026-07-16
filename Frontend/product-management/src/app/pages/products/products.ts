import { CommonModule } from '@angular/common';
import { Component, signal } from '@angular/core';
import {  ProductService } from '../../services/productservice';
import { Prodcts } from '../../services/productinterface';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../services/authservice';
import { ToastrService } from 'ngx-toastr';
import { FormsModule, TouchedChangeEvent } from '@angular/forms';
import { Subject,debounceTime,distinctUntilChanged,takeUntil } from 'rxjs';
@Component({
  selector: 'app-products',
  imports: [CommonModule,RouterLink,FormsModule],
  templateUrl: './products.html',
  styleUrl: './products.css',
})
export class Products {
  imageBaseUrl = 'https://localhost:7070/';
    productdata = signal<Prodcts[]>([]);
selectedProductId: number | null = null;
selectedCategory = '';
currentPage:number = 1;
pageSize:number=5;
totalCount: number = 0;
sortColumn = '';
sortAscending = true;
searchText:string='';
private searchSubject = new Subject<string>();
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
  this.searchSubject
    .pipe(
      debounceTime(500),
      distinctUntilChanged()
    )
    .subscribe(() => {

      this.currentPage = 1;
      this.loadProducts();

    });



  this.loadProducts();
}
 

  loadProducts() {
      
    this.productService.getproducts(this.currentPage,
      this.pageSize,
      this.sortColumn,
      this.sortAscending,
      this.searchText,
      this.selectedCategory).subscribe({
       next: (res:any) => {

    this.productdata.set(res.products);
        this.totalCount = res.totalCount;
      

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
// get filteredProducts() {

//   return this.productdata().filter(product => {

//     const matchesSearch =
//       product.name
//         .toLowerCase()
//         .includes(this.searchText.toLowerCase());

//     const matchesCategory =
//       this.selectedCategory === '' ||
//       product.category === this.selectedCategory;

//     return matchesSearch && matchesCategory;

//   });

// }
 sort(column: string){
   

    if (this.sortColumn === column) {
    this.sortAscending = !this.sortAscending;
  } else {
    this.sortColumn = column;
    this.sortAscending = true;
  }

  this.currentPage = 1;
  this.loadProducts();
}
// get sortedProducts() {

//   const products = [...this.filteredProducts];

//   products.sort((a, b) => {

//     if (this.sortColumn === 'price') {

//       return this.sortAscending
//         ? a.price - b.price
//         : b.price - a.price;

//     }

//     if (this.sortColumn === 'quantity') {

//       return this.sortAscending
//         ? a.quantity - b.quantity
//         : b.quantity - a.quantity;

//     }

//     if (this.sortColumn === 'name') {

//       return this.sortAscending
//         ? a.name.localeCompare(b.name)
//         : b.name.localeCompare(a.name);

//     }

//     return 0;

//   });

//   return products;

//  }

get totalPages() {

  return Math.ceil(
    this.totalCount/ this.pageSize
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

  if (this.currentPage*this.pageSize < this.totalCount) {
    this.currentPage++;
    this.loadProducts();

  }

}

previousPage() {

  if (this.currentPage > 1) {

    this.currentPage--;
    this.loadProducts();

  }

}
goToPage(page: number) {
  this.currentPage = page;
  this.loadProducts();
}
onPageSizeChange() {
  this.currentPage = 1;
  this.loadProducts();
}
onSearchInput(event: Event) {

  const value = (event.target as HTMLInputElement).value;

  this.searchText = value;

  this.searchSubject.next(value);

}
onCategoryChange() {
  this.currentPage = 1;
  this.loadProducts();
}
viewProduct(id: number) {
  this.route.navigate(['/product-details', id]);
}
get pages(): number[] {
  return Array.from(
    { length: this.totalPages },
    (_, i) => i + 1
  );
}
}
