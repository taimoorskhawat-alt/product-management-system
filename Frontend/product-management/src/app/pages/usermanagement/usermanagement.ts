import { Component, OnInit } from '@angular/core';
import { Userservice } from '../../services/userservice';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { Roleservice } from '../../services/roleservice';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-usermanagement',
  imports: [CommonModule,FormsModule],
  templateUrl: './usermanagement.html',
  styleUrl: './usermanagement.css',
})
export class Usermanagement implements OnInit{
 users: any[] = [];
 newRoleName: string = "";
 roles: any[] = [];
 editMode: boolean = false;
selectedRole: any = null;

  constructor(private userService: Userservice,private router:Router,private roleservice:Roleservice, private toastr:ToastrService) {
   
  }

  ngOnInit() {
    this.loadUsers();
    this.loadRoles();
  }

  loadUsers() {
    this.userService.getAllUsers().subscribe({
      next: (res: any) => {
        
        this.users = res;
      }
    });
  }
  updateRole(user: any) {
  this.userService.updateRole(user.id, user.role)
    .subscribe({
      next: () => {
        alert("Role updated successfully");
        this.loadUsers();
      }
    });
}
loadRoles() {
  this.roleservice.getRoles().subscribe(res => {
    
       this.roles = [...res];
  });
}
addRole() {

  if (!this.newRoleName) return;

  this.roleservice.addRole({ name: this.newRoleName }).subscribe({
    next: () => {

      this.toastr.success("Role added");

      this.newRoleName = "";

      this.loadRoles(); // 🔥 refresh dropdown

    },
    error: () => {
      this.toastr.error("Failed to add role");
    }
  });

}
deleteRole(id: number) {

  this.roleservice.deleteRole(id).subscribe({
    next: () => {

      this.toastr.success("Role deleted");

      this.loadRoles(); // 🔥 refresh dropdown

    },
    error: () => {
      this.toastr.error("Cannot delete role");
    }
  });

}
editRole(role: any) {
  this.editMode = true;
  this.selectedRole = { ...role };
}
updaterole() {

  this.roleservice.updateRole(this.selectedRole.id, this.selectedRole).subscribe({
    next: () => {

      this.toastr.success("Role updated");

      this.editMode = false;
      this.selectedRole = null;

      this.loadRoles(); // refresh dropdown

    },
    error: () => {
      this.toastr.error("Update failed");
    }
  });

}
}
