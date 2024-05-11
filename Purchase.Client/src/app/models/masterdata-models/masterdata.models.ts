
export interface Category {
	id: string;
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
	id: string;
	code: string;
	name: string;
	price: number;
	hasTax: boolean;
	taxRate: number;
	quantity: number;
	description: string;
	categoryId: string;
}

export interface Tax {
	id: string;
	code: string;
	name: string;
	rate: number;
}

export interface OrderItem {
	id: string;
	products: [];
	quantity: number;
	subTotal: number;
	taxRate: number;
	orderId: string;
}