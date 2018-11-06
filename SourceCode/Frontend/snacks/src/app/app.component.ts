import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { WarehouseService } from './warehouse.service';
import { PagedResponse, Product } from './models';

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
  isPreviousDisabled: boolean;
  isNextDisabled: boolean;

  ngOnInit() {
    this.title = 'Welcome to Snacks Store!';
    this.form = this.formBuilder.group({
      productName: new FormControl('', [])
    });
    this.columnsForProducts = [
      'productName',
      'price',
      'likes',
      'stocks',
      'productDescription'
    ];
    this.response = new PagedResponse<Product>();
    this.response.pageSize = 10;
    this.response.pageNumber = 1;
    this.response.pageCount = 0;
    this.search();
  }

  search(): void {
    this
      .warehouseSvc
      .getProducts(this.response.pageSize, this.response.pageNumber, this.form.get('productName').value)
      .subscribe((data: PagedResponse<Product>) => {
        this.response = data;
        this.isPreviousDisabled = this.response.pageNumber === 1;
        this.isNextDisabled = this.response.pageNumber > this.response.pageCount;
      });
  }

  previous(): void {
    this.response.pageNumber -= 1;
    this.search();
  }

  next(): void {
    this.response.pageNumber += 1;
    this.search();
  }
}
