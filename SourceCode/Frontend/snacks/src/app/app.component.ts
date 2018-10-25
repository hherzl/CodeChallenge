import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { WarehouseService } from './warehouse.service';
import { PagedResponse, Product } from './models';
import { SelectorMatcher } from '@angular/compiler';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  constructor(private formBuilder: FormBuilder, private warehouseSvc: WarehouseService) {
  }

  title: string;
  form: FormGroup;
  response: PagedResponse<Product>;
  columnsForProducts: string[];

  ngOnInit() {
    this.title = 'Welcome to Snacks Store!';
    this.form = this.formBuilder.group({
      productName: new FormControl('', [])
    });
    this.columnsForProducts = [
      'productName',
      'productDescription',
      'price',
      'likes',
      'stocks'
    ];
    this.warehouseSvc.getProducts(null).subscribe((data: PagedResponse<Product>) => {
      this.response = data;
    });
  }

  search(): void {
    this.warehouseSvc.getProducts(this.form.get('productName').value).subscribe((data: PagedResponse<Product>) => {
      this.response = data;
    });
  }
}
