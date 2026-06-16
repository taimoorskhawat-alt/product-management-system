import { Component } from '@angular/core';
import { Prodcts } from '../../services/productinterface';
import {  ProductService } from '../../services/productservice';
import { Router } from '@angular/router';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { required } from '@angular/forms/signals';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-add-product',
  imports: [ReactiveFormsModule],
  templateUrl: './add-product.html',
  styleUrl: './add-product.css',
})
export class AddProduct {
     product: Prodcts = {
    id: 0,
    name: '',
    price: 0,
    category: '',
    brand:'',
    itemCode:'',
    quantity:0,
    description:''
  };

  constructor(private productService:ProductService, private router:Router,private toastr:ToastrService) {}

 productForm = new FormGroup({

  itemCode: new FormControl('',[Validators.required,Validators.minLength(2)]),

  name: new FormControl('',[Validators.required,Validators.pattern('^[a-zA-Z ]+$')]),

  category: new FormControl('',[Validators.required]),

  brand: new FormControl('',[Validators.required]),

  price: new FormControl(0,[Validators.required,Validators.min(1)]),

  quantity: new FormControl(0,[Validators.required,Validators.min(1)]),

  description: new FormControl('')

});

  addProduct() {
    if (this.productForm.invalid) return;

    const product:Prodcts = {
      id: 0,
      name: this.productForm.value.name!,
      price: this.productForm.value.price!,
      category: this.productForm.value.category!,
      itemCode: this.productForm.value.itemCode!,
      quantity: this.productForm.value.quantity!,
      brand: this.productForm.value.brand!,
      description:this.productForm.value.description!,
    };

    this.productService.addProduct(product).subscribe({
      next: () => {
      
        this.toastr.success('Product Added Successfully','Success')

        this.productForm.reset();
        this.router.navigate(['/products'])

      },
      error: (err) => {
        console.error(err);
      }
    });
  }
}
