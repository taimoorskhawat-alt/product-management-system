import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ProductService } from '../../services/productservice';

@Component({
  selector: 'app-edit-products',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './edit-products.html',
  styleUrl: './edit-products.css',
})
export class EditProducts {
selectedFile: File | null = null;
imageBaseUrl = 'https://localhost:7070/';
  productForm: FormGroup;
  id: string | null = null;

  constructor(
    private fb: FormBuilder,
    private activeRoute: ActivatedRoute,
    private productservice: ProductService,
    private router: Router
  ) {

    this.productForm = this.fb.group({
      name: [''],
      category: [''],
      price: [''],
      brand:[''],
      quantity:[''],
      itemCode:[''],
      description:[''],
      imageUrl: [''] 
    });
  }
 editproducts() {

  if (!this.id) return;

  const formData = new FormData();

  formData.append('name', this.productForm.value.name);
  formData.append('category', this.productForm.value.category);
  formData.append('price', this.productForm.value.price);
  formData.append('brand', this.productForm.value.brand);
  formData.append('quantity', this.productForm.value.quantity);
  formData.append('itemCode', this.productForm.value.itemCode);
  formData.append('description', this.productForm.value.description);

  if (this.selectedFile) {
    formData.append('image', this.selectedFile);
  }

  this.productservice.editproduct(formData, this.id)
    .subscribe(() => {
      this.router.navigate(['/products']);
    });

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
        (this.productForm as any).imageUrl = data.imageUrl;
      });
    }
  }
onFileSelected(event: Event) {

  const input = event.target as HTMLInputElement;

  if (input.files && input.files.length > 0) {
    this.selectedFile = input.files[0];
  }

}
 
}