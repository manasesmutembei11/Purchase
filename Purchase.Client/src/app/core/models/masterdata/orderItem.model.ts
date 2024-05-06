export interface OrderItem {
	orderItemId: string;
	productId: string;
	productName: string;
	quantity: number;
	subTotal: number;
	taxRate: number;
	orderId: string;
}