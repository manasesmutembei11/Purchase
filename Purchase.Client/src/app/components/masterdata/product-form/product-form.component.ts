import { Location } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { BaseFormComponent } from '../../base/base-form-component';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { ProductService } from '../../../services/masterdata-services/product.service';
import { Guid } from 'guid-typescript';
import { first } from 'rxjs';
import { Product, Category, OrderItem } from '../../../models/masterdata-models/masterdata.models';
import { cloneDeep } from 'lodash';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { CategorySelectionModalComponent } from '../../modals/category-selection-modal/category-selection-modal.component';

@Component({
  selector: 'app-product-form',
  templateUrl: './product-form.component.html',
  styles: []
})
export class ProductFormComponent extends BaseFormComponent implements OnInit {
  form: FormGroup = this.fb.group({});
  constructor(
    private route: ActivatedRoute,
    private fb: FormBuilder,
    public location: Location,
    private productService: ProductService,
    private modalService: NgbModal,
    
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
      hasTax: [false, Validators.required],
      taxRate: [0,],
      quantity: [0, Validators.required],
      description: ['', Validators.required],
      category: [''],
      categoryId: [''],
      id: [Guid.create().toString()],
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
        error: (error) => {
          this.error = error;
          console.log('Error =>', this.error);
        },
      });
    }
  }
  back(): void {
    this.location.back();
  }

  openCategoryModal() {
    const modalRef = this.modalService.open(CategorySelectionModalComponent, { size: 'lg' });
    modalRef.componentInstance.selectedCategory.subscribe((category: Category) => {
      this.form.patchValue({
        category: category.name,
        categoryId: category.id
      });
    });
  }
  

}
