import { Component, OnInit, signal } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import {  ProductService } from '../../services/productservice';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-product-details',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './product-details.html',
  styleUrl: './product-details.css',
})
export class ProductDetails implements OnInit {
  
 product = signal<any>(null);

  constructor(
    private route: ActivatedRoute,
    private productService:ProductService
  ) {}

  ngOnInit() {
    const id = this.route.snapshot.paramMap.get('id');
console.log("PRODUCT ID:", id);
    if (id) {
      this.getProduct(id);
    }
  }
 getProduct(id: string) {
  console.log("CALLING API WITH ID:", id);

 this.productService.getbyid(id).subscribe({
  next: (res) => {
    
    this.product.set(res);
  },
  error: (err) => {
    console.log(err);
  }
});
}

}
