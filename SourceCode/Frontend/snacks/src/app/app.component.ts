import { Component, OnInit } from '@angular/core';
import { WarehouseService } from './warehouse.service';
import { PagedResponse, Product } from './models';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  constructor(private warehouseSvc: WarehouseService) {
  }

  title: string;
  response: PagedResponse<Product>;
  columnsForProducts: string[];

  ngOnInit() {
    this.title = 'Welcome to Snacks Store!';
    this.columnsForProducts = [
      'productName',
      'productDescription',
      'price',
      'likes',
      'stocks'
    ];
    this.warehouseSvc.getProducts().subscribe((data: PagedResponse<Product>) => {
      this.response = data;
    });
  }
}
