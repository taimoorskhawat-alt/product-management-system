import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Product } from '../../services/productservice';

@Component({
  selector: 'app-edit-products',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './edit-products.html',
  styleUrl: './edit-products.css',
})
export class EditProducts {

  productForm: FormGroup;
  id: string | null = null;

  constructor(
    private fb: FormBuilder,
    private activeRoute: ActivatedRoute,
    private productservice: Product,
    private router: Router
  ) {

    this.productForm = this.fb.group({
      name: [''],
      category: [''],
      price: [''],
      brand:[''],
      quantity:[''],
      itemCode:[''],
      description:['']
    });
  }
   editproducts() {
    if (this.id) {
      this.productservice.editproduct(this.productForm.value, this.id)
        .subscribe(() => {
          console.log("update success");
          
          this.router.navigate(['/products']);
        });
    }
  }

  ngOnInit() {
    this.id = this.activeRoute.snapshot.paramMap.get('id');

    if (this.id) {
      this.productservice.getbyid(this.id).subscribe((data: any) => {
        this.productForm.patchValue({
          name: data.name,
          category: data.category,
          price: data.price,
          brand:data.brand,
          quantity:data.quantity,
          itemCode:data.itemCode,
          description:data.description
        });
      });
    }
  }

 
}