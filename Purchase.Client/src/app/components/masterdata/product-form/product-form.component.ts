import { Location } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { BaseFormComponent } from '../../base/base-form-component';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { ProductService } from '../../../services/masterdata-services/product.service';
import { Guid } from 'guid-typescript';
import { first } from 'rxjs';
import { Product, Category } from '../../../models/masterdata-models/masterdata.models';
import { cloneDeep } from 'lodash';

@Component({
  selector: 'app-product-form',
  templateUrl: './product-form.component.html',
  styles: []
})
export class ProductFormComponent extends BaseFormComponent implements OnInit {
  private categories: Category[] = []
  form: FormGroup = this.fb.group({});
  constructor(
    private route: ActivatedRoute,
    private fb: FormBuilder,
    public location: Location,
    private productService: ProductService,
    
  ) {
    super();
  }
  ngOnInit(): void {
    this.form = this.createForm();
    this.route.params.pipe().subscribe((params) => {
      this.id = params['id'] ? params['id'] : '';
      this.editMode = params['id'] != null;
      this.pageTitle = this.editMode ? 'Edit Product' : 'New Product';
      this.breadCrumbItems = [
        { label: 'Master Data' },
        { label: 'Product' },
        { label: this.pageTitle, active: true },
      ];
      this.buttonText = this.editMode ? 'Update' : 'Create';
      this.initForm();
    });
  }
  createForm(): FormGroup {
    const f = this.fb.group({
      code: ['', [Validators.required]],
      name: ['', Validators.required],
      price: [0, Validators.required],
      hasTax: [0],
      taxRate: [0,],
      quantity: [0, Validators.required],
      description: ['', Validators.required],
      category: ['', Validators.required],
      productId: [Guid.create().toString()],
    });
    return f;
  }
  initForm() {
    if (this.editMode) {
      this.productService.findById(this.id).pipe(first())
        .subscribe({
          next: (_) => {
            this.form.patchValue(_);
          }
        })
    }
  }
  onSubmit() {
    this.submitted = true;
    if (this.validateForm(this.form)) {
      const model: Product = cloneDeep(this.form.value)
      console.log("onSubmit =>",model);
      

      this.productService.save(model).subscribe({
        next: (_) => {
          this.location.back();
        },
        error: (errors) => {
          this.errors = errors;
          console.log('Error =>', this.errors);
        },
      });
    }
  }
  back(): void {
    this.location.back();
  }

}
