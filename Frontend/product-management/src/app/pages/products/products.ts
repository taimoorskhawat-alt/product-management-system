import { CommonModule } from '@angular/common';
import { Component, signal } from '@angular/core';
import { Product } from '../../services/productservice';
import { Prodcts } from '../../services/productinterface';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../services/authservice';

@Component({
  selector: 'app-products',
  imports: [CommonModule,RouterLink],
  templateUrl: './products.html',
  styleUrl: './products.css',
})
export class Products {
    productdata = signal<Prodcts[]>([]);

  
  newProduct = {
    name: '',
    category: '',
    price: '',
    itemCode:'',
    brand:'',
    quantity:'',
  };


  constructor(private productService:Product,private route:Router,private auth:AuthService) {}
  get role() {
  return this.auth.getRole();
}
ngOnInit(): void {
  this.loadProducts();
}
 

  loadProducts() {
    this.productService.getproducts().subscribe({
      next: (data: any[]) => {
       
        this.productdata.set(data);
      }
    });
  }
  deleteproducts(id:number){
if(confirm("Are You Sure U Want To Delete")){
  this.productService.deleteproduct(id).subscribe(()=>{
    this.productdata.update(products=>products.filter(product=>product.id!==id));
  });
}
  }
  editproducts(id:number){
    this.route.navigate(['/edit-product',id]);
  }
}
