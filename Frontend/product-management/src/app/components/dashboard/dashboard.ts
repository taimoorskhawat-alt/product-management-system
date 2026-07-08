import { Component, OnInit, signal } from '@angular/core';
import { ProductService } from '../../services/productservice';
import { DecimalPipe } from '@angular/common';

@Component({
  selector: 'app-dashboard',
  imports: [DecimalPipe],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.css',
})
export class Dashboard implements OnInit {

  dashboardData = signal<any>(null);

  constructor(
    private productService: ProductService
  ) {}


  ngOnInit() {

    this.productService.getDashboard()
      .subscribe({
        next: (res) => {

          this.dashboardData.set(res);

          console.log(res);

        },
        error: (err) => {

          console.log(err);

        }
      });

  }
  getProgressColor(category: string): string {

  switch (category) {

    case 'Electronics':
      return 'bg-primary';

    case 'Food':
      return 'bg-success';

    case 'Sports':
      return 'bg-warning';

    case 'Books':
      return 'bg-info';

    case 'Furniture':
      return 'bg-secondary';

    case 'Medicine':
      return 'bg-danger';

    case 'Fashion':
      return 'bg-dark';

    case 'Others':
      return 'bg-primary';

    default:
      return 'bg-secondary';
  }

}

}
