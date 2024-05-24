

export interface User {
    permissions: string[];
    id: string;
    userName: string;
    firstName: string;
    lastName: string;
    email: string;
    phoneNumber?: any;
    active: boolean;
    emailConfirmed: boolean;
    roles:UserRole[],
    accounts:Account[]
}

export interface UserRole {
    id: string;
    name: string;

    checked: boolean;
}
export interface ForgotPassword {
    email: string;
}

export interface ResetPassword {
    password: string;
    confirmPassword: string;
    email: string;
    token: string;
}

export interface Account {
	id: string;
	name: string;
	typeName: string;    
	code: string;
	
	
}