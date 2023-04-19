import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from 'src/app/shared/shared.module';
import { ProductsGeneratedModule } from './generated/products-generated.module';
import { ProductsRoutingModule } from './products-routing.module';
import { ProductEditComponent } from './product-edit/product-edit.component';
import { ProductListComponent } from './product-list/product-list.component';
import { GridCellsModule } from 'src/app/shared/grid-cells/grid-cells.module';
import { ENTRY_CKEDITOR_OPTIONS, FormlyCkeditorModule } from '@enigmatry/entry-form';
import ClassicEditor from '@ckeditor/ckeditor5-build-classic';

@NgModule({
  declarations: [
    ProductEditComponent,
    ProductListComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    GridCellsModule,
    ProductsGeneratedModule,
    ProductsRoutingModule,
    FormlyCkeditorModule
  ],
  providers: [
    {
      provide: ENTRY_CKEDITOR_OPTIONS,
      useValue: {
        build: ClassicEditor,
        config: {
          toolbar: ['bold', 'italic', 'bulletedList', 'numberedList', 'blockQuote', 'link']
        }
      }
    }]
})
export class ProductsModule { }
