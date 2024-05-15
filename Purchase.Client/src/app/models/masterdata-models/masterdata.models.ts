
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
	orderDate: string;
	total: number;
	orderItems: [];
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
	orderItemId: string;
}

export interface Tax {
	id: string;
	code: string;
	name: string;
	rate: number;
}

export interface OrderItem {
	id: string;
	productId: string;
	productName: string;
	products: Product[];
	quantity: number;
	subTotal: number;
	taxRate: number;
	orderId: string;
}