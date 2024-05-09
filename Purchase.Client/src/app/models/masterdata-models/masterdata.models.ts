
export interface Category {
	categoryId: string;
	name: string;
	code: string;
	description: string;
}

export interface Customer {
	customerId: string;
	firstName: string;
	lastName: string;
	address: string;
	email: string;
	phone: string;
}

export interface Order {
	orderId: string;
	customerId: string;
	customerName: string;
	orderDate: string;
	total: number;
}

export interface Product {
	productId: string;
	code: string;
	name: string;
	price: number;
	hasTax: boolean;
	taxRate: number;
	quantity: number;
	description: string;
	category: Category;
}

export interface Tax {
	taxId: string;
	code: string;
	name: string;
	rate: number;
}

export interface OrderItem {
	orderItemId: string;
	productId: string;
	productName: string;
	quantity: number;
	subTotal: number;
	taxRate: number;
	orderId: string;
	selectpProduct: Product| any;
}