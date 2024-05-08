import { KeyValuePipe } from "@angular/common";

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