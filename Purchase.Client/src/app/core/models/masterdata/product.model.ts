export interface Product {
	productId: string;
	code: string;
	name: string;
	price: number;
	hasTax: boolean;
	taxRate: number;
	categoryId: string;
	categoryName: string;
	quantity: number;
	description: string;
}