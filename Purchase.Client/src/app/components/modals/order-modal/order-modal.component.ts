import { Component } from '@angular/core';
import { Order, Product, Customer } from '../../../models/masterdata-models/masterdata.models';

@Component({
  selector: 'app-order-modal',
  templateUrl: './order-modal.component.html',
  styleUrl: './order-modal.component.css'
})
export class OrderModalComponent {
  orders: Order[] = [];
  products: Product[] = [];
  customers: Customer[] = [];

  selectedOrder: Order = { items: [] };
  selectedProduct: Product ;
  selectedCustomer: Customer;
  quantity: number = 1;

  constructor(
    private orderService: OrderService,
    private productService: ProductService,
    private customerService: CustomerService
  ) {}

  ngOnInit(): void {
    this.loadProducts();
    this.loadCustomers();
  }

  loadProducts(): void {
    this.productService.getAllProducts().subscribe(
      (products: ProductDTO[]) => {
        this.products = products;
      },
      (error) => {
        console.error('Error fetching products:', error);
      }
    );
  }

  loadCustomers(): void {
    this.customerService.getAllCustomers().subscribe(
      (customers: CustomerDTO[]) => {
        this.customers = customers;
      },
      (error) => {
        console.error('Error fetching customers:', error);
      }
    );
  }

  addToOrder(): void {
    if (this.selectedProduct && this.quantity > 0) {
      const orderItem: OrderItemDTO = {
        productId: this.selectedProduct.id,
        productName: this.selectedProduct.name,
        quantity: this.quantity,
        subtotal: this.selectedProduct.price * this.quantity
      };
      this.selectedOrder.items.push(orderItem);
      // Reset product and quantity
      this.selectedProduct = null;
      this.quantity = 1;
    }
  }

  removeFromOrder(index: number): void {
    this.selectedOrder.items.splice(index, 1);
  }

  submitOrder(): void {
    if (this.selectedCustomer) {
      this.selectedOrder.customerId = this.selectedCustomer.id;
      this.orderService.createOrder(this.selectedOrder).subscribe(
        (response: BasicResponse) => {
          console.log('Order created successfully:', response);
          // Clear selected order
          this.selectedOrder = { items: [] };
        },
        (error) => {
          console.error('Error creating order:', error);
        }
      );
    }
  }

  getTotal(): number {
    return this.selectedOrder.items.reduce((total, item) => total + item.subtotal, 0);
  }

}
