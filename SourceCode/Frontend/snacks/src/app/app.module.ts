import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule } from '@angular/common/http';
import { AppMaterialModule } from './app-material.module';
import { WarehouseService } from './warehouse.service';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    HttpClientModule,
    AppMaterialModule
  ],
  exports: [
    AppMaterialModule
  ],
  providers: [
    WarehouseService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
