import { Component } from '@angular/core';
import { FormBuilder, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthService } from '../../services/authservice';
import { Router, RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';
import { ToastrService } from 'ngx-toastr';
@Component({
  selector: 'app-register',
  imports: [CommonModule,ReactiveFormsModule,RouterLink,FormsModule],
  standalone: true,
  templateUrl: './register.html',
  styleUrl: './register.css',
})
export class Register {
    form;
isLoading = false;
  constructor(
    private fb: FormBuilder,
    private auth: AuthService,
    private router: Router,
    private toastr:ToastrService
  ) {

    this.form = this.fb.group({
      name: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]]
    });
  }
 

 register() {



  this.auth.register(this.form.value as any)
    .subscribe({

      next: (res) => {
        this.isLoading = true;
    this.toastr.success('Registration Successfull','Success')
        this.router.navigate(['/login']);
      },

      error: (err) => {
        this.isLoading = false;
        console.log("ERROR RESPONSE:", err);
        this.toastr.error('Registraion Failed','error')
      }

    });

}
 
}
