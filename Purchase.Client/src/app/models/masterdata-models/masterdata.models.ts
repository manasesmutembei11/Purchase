
export interface Category {
	id: string;
	name: string;
	code: string;
	description: string;
}

export interface Customer {
	id: string;
	firstName: string;
	lastName: string;
	address: string;
	email: string;
	phone: string;
}

export interface Order {
	id: string;
	customerId: string;
	productId: string;
	orderDate: string;
	total: number;
	customerName: string;
	customerPhone: string;
	orderItems: [];
}

export interface Product {
	id: string;
	code: string;
	name: string;
	price: number;
	quantity: number;
	description: string;
	categoryId: string;
	taxRate: number;
	totalPrice: number;
	taxId: string;
}

export interface Tax {
	id: string;
	code: string;
	name: string;
	rate: number;
}

export interface OrderItem {
	id: string;
	label: string;
	quantity: number;
	unitPrice: number;
	subTotal: number;
	productId: string;
}